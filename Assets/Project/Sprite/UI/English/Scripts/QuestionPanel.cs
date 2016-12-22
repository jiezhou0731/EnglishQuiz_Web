using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPanel : UITransitionController {
	static QuestionPanel instance;
	public TextQuestion textQuestion;
	public FillEffect tick;
	public FillEffect crossLeft;
	public FillEffect crossRight;
	public ShakingLabelDisplay quizTitle;
	public GameObject finishedPanel;
	private GameObject rateBoxPanel;

	// Problem Set Start!
	public void TriggerEffect(){
		Everyplay.StartRecording ();
		finishedPanel.SetActive (false);
		rateBoxPanel = transform.FindChild ("RateBox").gameObject;
		rateBoxPanel.SetActive (false);
		ScoreDisplayTwoPlayers.instance.Reset ();

		category1 = CategoryController.currentCategory1;
		string[] category2 = CategoryController.currentCategory2.Substring (5).Split ('.');
		subset = category2[0];
		if (category2[1]=="1"){
			MessageBubbleManager.AddTextMessage("Select the correct definition.",0.5f,10f);
			questionType = "Definition";
		}else if (category2[1]=="2"){
			MessageBubbleManager.AddTextMessage("Select if the two words are separable.",0.5f,10f);
			questionType = "Separable";
		}
		data = CategoryController.rawData;
		GenerateProblemSet ();

		string title = category1 + " "
		               + CategoryController.currentCategory2 + "\n"
		               + problemSet.Count + " Questions";
		
		quizTitle.SetLabel (title);
		ShowNextQuestion ();
	}


	 public void ShowNextQuestion(){
		tick.gameObject.SetActive (false);
		crossLeft.gameObject.SetActive (false);
		crossRight.gameObject.SetActive (false);
		transform.FindChild ("Transition/WrongAnswerContinue").gameObject.SetActive (false);
		currentQuestionIndex++;
		if (UnityAdsInit.IsMandatoryAdsReady () 
			&& (currentQuestionIndex == problemSet.Count / 2||currentQuestionIndex == problemSet.Count -1)) {
			UnityAdsInit.ForceToPlayAds ();
		}
		if (currentQuestionIndex < problemSet.Count) {
			if (questionType == "Separable") {
				Dictionary<string,bool> answers = new Dictionary<string,bool> ();
				answers ["Separable."] = (problemSet [currentQuestionIndex] [1] == "yes");
				answers ["Not separable."] = (problemSet [currentQuestionIndex] [1] == "no");
				textQuestion.SetNewQuestion (problemSet [currentQuestionIndex] [0], answers);
			} else if (questionType == "Definition") {
				Dictionary<string,bool> answers = new Dictionary<string,bool> ();
				int correctAnswerIndex = Random.Range (0, 4);
				Dictionary<string, bool> usedDefinition = new Dictionary<string, bool> ();
				for (var i = 0; i < 4; i++) {
					if (correctAnswerIndex == i) {
						answers [problemSet [currentQuestionIndex] [1]] = true;
						usedDefinition [problemSet [currentQuestionIndex] [1]] = true;
					} else {
						string randomSelect = data [Random.Range (0, data.Count)] ["definition"].ToString ();
						int failSafe = 0;
						while (usedDefinition.ContainsKey (randomSelect) && failSafe < 50) {
							failSafe += 1;
							randomSelect = data [Random.Range (0, data.Count)] ["definition"].ToString ();
						}
						answers [randomSelect] = false;
						usedDefinition [randomSelect] = true;
					}
				}
				textQuestion.SetNewQuestion (problemSet [currentQuestionIndex] [0], answers);
			}
		} else {
			GoTo ("FinishedTransition");
		}
	}

	public void CorrectAnswerClicked(){
		tick.Trigger ();
		SoundManager.Play ("CorrectAnswer");
		ScoreDisplayTwoPlayers.instance.IncreaseLeftScore (1);
		GoTo ("CorrectAnswerClickedTransition");
	}

	public void IncorrectAnswerClicked(){
		SoundManager.Play ("WrongAnswer");
		crossLeft.Trigger ();
		crossRight.Trigger ();
		ScoreDisplayTwoPlayers.instance.IncreaseRightScore (1);
		GoTo ("IncorrectAnswerClickedTransition");
	}

	public void TipsButtonClicked(){
	}

	public void HomeButtonClicked(){
		UIStateController.ShowState ("Category");
	}

	public void NextQuizButtonClicked(){
		float percent = 1f * ScoreDisplayTwoPlayers.instance.leftScore / (ScoreDisplayTwoPlayers.instance.leftScore + ScoreDisplayTwoPlayers.instance.rightScore);
		string rate = Util.PercentageToGrade(percent);
		/*
		if (rate.Contains ("A") && GameState.CanPopRateBox()) {
			GoTo ("RateBoxTransition");
		} else {

		}
		*/
		CategoryController.instance.ShowNextQuiz ();
	}

	public void TryAgainButtonClicked(){
		UIStateController.ShowState ("QuestionPanel");
	}

	void Awake(){
		instance = this;
		AddTransition (new QuestionPanel.CorrectAnswerClickedTransition ());
		AddTransition (new QuestionPanel.IncorrectAnswerClickedTransition ());
		AddTransition (new QuestionPanel.ShowCorrrectAnswerTransition ());
		AddTransition (new QuestionPanel.FinishedTransition ());
		AddTransition (new QuestionPanel.RateBoxTransition ());
	}

	protected class CorrectAnswerClickedTransition : UITransition{
		public CorrectAnswerClickedTransition(){
			this.name = "CorrectAnswerClickedTransition";
			this.duration = 1f;
		}
		public override void Update ()
		{
		}

		public override void End ()
		{
			QuestionPanel.instance.ShowNextQuestion ();
		}
	}

	protected class IncorrectAnswerClickedTransition : UITransition{
		public IncorrectAnswerClickedTransition(){
			this.name = "IncorrectAnswerClickedTransition";
			this.duration = 1f;
		}
		public override void Update ()
		{
			
		}
		public override void End ()
		{
			QuestionPanel.instance.GoTo("ShowCorrrectAnswerTransition");
			QuestionPanel.instance.crossLeft.Hide ();
			QuestionPanel.instance.crossRight.Hide ();
		}

	}

	protected class ShowCorrrectAnswerTransition : UITransition{
		public ShowCorrrectAnswerTransition(){
			this.name = "ShowCorrrectAnswerTransition";
			this.duration = -1f;
		}

		public override void FirstUpdate(){
			QuestionPanel.instance.transform.FindChild ("Transition/WrongAnswerContinue").gameObject.SetActive (true);
			QuestionPanel.instance.textQuestion.ShowCorrectAnswer ();
		}

		public override void End ()
		{
			//QuestionPanel.instance.finishedPanel.SetActive (false);
		}
	}

	protected class FinishedTransition : UITransition{
		public FinishedTransition(){
			this.name = "FinishedTransition";
			this.duration = -1f;
		}

		public override void FirstUpdate(){
			Everyplay.StopRecording ();
			SoundManager.Play ("Success1");
			QuestionPanel.instance.finishedPanel.SetActive (true);
			string score = "" + ScoreDisplayTwoPlayers.instance.leftScore + "/"
			               + (ScoreDisplayTwoPlayers.instance.leftScore + ScoreDisplayTwoPlayers.instance.rightScore);
			QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Score").GetComponent<UILabel> ().text =score;
			float percent = 1f * ScoreDisplayTwoPlayers.instance.leftScore / (ScoreDisplayTwoPlayers.instance.leftScore + ScoreDisplayTwoPlayers.instance.rightScore);
			GameState.UpdateRecord (CategoryController.currentCategory1, CategoryController.currentCategory2, percent);

			string rate = Util.PercentageToGrade(percent);
			QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate").GetComponent<UILabel> ().text = rate;
			if (rate.Contains ("A") || rate.Contains ("B")) {
				QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate").GetComponent<UILabel> ().color = Color.green;
				QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate/Circle").gameObject.GetComponent<UI2DSprite>().color = Color.green;
			} else {
				QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate").GetComponent<UILabel> ().color = Color.red;
				QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate/Circle").gameObject.GetComponent<UI2DSprite>().color = Color.red;
			}

			QuestionPanel.instance.finishedPanel.transform.FindChild ("Frame/Rate").GetComponent<FlyInEffect> ().Trigger ();
		}

		public override void Update ()
		{
		}

		public override void End ()
		{
			QuestionPanel.instance.finishedPanel.SetActive (false);
		}
	}

	protected class RateBoxTransition : UITransition{
		public RateBoxTransition(){
			this.name = "RateBoxTransition";
			this.duration = -1f;
		}

		public override void FirstUpdate(){
			QuestionPanel.instance.rateBoxPanel.SetActive (true);
			GameState.PopRateBox ();
		}

		public override void Update ()
		{
		}

		public override void End ()
		{
			QuestionPanel.instance.rateBoxPanel.SetActive (false);
		}
	}

	int currentQuestionIndex=0;
	private string category1;
	private string subset;
	private string questionType;
	private List<Dictionary<string,object>> data;
	List<List<string>> problemSet;
	public void GenerateProblemSet(){
		currentQuestionIndex = -1;
		problemSet = new List<List<string>> ();
		if (questionType == "Separable") {
			for (int i = 0; i < data.Count; i++) {
				if (category1.Equals (data [i] ["level"].ToString ()) 
					&& subset.Equals (data [i] ["subset"].ToString ())) {
					List<string> newQuestion = new List<string> ();
					newQuestion.Add (data [i] ["word"].ToString ());
					newQuestion.Add (data [i] ["separable"].ToString ());
					problemSet.Add (newQuestion);
				}
			}
		} else if (questionType == "Definition") {
			for (int i = 0; i < data.Count; i++) {
				if (category1.Equals (data [i] ["level"].ToString ())
					&& subset.Equals (data [i] ["subset"].ToString ())) {
					List<string> newQuestion = new List<string> ();
					newQuestion.Add (data [i] ["word"].ToString ());
					newQuestion.Add (data [i] ["definition"].ToString ());
					problemSet.Add (newQuestion);
				}
			}
		}
	}


}