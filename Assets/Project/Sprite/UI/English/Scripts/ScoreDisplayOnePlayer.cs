using UnityEngine;
using System.Collections;

public class ScoreDisplayOnePlayer : MonoBehaviour {
	static public ScoreDisplayOnePlayer instance;
	public ShakingLabelDisplay left;
	private int _leftScore = 0;
	public float leftScore {
		get {
			return _leftScore;
		}    
	}


	// Use this for initialization
	void Start () {
		instance = this;
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseLeftScore(int value){
		_leftScore += value;
		left.SetLabel(_leftScore.ToString("F0"));
	}


	public void Reset(){
		_leftScore = 0;
		IncreaseLeftScore (0);
	}
}
