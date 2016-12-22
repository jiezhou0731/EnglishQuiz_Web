using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameState { 

	public static GameState current;
	public List<QuizRecord> records;
	protected bool tutorialCompleted=false;
	static long tickToSeconds = 10000000;

	protected long lastRateBoxPopTime=0; 
	protected bool rated = false;

	public GameState () {
		records = new List<QuizRecord> ();
	}

	public static void UpdateRecord(string category1, string category2, float percentage = 0){
		for (var i = 0; i < current.records.Count; i++) {
			if (current.records [i].category1 == category1 && current.records [i].category2 == category2) {
				current.records [i].UpdateRecord (percentage);
				return;
			}
		}
		// Not found
		current.records.Add(new QuizRecord( category1,  category2,  percentage));
	}


	public static float GetRecord(string category1, string category2 ){
		for (var i = 0; i < current.records.Count; i++) {
			if (current.records [i].category1 == category1 && current.records [i].category2 == category2) {
				return current.records [i].percentage;
			}
		}
		return -1f;
	}

	public static void TutorialComplete(){
		current.tutorialCompleted = true;
		GameStateManager.Save ();
	}

	public static bool GetTutorialComplete(){
		return current.tutorialCompleted;
	}

	static long popRateBoxInterval = 1209600;// 2 weeks
	public static bool CanPopRateBox(){
		// Already rated
		if (current.rated == true) {
			return false;
		}
		// Check if it has been 2 weeks since last pop.
		if (popRateBoxInterval + current.lastRateBoxPopTime -System.DateTime.Now.Ticks / tickToSeconds>-1){
			return false;
		} else {
			return true;
		}
	}

	public static void Rate(){
		current.rated = true;
		GameStateManager.Save ();
	}

	public static void PopRateBox(){
		current.lastRateBoxPopTime = System.DateTime.Now.Ticks/tickToSeconds;
		GameStateManager.Save ();
	}
}


[System.Serializable] 
public class QuizRecord {

	public string category1;

	public string category2;

	public float percentage = 0;

	public QuizRecord(string category1, string category2, float percentage = 0){
		this.category1 = category1;
		this.category2 = category2;
		this.percentage = percentage;
		GameStateManager.Save ();
	}

	public void UpdateRecord(float percentage){
		this.percentage = percentage;
		GameStateManager.Save ();
	}

}