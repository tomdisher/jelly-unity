using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor: MonoBehaviour {

	public int i = 0; // try to get it to only trigger once.

	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;


	// Use this for initialization
	void Awake () {


	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		//transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
		if (transform.position.y <= -20) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") && i == 0) {
			GameControl.instance.PartyTime ();
			gameObject.GetComponent<Renderer>().enabled = false;
			Destroy(gameObject);
			i = 1;



		}
	}
}