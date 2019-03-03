using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player = Player;

//using UnityEditor.SceneManagement;

public class GameControl : MonoBehaviour {

	public static GameControl instance; // so we can access our game control script statically

	// Use this for initialization
	void Awake () {

		//livesText.text = playerStats.Lives.ToString ();
		player = GameObject.Find("Player");


		playerStartPosition = player.transform.position;


		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}


	private static int _remainingLives = 3; //public int lives = 3;
	public static int RemainingLives
	{
		get {return _remainingLives; }
	}


	public Text scoreText;
	public Text upgradeScoreText;
	public Text livesText;
	public InputField playerName;
	public GameObject player;

	public Transform playerPrefab;
	public Transform spawnPoint;
	public Wave remoteWave;

	public AudioSource mainMusic;
	public AudioSource discoMusic; 

	public int score = 0;
	public int spawnDelay = 2;
	public Transform spawnPrefab;
	[SerializeField] private GameObject GameOverUI;
	[SerializeField] private GameObject GameOverBG;
	[SerializeField] private GameObject TryAgainUI;
	[SerializeField] private GameObject EnterInitialsUI;


	[SerializeField] private int startingLives = 3;
	private bool partyEnabled = false;
	public bool gameOver = false;

	public GameObject discoOverlay;

	// call in the audio manager C# file we created so we can use PlaySound()
	private AudioManager audioManager;

	public Vector3 playerStartPosition;

	void Start () {

		_remainingLives = startingLives;

		// since this is a static variable, we need to specify the class instead of saying instance.property
		livesText.text = GameControl.RemainingLives.ToString ();
		// set the GM audiomanager to the one we initiated in the audiomanager file.
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No Audio Manager in this scene. Add one please!");
		}
			
	}



	// Update is called once per frame
	void Update () {

	}

	//public void DamagePlayer(int damage) {
	//	playerStats.Health -= damage;
//		if (playerStats.Health <= 0) {
//			Debug.Log ("Kill Player");
//		}
//	}

	public static void KillPlayer(Player player){
		Destroy (player.gameObject);
		_remainingLives -= 1 ;
		instance.livesText.text = GameControl.RemainingLives.ToString ();

		if (_remainingLives <= 0) {
			instance.GameOver ();
		} 

		else {
			instance.TryAgain ();
			//PlayerPlayerStats.FastSecondsLeft = 0;
			instance.StartCoroutine (instance.RespawnPlayer ());
		}
		

	}

	public void TryAgain(){
		TryAgainUI.SetActive (true);
	}

	public void GameOver()
	{	
		
		Debug.Log ("Game Over");
		//Set the game to be over.
		gameOver = true;

		StartCoroutine (StopTime());
		// fade out the music
		StartCoroutine (AudioFadeOut.FadeOut (mainMusic, 4f));
		GameOverBG.SetActive (true);

	}

	public IEnumerator StopTime(){
		yield return new WaitForSeconds (4.0f);
		//Time.timeScale = 0.0f;
		GameObject machine = GameObject.FindGameObjectWithTag("machine");
		Destroy (machine.gameObject);


		if (GetComponent<LeaderBoard> ().CheckForHighScore (score) == true) {
			EnterInitialsUI.SetActive (true);
			// we need to enter initials and update leaderboard	
		}
		else {
			//Activate the game over text.
			GameOverUI.SetActive (true);

		}
			
	}
		

	public void InitialsEntered() {

		GetComponent<LeaderBoard> ().EnterHighScore (score, playerName.text);
		EnterInitialsUI.SetActive (false);
		//Activate the game over text.
		GameOverUI.SetActive (true);
		//Set the game to be over.
		gameOver = true;
		
	}

	public IEnumerator RespawnPlayer() {
		audioManager.PlaySound ("respawn");

		yield return new WaitForSeconds (spawnDelay);
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		// spawn background slash effect
		Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		// remove try again overlay
		TryAgainUI.SetActive (false);
		//remove object from stack
		Destroy (clone.gameObject, 3f);

	}
		


	public void JellyScored()
	{
		//Jelly can't score if the game is over
		if (gameOver)   
			return;
		//If the game is not over, increase the score...
		score++;
		//...and adjust the score text.
		scoreText.text = score.ToString();
		upgradeScoreText.text = score.ToString();
		// give extra life at 100.
		/*if (score == 100) {
			_remainingLives += 1;
			instance.livesText.text = GameControl.RemainingLives.ToString ();
			audioManager.PlaySound ("1up");
			score = 0;
			scoreText.text = score.ToString();
		}
		*/

	}

	public void PartyTime() {
		if (partyEnabled == false) {
			partyEnabled = true;
			StartCoroutine (PartyRoutine ());

		}
	}

	IEnumerator PartyRoutine() {
		SpawnItems.instance.PartySpawn();
		discoOverlay.gameObject.SetActive (true);
		mainMusic.Pause ();
		discoMusic.Play ();

		yield return new WaitForSeconds (2);
		SpawnItems.instance.PartySpawn();

		yield return new WaitForSeconds (2);
		SpawnItems.instance.PartySpawn();

		yield return new WaitForSeconds (2);
		SpawnItems.instance.PartySpawn();

		yield return new WaitForSeconds (6);



		discoOverlay.gameObject.SetActive (false);
		mainMusic.Play ();
		discoMusic.Pause ();
		partyEnabled = false;

		//int spawndelay=Wave.delay;
		//SpawnItems.instance.wave.delay = .01f;
		//	SpawnScript = GetComponent <SpawnItems> ();
		//	SpawnScript.delay = .01f;
		
	}


	public void JellyChomp()
	{
		//AudioSource audio = GetComponent<AudioSource> ();
		//audio.Play ();
	}

	public void LoadScene(string sceneName) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
	}

	public static class AudioFadeOut {

		public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
			float startVolume = audioSource.volume;
			Debug.Log ("got into fader");

			while (audioSource.volume > 0) {
				audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

				yield return null;
			}

			audioSource.Stop ();
			audioSource.volume = startVolume;
		}

	}
}