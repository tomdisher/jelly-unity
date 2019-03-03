using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;



public class MenuManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		bool coin = CrossPlatformInputManager.GetButton("Coin");
		if (coin) {
			Play();
		}
	}

	public void Play () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Quit() {
		Application.Quit();
		Debug.Log ("Application Quit");
	}

}
