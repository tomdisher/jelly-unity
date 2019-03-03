using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Restart ());
	}
		

	IEnumerator Restart() {
		yield return new WaitForSeconds (6);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
