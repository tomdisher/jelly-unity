using UnityEngine;
using System.Collections;
public class SlideBackFore : MonoBehaviour
{
	public Transform farEnd;
	private Vector3 frometh;
	private Vector3 untoeth;
	private float secondsForOneLength = 10f;

	void Start()
	{
		frometh = transform.position;
		untoeth = farEnd.position;
	}

	void Update()
	{
		
		transform.position = Vector3.Lerp(frometh, untoeth,
			Mathf.SmoothStep(0f,1f,
				Mathf.PingPong(Time.time/secondsForOneLength, 1f)
			) );
	}
}