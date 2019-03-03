using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSockEffects : MonoBehaviour {

	public Transform itemSpawnPoint;
	public GameObject item;
	//private float xRange;


/*	// Use this for initialization
	void Start () {
		
		
	}

	void SpawnItem(GameObject _item) {

		GameObject obj = Instantiate (_item, itemSpawnPoint.position, itemSpawnPoint.rotation);
		xRange = Random.Range (itemSpawnPoint.position.x * -1 * .1f, itemSpawnPoint.position.x * -1 * .5f);
		obj.GetComponent<Rigidbody2D>().velocity = new Vector3 (xRange, Random.Range(18f,20f), 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
*/



	void Start()
	{
		StartCoroutine(Spawn1());
		Debug.Log ("got into start");
	}

	IEnumerator Spawn1()
	{
		yield return new WaitForSeconds(1);
		//change new Vector3(XXX, XXX, XXX) with wherever you're spawning
	Instantiate(item, itemSpawnPoint.position, itemSpawnPoint.rotation);
		StartCoroutine(Spawn2());
		Debug.Log ("got into spawn1");
	}

	IEnumerator Spawn2()
	{
		yield return new WaitForSeconds (1);
		//change new Vector3(XXX, XXX, XXX) with wherever you're spawning
	Instantiate (item, itemSpawnPoint.position, itemSpawnPoint.rotation);
		StartCoroutine (Spawn1 ());
	}
}