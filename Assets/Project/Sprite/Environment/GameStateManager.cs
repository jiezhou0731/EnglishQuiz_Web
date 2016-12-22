using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameStateManager : MonoBehaviour {

	void Awake () {
		GameStateManager.Load ();
	}

	void Start(){
	}
	// Update is called once per frame
	void Update () {
	}

	// Called whenever GameState value changes
	public static void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGame1.gd"); //you can call it anything you want
		bf.Serialize(file, GameState.current);
		file.Close();
	}   

	// Called only on the scene start
	public static void Load() {
		if (File.Exists (Application.persistentDataPath + "/savedGame1.gd")) {
			try {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/savedGame1.gd", FileMode.Open);
				GameState.current = (GameState)bf.Deserialize (file);
				file.Close ();
			} catch (System.Exception e){
				GameState.current = new GameState ();
				Save ();
			}
		} else {
			GameState.current = new GameState ();
			Save ();
		}
	}
}