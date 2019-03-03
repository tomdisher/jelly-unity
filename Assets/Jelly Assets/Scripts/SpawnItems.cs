using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave {
	public string name;
	public int count = 100;
	public float delay = 0.4f;
	//public GameObject[] item;

	[System.Serializable]
	public class ItemEntry
	{
		public GameObject item;
		public int probability;
	}

	public ItemEntry[] items;    
}

public class SpawnItems : MonoBehaviour {

	public static SpawnItems instance; // so we can access our script statically

	public Wave[] waves;
	private int nextWave = 0;
	public float timeBetweenWaves = 5f;
	public float waveCountdown = 0f;
	public enum SpawnState {SPAWNING, WAITING ,COUNTING};

	private SpawnState state = SpawnState.COUNTING;
	public GameObject banana;
	public GameObject big;
	public GameObject spring;
	public GameObject rocketSocks;
	public GameObject janitor;
	private GameObject fallingItem;
	public Text countdownText;
	public Text waveText;

	public Text waveComplete;
	public Text waveAnyKey;
	public Text waveNumber;
	public GameObject wavePanel;

	public GameObject SpringHelpText;
	public GameObject RocketSocksHelpText;
	public GameObject JanitorHelpText;


	public Transform itemSpawnPoint;
	private float xRange;
	public float startPctItems = 1f; // store the % of items left in the wave.
	[Header("Optional")]
	[SerializeField]
	private StatusIndicator statusIndicator;

	// Use this for initialization
	void Awake () {

		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}


	public float WaveCountdown {
		get { return waveCountdown; }
	}
	// start the search countdown at one second since find tag is so expensive to run.
	float searchCountdown = 1f;

	// Use this for initialization
	void Start () {

		waveNumber.text = (nextWave+1).ToString();

		countdownText.gameObject.SetActive (true);
		waveText.gameObject.SetActive (true);
		waveNumber.gameObject.SetActive (true);

		waveCountdown = 5f;
		if (statusIndicator == null) {
			
		}

		//InvokeRepeating ("Spawn", delay, delay);
	}
	/*
	void Spawn(){
		
		int randomNumber = Random.Range(1, 100);
		if (randomNumber == 1) {
			fallingItem = big;
		} else if (randomNumber >= 2 && randomNumber <= 10)
			fallingItem = spring;
		else if (randomNumber >= 11 && randomNumber <= 15)
			fallingItem = janitor;
		else {
			fallingItem = banana;
		}

		GameObject obj = Instantiate (fallingItem, itemSpawnPoint.position, itemSpawnPoint.rotation);

		xRange = Random.Range (itemSpawnPoint.position.x * -1 * .1f, itemSpawnPoint.position.x * -1 * .5f);

		obj.GetComponent<Rigidbody2D>().velocity = new Vector3 (xRange, Random.Range(18f,20f), 0);


	}
	*/
	
	// Update is called once per frame
	void Update () {

		countdownText.text = waveCountdown.ToString();


		//@todo all the upgrade stuff is disabled for now and will show tooltips instead. 
		if (state == SpawnState.COUNTING) {
			//pause the game until a key is presses, so we can explain the upgrades/new rules.
			//if (Time.timeScale == 0.0f) {

			waveNumber.text = (nextWave+1).ToString();

				//wavePanel.gameObject.SetActive (true);
				//SpringHelpText.gameObject.SetActive (true);
				/*
				switch (nextWave) {
					case 1:
						SpringHelpText.gameObject.SetActive (true);
						break;
					case 2:
						RocketSocksHelpText.gameObject.SetActive (true);
						break;
					case 3:
						JanitorHelpText.gameObject.SetActive (true);
						break;
				}


								
				if (Input.GetKey(KeyCode.Return)) {
					Time.timeScale = 1.0f;
				}

			}

			//after time resumes, start the next wave countdown
			if (Time.timeScale > 0.0f) {

				//when gameplay resumes, remove UI elements

				wavePanel.gameObject.SetActive (false);

				switch (nextWave) {
				case 1:
					SpringHelpText.gameObject.SetActive (false);
					break;
				case 2:
					RocketSocksHelpText.gameObject.SetActive (false);
					break;
				case 3:
					JanitorHelpText.gameObject.SetActive (false);
					break;
				}
					
			}
			*/
		}


		countdownText.text = ((int)waveCountdown + 1).ToString ();

		if (state == SpawnState.WAITING) {
			// check if items are still on the screen
			if (!ItemsOnScreen ()) {
				// begin a new round
				WaveCompleted();
			} 

			else {
				// still items on the screen, don't start a new round until they are cleared.
				return;
			}
		}
		if (waveCountdown <= 0 && state != SpawnState.SPAWNING) {
			StartCoroutine(SpawnWave(waves[nextWave]));
			
		} 
		else {
			waveCountdown -= Time.deltaTime;
			
		}
	}

