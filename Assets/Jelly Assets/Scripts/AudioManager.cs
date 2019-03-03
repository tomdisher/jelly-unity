using UnityEngine;

[System.Serializable]
public class Sound {
	public string name;
	public AudioClip clip;

	private AudioSource source;
	[Range(0f, 1f)]
	public float volume = 0.7f;
	[Range(0.5f, 2f)]
	public float pitch = 1f;

	[Range(0f, 0.5f)]
	public float randomPitch = 0f;
	[Range(0f, 0.5f)]
	public float randomVolume = 0f;

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
	}
	public void Play() {
		source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
		source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
		source.Play();
	}
}

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	[SerializeField] Sound[] sounds;

	void Awake () {
		if (instance != null) {
			Debug.LogError ("More than one Audio Manager in the scene!");
		}

		else {
			instance = this;
		}
	}


	// Use this for initialization
	void Start () {
		for (int i = 0; i < sounds.Length; i++) {

			// create a game object for each of the audio items in the above array
			GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			// set the parent to the _GM so the inspector stays clean
			_go.transform.SetParent(this.transform);
			//create an audio source for each of the game objects above and add the clip to it.
			sounds[i].SetSource (_go.AddComponent<AudioSource> ());


		}
		
	}

	public void PlaySound (string _name){
		//loop through each sound and see if we can find a sound with the name we've called.
		for (int i = 0; i < sounds.Length; i++) { 
			// if the name is the same, let's play it!
			if (sounds[i].name == _name) {
				sounds[i].Play();
				return;
			}
		}
		// uh oh, no sound with that name. Let's throw an error.
		Debug.LogError("Calling a sound that does not exist. Check Audio Manager list and see if the name you're calling is correct: " + _name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
