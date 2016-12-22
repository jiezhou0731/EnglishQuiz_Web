using UnityEngine;
using System.Collections;

public class ScoreDisplayTwoPlayers : MonoBehaviour {
	static public ScoreDisplayTwoPlayers instance;
	public ShakingLabelDisplay left;
	public ShakingLabelDisplay right;
	private int _leftScore = 0;
	public float leftScore {
		get {
			return _leftScore;
		}    
	}

	private int _rightScore = 0;
	public float rightScore {
		get {
			return _rightScore;
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

	public void IncreaseRightScore(int value){
		_rightScore += value;
		right.SetLabel(_rightScore.ToString("F0"));
	}


	public void Reset(){
		_leftScore = 0;
		_rightScore = 0;
		IncreaseLeftScore (0);
		IncreaseRightScore (0);
	}
}
