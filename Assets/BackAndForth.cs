using UnityEngine;
using System.Collections;

public class BackAndForth : MonoBehaviour
{
	public enum OccilationFuntion { Sine, Cosine }

	public void Start ()
	{
		//to start at zero
		//StartCoroutine (Oscillate (OccilationFuntion.Sine, 1f));
		//to start at scalar value
		StartCoroutine (Oscillate (OccilationFuntion.Cosine, 3f));
	}

	private IEnumerator Oscillate (OccilationFuntion method, float scalar)
	{
		while (true)
		{
			if (method == OccilationFuntion.Sine)
			{
				transform.position = new Vector3 (Mathf.Sin (Time.time) * scalar, 5, 0);
			}
			else if (method == OccilationFuntion.Cosine)
			{
				transform.position = new Vector3(Mathf.Cos(Time.time)  * scalar, 5, 0);
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}