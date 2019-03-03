using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Big : MonoBehaviour {

	private AudioSource source;
	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;
	private bool StopSpin = false;



	void Awake () {
		source = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
		if (transform.position.y <= -20) {
			Destroy (this);
		}

		if (StopSpin == true && spinSpeed > 0f) {
			spinSpeed = 0f;
		}
	}


	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		Player _player = other.GetComponent<Collider2D>().GetComponent<Player> ();
		if(_player != null)
		{
			if (this.enabled)
			{
				this.enabled = false;
				source.Play();
				gameObject.GetComponent<Renderer>().enabled = false;
				_player.MakeBig (10);
				Destroy(gameObject, source.clip.length);



			}
		}
		if(other.gameObject.CompareTag ("Ground")){
			StopSpin = true;

		}

	}
}
