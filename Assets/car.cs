using UnityEngine;
using System.Collections;
public class car : MonoBehaviour
{
	public Transform farEnd;
	private Vector3 frometh;
	private Vector3 untoeth;
	private float secondsForOneLength = 55f;
	private bool m_FacingRight = false;  // For determining which way the player is currently facing.
	private Vector3 oldPosition;
	public Transform itemSpawnPoint;
	private float xRange;
	public int i = 0; // to control trigger for collision, had a problem with it throwing two powerups

	[System.Serializable]
	public class ItemEntry
	{
		public GameObject item;
		public int probability;
	}

	public ItemEntry[] items;   

	void Start()
	{
		frometh = transform.position;
		untoeth = farEnd.position;
	}

	void FixedUpdate()
	{

		transform.position = Vector3.Lerp(frometh, untoeth,
			Mathf.SmoothStep(0f,1f,
				Mathf.PingPong(Time.time/secondsForOneLength, 1f)
			) );
		
		if (oldPosition.x < transform.position.x && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (oldPosition.x > transform.position.x && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

		oldPosition = transform.position;
	}

	private void Flip()
	{
		i = 0; // allow the car to throw more updates once it turns around.
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}



/*	int getItem()
	{
		// get the powerup that we should spawn
	}
*/
	void SpawnItem(GameObject _item) {
		// spawn the item
		GameObject obj = Instantiate (_item, itemSpawnPoint.position, itemSpawnPoint.rotation);
		xRange = Random.Range (itemSpawnPoint.position.x * -1 * .1f, itemSpawnPoint.position.x * -1 * .5f);
		obj.GetComponent<Rigidbody2D>().velocity = new Vector3 (xRange, Random.Range(18f,20f), 0);

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag ("Player") && i == 0) {
			
			SpawnItem (items[getItem()].item);
			
			//AudioManager.instance.PlaySound("chomp");
			//gameObject.GetComponent<Renderer>().enabled = false;
			//GameControl.instance.JellyScored ();
			//GameControl.instance.PartyTime ();
			//Destroy(gameObject);
			i = 1;




		}
	}

	int getItem(){
		if (items.Length == 1) // if there is only one item in the array there is no need to calculate.
			return 0;

		int totalProbability = 0; // 0
		int[] range = new int[items.Length]; // will store the range of each item
		// loop through each item available inside this wave
		for (int i = 0; i < items.Length; i++) {
			// calculate the total probability
			totalProbability += items[i].probability;
			if (i == 0)
				range[i] = items[i].probability;
			else
				range[i] = range[i-1] + items[i].probability;
		}
		int randomNumber = Random.Range(1, totalProbability);
		// if we're less than the first second item, we'll assume that it is the first item, so return array point 0.
		if (randomNumber <= range[0]) {			
			return 0;
		}
		// if we're greater than the second to last item, we'll assume it is the last item, so return the last point in the array. Taking away 2 since arrays are zero based and length is one based.
		if (randomNumber > range[items.Length - 2]) {
			return items.Length - 1;
		}
		// if we arent on the first or last item we're going to have to loop through and see what two numbers we are between.
		// we can start at the array position 1 since we already bypassed 0.
		for (int i = 1; i < items.Length; i++) {
			if (randomNumber > range[i - 1] && randomNumber < range[i + 1]) {
				return i;
			}
		}

		Debug.Log ("returning line 176. how did I get here?");
		return 0; // we should never get here.


	}
}