using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CategoryItem : MonoBehaviour {

	public string categoryName;
	public List<string> subCategoryNames = new  List<string>();
	public string superCategory;

	public void Start(){
		transform.GetComponent<ButtonAppearEffect> ().Trigger ();
	}
	public void Initialize(string superCategory, string categoryName, List<string> subCategoryNames, string currentRate=null){
		this.superCategory = superCategory;
		this.categoryName = categoryName;
		if (subCategoryNames != null) {
			for (var i = 0; i < subCategoryNames.Count; i++) {
				this.subCategoryNames.Add (subCategoryNames [i]);
			}
		} else {
			this.subCategoryNames = null;
		}
		transform.FindChild ("Button/Label").GetComponent<UILabel>().text = categoryName;
		if (currentRate == null) {
			transform.FindChild ("Button/Rate").gameObject.SetActive (false);
		} else {
			transform.FindChild ("Button/Rate").gameObject.SetActive (true);
			transform.FindChild ("Button/Rate").gameObject.GetComponent<UILabel> ().text = currentRate;
			if (currentRate.Contains ("A") || currentRate.Contains ("B")) {
				transform.FindChild ("Button/Rate").gameObject.GetComponent<UILabel> ().color = Color.green;
				transform.FindChild ("Button/Rate/Circle").gameObject.GetComponent<UI2DSprite>().color = Color.green;
			} else {
				transform.FindChild ("Button/Rate").gameObject.GetComponent<UILabel> ().color = Color.red;
				transform.FindChild ("Button/Rate/Circle").gameObject.GetComponent<UI2DSprite>().color = Color.red;
			}
		}
	}

	public void ButtonClicked(){
		if (superCategory == null) {
			// Level 1
			CategoryController.ShowLevel2Names(categoryName);
		} else {
			// Level 2
			CategoryController.ShowQuiz(superCategory,categoryName);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

}
