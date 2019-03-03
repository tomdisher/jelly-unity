using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class InputInitials : MonoBehaviour {
	private string initials = "";
	private string nextButton = "Jump";
	private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-?!+*(=) 1234567890";
	private int stepper = 0;
	private int letterSelect = 0;
	public Text[] Letters = null;
	public InputField fullName;
	public float moveDelay = .0f;
	private bool readyToMove = true;
	private Color selectedColor = Color.white;
	private GameObject nextText;
	private TextMesh fireText;

	void Start ()
	{
		Letters [letterSelect].text = alphabet [stepper].ToString ();
		Input.ResetInputAxes ();
		Cursor.visible = false;
		nextText = GameObject.Find ("button");

	}

	void Update ()
	{
		float direction = CrossPlatformInputManager.GetAxis ("Vertical");
		
		if (direction > 0 && readyToMove) {
			if (stepper < alphabet.Length - 1) {
				stepper++;
				Letters [letterSelect].text = alphabet [stepper].ToString ();
				print (Letters [letterSelect].text);
				readyToMove = false;
				Invoke ("ResetReadyToMove", moveDelay);
			}
			// if we're at the end of the stepper, go back to beginning
			if (stepper == alphabet.Length - 1) {
				stepper = -1;
			}
		}
	
		if (direction < 0 && readyToMove) {
			if (stepper > 0) {
				stepper--;
				Letters [letterSelect].text = alphabet [stepper].ToString ();
				readyToMove = false;
				Invoke ("ResetReadyToMove", moveDelay);
			}
			// if we're at the beginning of the stepper, go back to end
			if (stepper == 0) {
				stepper = alphabet.Length;
			}
		}
		if (Input.GetButton (nextButton) && readyToMove) {
			if (letterSelect <= Letters.Length - 1) {
				initials = initials + alphabet [stepper].ToString (); // add current letter to string
				// if the last letter is reached then add initials
				if (letterSelect == Letters.Length - 1) {
					letterSelect = 4; // breaks loop then sets name
					for (int i = 0; i < Letters.Length; i++) {
						fullName.text += Letters[i].text;
					}
					if (GameControl.instance) {
						GameControl.instance.InitialsEntered();
					}
				}
				// keep on till the very last letter
				if (letterSelect < Letters.Length - 1) {
					letterSelect++;
					//Letters [letterSelect].GetComponent<PulseColors> ().enabled = true;
					Letters [letterSelect].color = Color.white; // resets alpha
					//Letters [letterSelect - 1].GetComponent<PulseColors> ().enabled = false;
					Letters [letterSelect - 1].color = selectedColor;
					readyToMove = false;
					Invoke ("ResetReadyToMove", moveDelay);
				}
				stepper = 0; // stepper is reset for next run
			}
		}
	}

	void ResetReadyToMove ()
	{
		readyToMove = true;
	}
}
