using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MySceneManager : MonoBehaviour {

	ScreenFader fadeScr;

	void Awake()
	{
		fadeScr = GameObject.FindObjectOfType<ScreenFader>();
	}

	void Start () {
	}
		

	public void GoToJungle(){
		GoTo ("Jungle");
	}

	public void GoToStore(){
		GoTo ("Store");
	}

	public void GoToTutorial(){
		GoTo ("Tutorial");
	}

	public void GoTo(string sceneName){
		fadeScr.EndScene(sceneName);
	}
		
	public void DoNothing(){
	}

}
	