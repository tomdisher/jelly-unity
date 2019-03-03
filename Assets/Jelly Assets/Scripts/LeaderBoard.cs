using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour {

	public Text[] highScores;
	int [] highScoreValues;
	string [] highScoreNames;

	// Use this for initialization
	void Start () {
		highScoreValues = new int[highScores.Length];
		highScoreNames = new string[highScores.Length];
		for (int x = 0; x < highScores.Length; x++) {
			// adding all into a string, playerprefs can only store ints, not arrays
			highScoreValues [x] = PlayerPrefs.GetInt ("highScoreValues" + x);
			highScoreNames [x] = PlayerPrefs.GetString ("highScoreNames" + x);


		}

		DrawScores ();
		
	}

	void SaveScores() { 
		for (int x = 0; x < highScores.Length; x++) {
			// adding all into a string, playerprefs can only store ints, not arrays
			PlayerPrefs.SetInt ("highScoreValues" + x, highScoreValues[x]);
			PlayerPrefs.SetString ("highScoreNames" + x, highScoreNames[x]);

		}
	}

	public bool CheckForHighScore(int _value) {
		for (int x = 0; x < highScores.Length; x++) {
			// new value is bigger
			if (_value > highScoreValues [x]) {
				return true;
			}
		}
		return false;
	}

	public void EnterHighScore(int _value, string _userName) {
		for (int x = 0; x < highScores.Length; x++) {
			// new value is bigger
			if (_value > highScoreValues [x]) {
				for (int y = highScoreValues.Length - 1; y > x; y--) {
					highScoreValues [y] = highScoreValues [y - 1];
					highScoreNames [y] = highScoreNames [y - 1];

				}
				highScoreValues [x] = _value;
				highScoreNames [x] = _userName;
				DrawScores ();
				SaveScores ();
				break;
			}
		}
	}
	
	void DrawScores() {
		for (int x = 0; x < highScores.Length; x++) {
			
			highScores [x].text = highScoreNames[x] + " -- -- -- -- " + highScoreValues [x].ToString ("D4");

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
