using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CategoryController : MonoBehaviour {
	public string fileName = "academicword";


	public QuizType quizType = QuizType.RandomWrong;

	public GameObject categoryItemModel;
	private Transform category1Grid;
	private Transform category2Grid;

	static private string _currentCategory1="";
	static private string _currentCategory2="";
	static public string currentCategory1{
		get {
			return  _currentCategory1;
		}
	}
	static public string currentCategory2{
		get {
			return  _currentCategory2;
		}
	}

	static private List<CategoryLevel1> data = new List<CategoryLevel1> () ;
	static public List<Dictionary<string,object>> rawData;
	static public CategoryController instance;
	void Start () {
		rawData = CSVReader.Read ("English/"+fileName);

		// category1: level, and its sets
		Dictionary<string, List<string>>  category1 = new Dictionary<string,  List<string>> ();
		for(var i=0; i < rawData.Count; i++) {
			if (category1.ContainsKey (rawData [i] ["level"].ToString ())) {
				if (category1 [rawData [i] ["level"].ToString ()].Contains (rawData [i] ["subset"].ToString ()) == false) {
					category1 [rawData [i] ["level"].ToString ()].Add (rawData [i] ["subset"].ToString ());
				}
			} else {
				category1 [rawData [i] ["level"].ToString ()] = new List<string> ();
				category1 [rawData [i] ["level"].ToString ()].Add(rawData [i] ["subset"].ToString ());
			}
		}



		instance = this;
		category1Grid = transform.FindChild ("Category1/Scroll View/UIGrid");
		category2Grid = transform.FindChild ("Category2/Scroll View/UIGrid");
		int index = 0;
		foreach (KeyValuePair<string,List<string>> item in category1) {
			data.Add ((new CategoryLevel1 ()));
			string st = "";
			foreach (string subset in item.Value){
				if (quizType == QuizType.RandomWrong) {
					st += "Quiz " + subset
						+ ".1,";
				} else if (quizType == QuizType.RandomWrong) {
					st += "Quiz "+subset 
						+ ".1,Quiz "
						+subset
						+".2,";
				} else if (quizType == QuizType.FixedWrong) {
					st += "Quiz " + subset
						+ ".1,";
				}
			}
			data [index].Initialize (item.Key ,st);
			index++;
		}
	}

	public void ShowNextQuiz(){
		for (int i=0; i<data.Count; i++){
			if (data[i].categoryName==_currentCategory1){
				// Found _currentCategory1
				if (data[i].subCategoryNames.IndexOf(_currentCategory2)+1<data[i].subCategoryNames.Count){
					// Same _currentCategory1, next _currentCategory2
					_currentCategory2=data[i].subCategoryNames[data[i].subCategoryNames.IndexOf(_currentCategory2)+1];
					break;
				} else {
					if (i+1>=data.Count){
						// No more quiz. Go to first one
						_currentCategory1 = data[0].categoryName;
						_currentCategory2 = data[0].subCategoryNames[0];
					} else {
						// Next _currentCategory1
						_currentCategory1 = data[i+1].categoryName;
						_currentCategory2 = data[i+1].subCategoryNames[0];
						break;
					}
				}
			}
		}
		ShowQuiz (_currentCategory1, _currentCategory2);
	}

	private int updateCounter=0;
	public void TriggerEffect(){
		updateCounter = 0;
	}

	public int firstCategory2=0;
	void Update () {
		// Need to show twice because of scroll's strange initial position offset.
		if (updateCounter <= 1) {
			updateCounter += 1;
			ShowLevel1Names ();
			if (_currentCategory1 != "") {
				ShowLevel2Names (_currentCategory1);
			}
		}

	}

	static public void ShowLevel1Names(){
		NGUITools.DestroyChildren (instance.category1Grid);
		for (var i = 0; i < data.Count; i++) {
			GameObject newItem = NGUITools.AddChild (instance.category1Grid.gameObject, instance.categoryItemModel);
			newItem.GetComponent<CategoryItem> ().Initialize (null, data [i].categoryName, data [i].subCategoryNames);
		}
		instance.category1Grid.GetComponent<UIGrid> ().Reposition ();
		instance.transform.FindChild ("Category1/ScrollBarContainer/Scroll Bar").GetComponent<UIScrollBar> ().scrollValue = 0;
		instance.transform.FindChild ("Category1/Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	static public void ShowLevel2Names(string level1Name){
		foreach (Transform category1Item in instance.transform.FindChild("Category1/Scroll View/UIGrid").transform) {
			if (level1Name == category1Item.GetComponent<CategoryItem> ().categoryName) {
				category1Item.FindChild ("Button").GetComponent<MenuButton> ().ButtonClicked();
				//ChangeColor (category1Item.gameObject, Color.white);
			} else {
				if (category1Item.FindChild ("Button").GetComponent<MenuButton> ().IsPressed ()) {
					category1Item.FindChild ("Button").GetComponent<MenuButton> ().ButtonReleased ();
				}
				//ChangeColor (category1Item.gameObject, Color.red);
			}
		}

		NGUITools.DestroyChildren (instance.category2Grid);
		for (var i = 0; i < data.Count; i++) {
			if (data [i].categoryName == level1Name) {
				for (var j = 0; j < data[i].subCategoryNames.Count; j++) {
					GameObject newItem = NGUITools.AddChild (instance.category2Grid.gameObject, instance.categoryItemModel);

					float percent = GameState.GetRecord (level1Name, data [i].subCategoryNames [j]);
					string rate =  Util.PercentageToGrade(percent);
					newItem.GetComponent<CategoryItem> ().Initialize (level1Name, data[i].subCategoryNames[j], null,rate);
				}
			}
		}
		instance.category2Grid.GetComponent<UIGrid> ().Reposition ();
		instance.transform.FindChild ("Category2/Scroll View").GetComponent<UIScrollView> ().ResetPosition ();

	}

	static public void ShowQuiz(string level1Name, string level2Name){
		_currentCategory1 = level1Name;
		_currentCategory2 = level2Name;
		UIStateController.ShowState ("QuestionPanel");
	}


	static private void ChangeColor(GameObject component, Color color){
		Transform background = component.transform.FindChild ("Button/Background");
		foreach (Transform child in background) {
			child.GetComponent<UI2DSprite> ().color = color;
		}
	}

}

public class CategoryLevel1{
	public string categoryName;
	public List<string> subCategoryNames = new  List<string>(); 

	public void Initialize(string categoryName, string subCategories){
		this.categoryName = categoryName;
		string[] names = subCategories.Split (',');
		for (var i = 0; i < names.Length; i++) {
			if (names [i] != "") {
				subCategoryNames.Add (names [i]);
			}
		}
	}
}
public enum QuizType {RandomWrong, RandonWrongAndSpererable, FixedWrong};