﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

	public void Quit() {
		Application.Quit();
		Debug.Log ("Application Quit");
	}

	public void Retry() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
