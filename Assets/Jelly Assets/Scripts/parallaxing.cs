using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour {

	public Transform[] backgrounds; // array of backgrounds and foregrounds that will parallax
	private float[] parallaxScales; // proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f; // how smooth will the parralax be? should be >0!

	private Transform cam; // ref to the main camera transform
	private Vector3 previousCamPos; // store the position of the camera in the previous frame

	// call awake before start
	void Awake () {
		// set up cam ref 
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start () {
		// store prev. frame
		previousCamPos = cam.position;

		// loop through the backgrounds list and assign a scale
		parallaxScales = new float[backgrounds.Length];

		//assign corresponding parallax scales
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < backgrounds.Length; i++) {
			
			// parallax is the opposite of camera movement
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			// set target X position, plus parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create target position which is the bg's current pos with its target X position
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current and target position using lerp
			backgrounds[i].position = Vector3.Lerp ( backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

		}

		// update cam position
		previousCamPos = cam.position;

	
	}
}
