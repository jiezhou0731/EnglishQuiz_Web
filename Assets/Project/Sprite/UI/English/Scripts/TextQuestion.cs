using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextQuestion : MonoBehaviour {

	public static TextQuestion instance;
	public QuestionPanel questionPanel;
	public List<GameObject> questions;
	public List<GameObject> answers;

	private List<ButtonAppearEffect> buttons = new List<ButtonAppearEffect> ();

	void Start () {
		instance = this;
		foreach (Transform child in transform){
			try {
				buttons.Add (child.FindChild("Button").GetComponent<ButtonAppearEffect>());
			} catch(System.Exception e){

			}
		}

	}
		
	void Update () {

	}

	public void TriggerEffect(){

		for (var i = 0; i < buttons.Count; i++) {
			buttons [i].Trigger ();
		}
	}

	private int correctAnswerIndex=0;
	public void SetNewQuestion(string question, Dictionary<string, bool> answerDict){
		ChangeLabel (questions[0], question);
		ClearTipCrosses ();
		int index = 0;
		foreach (KeyValuePair<string,bool> item in answerDict) {
			answers[index].SetActive (true);
			ChangeColor (answers[index], new Color32(0x8C,0x6D,0xAD,0xFF));
			ChangeLabel (answers[index], item.Key);
			if (item.Value == true) {
				correctAnswerIndex = index ;
			}
			index += 1;
		}
		for (int i = index; i < 4; i++) {
			answers[i].SetActive (false);
		}
		TriggerEffect ();
	}

	private void ChangeLabel(GameObject component, string label){
		component.transform.FindChild ("Button/Label").GetComponent<UILabel> ().text = label;
	}

	private void ChangeColor(GameObject component, Color color){
		Transform background = component.transform.FindChild ("Button/Background");
		foreach (Transform child in background) {
			child.GetComponent<UI2DSprite> ().color = color;
		}
	}

	public void TipCrossTwo(){
		int number = 2;
		int counter = 0;
		for (int i = 0; i < answers.Count; i++) {
			if (i != correctAnswerIndex) {
				DrawCrossToAnswer (i);
				counter++;
				if (counter == number) {
					break;
				}
			}
		}
	}
		
	private void DrawCrossToAnswer(int answerIndex){
		answers [answerIndex].transform.Find ("Button/TipCross").GetComponent<FillEffect> ().Trigger ();
	}

	private void ClearTipCrosses(){
		for (int i = 0; i < answers.Count; i++) {
			answers [i].transform.Find ("Button/TipCross").GetComponent<FillEffect> ().Hide ();
		}
	}

	public void AnswerClicked(GameObject button){
		if (answers [correctAnswerIndex] == button) {
			ChangeColor (button, new Color32(0x8c,0xc6,0x3f,0xFF));
			questionPanel.CorrectAnswerClicked ();
		} else {
			ChangeColor (button, new Color32(0xda,0x32,0x1b,0xff));
			questionPanel.IncorrectAnswerClicked ();
		}
	}

	public void ShowCorrectAnswer(){
		ChangeColor (answers [correctAnswerIndex], new Color32(0x8c,0xc6,0x3f,0xFF));
	}

	static public void ShowTip(){
		instance.TipCrossTwo ();
	}
}