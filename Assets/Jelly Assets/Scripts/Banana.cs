using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {

	public int i = 0; // try to get it to only trigger once.
	private AudioSource source;


	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;


	//private Rigidbody2D m_Rigidbody2D;


	// Use this for initialization
	void Awake () {

		//m_Rigidbody2D = GetComponent<Rigidbody2D>();

	}

	void Start () {
		/*int multiplier = 1;
		int posneg = Random.Range (0, 2);
		if (posneg < 1)
			multiplier = -1;
			
		m_Rigidbody2D.velocity = new Vector3 (Random.Range(.5f,5f) * multiplier, Random.Range(16f,18f), 0);
		*/
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
		if (transform.position.y <= -20) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") && i == 0) {
			AudioManager.instance.PlaySound("chomp");
			gameObject.GetComponent<Renderer>().enabled = false;
			GameControl.instance.JellyScored ();
			//GameControl.instance.PartyTime ();
			Destroy(gameObject);
			i = 1;




		}
	}
}