	bool ItemsOnScreen() {
		// check once per second if there is still an item on the screen.
		if (searchCountdown <= 0f) {
			// reset the timer
			searchCountdown = 1f;
			// try to find an item on the screen, if there is no item return false.
			if (GameObject.FindGameObjectWithTag ("Item") == null) {
				return false;
			}
		}
		searchCountdown -= Time.deltaTime;
		return true;
	}

	void WaveCompleted(){
		Debug.Log ("Wave Completed!");
		countdownText.gameObject.SetActive (true);
		waveText.gameObject.SetActive (true);
		waveNumber.gameObject.SetActive (true);





		//Time.timeScale = 0.0f;

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			Debug.Log ("All the waves are completed. Starting at beginning.");
		} 
		else {
			nextWave++;
		}
	}

	IEnumerator SpawnWave(Wave _wave) {
		state = SpawnState.SPAWNING;
		Debug.Log ("We are spawning.");
		countdownText.gameObject.SetActive (false);
		waveText.gameObject.SetActive (false);
		waveNumber.gameObject.SetActive (false);

		// loop the number of times and spawn items.
		for (int i = 0; i < _wave.count; i++) {
			//find out which item we should spawn.
			int keyToSpawn = getItem(_wave); // get an item that is available in the current wave
			SpawnItem (_wave.items[keyToSpawn].item);
			statusIndicator.SetItemCount (_wave.count-i-1, _wave.count);
			yield return new WaitForSeconds (_wave.delay);
		}
		state = SpawnState.WAITING;
		yield break;
	}

	int getItem(Wave _wave){
		if (_wave.items.Length == 1) // if there is only one item in the array there is no need to calculate.
			return 0;
		
		int totalProbability = 0; // 0
		int[] range = new int[_wave.items.Length]; // will store the range of each item
		// loop through each item available inside this wave
		for (int i = 0; i < _wave.items.Length; i++) {
			// calculate the total probability
			totalProbability += _wave.items[i].probability;
			if (i == 0)
				range[i] = _wave.items[i].probability;
			else
				range[i] = range[i-1] + _wave.items[i].probability;
		}
		int randomNumber = Random.Range(1, totalProbability);
		// if we're less than the first second item, we'll assume that it is the first item, so return array point 0.
		if (randomNumber <= range[0]) {			
			return 0;
		}
		// if we're greater than the second to last item, we'll assume it is the last item, so return the last point in the array. Taking away 2 since arrays are zero based and length is one based.
		if (randomNumber > range[_wave.items.Length - 2]) {
			return _wave.items.Length - 1;
		}
		// if we arent on the first or last item we're going to have to loop through and see what two numbers we are between.
		// we can start at the array position 1 since we already bypassed 0.
		for (int i = 1; i < _wave.items.Length; i++) {
			if (randomNumber > range[i - 1] && randomNumber < range[i + 1]) {
				return i;
			}
		}

		Debug.Log ("returning line 176. how did I get here?");
		return 0; // we should never get here.
			

	}
		
	void SpawnItem(GameObject _item) {
		
		GameObject obj = Instantiate (_item, itemSpawnPoint.position, itemSpawnPoint.rotation);
		xRange = Random.Range (itemSpawnPoint.position.x * -1 * .1f, itemSpawnPoint.position.x * -1 * .5f);
		obj.GetComponent<Rigidbody2D>().velocity = new Vector3 (xRange, Random.Range(18f,20f), 0);

	}

	public void PartySpawn() {
		for (int i = 1; i < 35; i++) {
			SpawnItem (banana);
		}
	}

}
