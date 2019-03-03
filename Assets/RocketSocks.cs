using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSocks : MonoBehaviour {

	public int i = 0; // try to get it to only trigger once.

	public float fallSpeed = 3.0f;
	public float spinSpeed = 25.0f;
	private bool StopSpin = false;


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

		if (StopSpin == true && spinSpeed > 0f) {
			spinSpeed -= (Time.deltaTime * 1000);
		}
	}


	private void OnTriggerExit2D (Collider2D other)
	{
		
	}

	private void OnTriggerEnter2D (Collider2D other){
		Player _player = other.GetComponent<Collider2D>().GetComponent<Player> ();
		if(_player != null)
		{
			if (this.enabled)
			{
				this.enabled = false;
				_player.MakeFast (20);
				gameObject.GetComponent<Renderer>().enabled = false;
				Destroy(gameObject);
			}
		}

		if(other.gameObject.CompareTag ("Ground")){
			StopSpin = true;

		}

	}
}