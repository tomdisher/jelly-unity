using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using player = Player;


public class Player : MonoBehaviour {
	[System.Serializable]
	public class PlayerStats {
		public int Health = 100;
		public int Lives = 3;
		public bool Big = false;
		public bool Fast = false;
		public float BigSecondsLeft = 0f;
		public float FastSecondsLeft = 0f;

	}

	[SerializeField] private GameObject [] RocketSocks;

	// instantiate the class we defined above so we can use it. 
	public PlayerStats playerStats = new PlayerStats();
	public Animator m_Anim;            // Reference to the player's animator component.



	void Awake() {
		m_Anim = GetComponent<Animator>();
	}

	public void DamagePlayer(int damage) {
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			GameControl.KillPlayer(this);
		}
	}

	public void MakeBig(int seconds)
	{	
		m_Anim.SetTrigger ("Spin360");
		m_Anim.SetTrigger ("Grow");
		playerStats.Big = true;
		transform.localScale = new Vector3 (transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z * 2);
		playerStats.BigSecondsLeft = seconds;

	}

	public void MakeFast(int seconds) {
		for(int i = 0; i < RocketSocks.Length; i++){
			if (!RocketSocks[i].activeSelf){
				RocketSocks[i].SetActive(true);
			}
		 }
		playerStats.Fast = true;
		m_Anim.SetBool ("Rockets", true);
		PlatformerCharacter2D.m_MaxSpeed = PlatformerCharacter2D.m_MaxSpeed + 20f;
		playerStats.FastSecondsLeft = seconds;
		Time.timeScale = 1.8f;
		AudioSource audio = GameControl.instance.mainMusic;
		audio.pitch = 1.6f;


	}


	void Update () {

		if (transform.position.y <= -20) {
			DamagePlayer (99999);
		}
		// if there are more than 0 bigseconds left, decrease the time passed
		if (playerStats.BigSecondsLeft > 0) {
			playerStats.BigSecondsLeft -= Time.deltaTime;
		}

		if (playerStats.BigSecondsLeft <= 0 && playerStats.Big == true) {
			// shrink the character
			transform.localScale = new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
			// set big to false, so we can make someone big again
			playerStats.Big = false;
			// make sure that big seconds are reset to zero, as they may be negative right now
			playerStats.BigSecondsLeft = 0;

		}

		// if there are more than 0 bigseconds left, decrease the time passed
		if (playerStats.FastSecondsLeft > 0) {
			playerStats.FastSecondsLeft -= Time.deltaTime;
		}

		if (playerStats.FastSecondsLeft <= 0 && playerStats.Fast == true) {
			// put back down to normal speed
			PlatformerCharacter2D.m_MaxSpeed = 10f;
			// set fast to false, so we can make someone fast again
			playerStats.Fast = false;
			m_Anim.SetBool ("Rockets", false);
			Time.timeScale = 1.0f;
			AudioSource audio = GameControl.instance.mainMusic;
			audio.pitch = 1.0f;

			// remove the socks items from the character
			for(int i = 0; i < RocketSocks.Length; i++){
				if (RocketSocks[i].activeSelf){
					RocketSocks[i].SetActive(false);
				}
			}
			// make sure that fast seconds are reset to zero, as they may be negative right now
			playerStats.FastSecondsLeft = 0;

		}
				
	}
}
