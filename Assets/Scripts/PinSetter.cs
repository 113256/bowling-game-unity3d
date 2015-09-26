using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public int standingPins = 10;
	public Text standingpinsText;
	private bool ballEntered = false;
	//private bool AllSettled = true;
	private float lastChangeTime;

	//standing count = no. standing pins
	//if the last standing count == current standing count after 3 seconds, all pins have settled.
	private int lastStandingCount = -1; //-1 because nothing has fallen over yet

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ballEntered) {
			checkStanding ();
		}
		standingpinsText.text = standingPins.ToString();
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.GetComponent<Ball> ()) {
			print ("ball entered");
			ballEntered = true;
		}
	}

	private void pinsHaveSettled(){
		standingpinsText.color = Color.green;
	}

	void checkStanding(){
		int currentStandingCount = countStanding ();

		//if not equal (if there was a change in the number of upright pins)
		if (lastStandingCount != currentStandingCount) {
			lastChangeTime = Time.time;//update last changed time
			lastStandingCount = currentStandingCount;//update lastStanding count
			return;
		}
		//if there was no change after 3 seconds (difference between currenttime and lastChangeTim) deduce that pins have settled 
		float settleTime = 3f;
		if ((Time.time - lastChangeTime) > settleTime) {
			pinsHaveSettled();
		}
	}

	public int countStanding(){
		int pins = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if(pin.isStanding()){
				pins++;
			}

		}

		standingPins = pins;
		return pins;
	}
}
