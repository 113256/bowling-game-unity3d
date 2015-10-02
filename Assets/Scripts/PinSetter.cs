using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	private int standingPins = 10;
	public Text standingpinsText;
	public bool ballEntered = false;
	//private bool AllSettled = true;
	private float lastChangeTime;

	public Animator anim; 

	//standing count = no. standing pins
	//if the last standing count == current standing count after 3 seconds, all pins have settled.
	public int lastStandingCount = -1; //-1 because nothing has fallen over yet

	public GameObject fullPinSetup;



	GameObject PinsParent;


	private ActionMaster actionMaster;
	private Ball ball;
	public bool pinsSettled = false;


	// Use this for initialization
	void Start () {
		PinsParent = GameObject.Find ("Pins");
		anim = GetComponent<Animator> ();
		actionMaster = GameObject.FindObjectOfType<ActionMaster> ();
		ball = GameObject.FindObjectOfType<Ball> ();
	}

	void raisePins(){
		//raising = true;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if(pin.isStanding()){
				pin.anim.SetTrigger("Raised");}
			}

	}

	void finishedSwipe(){
		anim.SetTrigger ("FinishedSwipe");
	}

	//used for reset methods to delete all pins before renewing
	void deletePins(){
		//GameObject pinsObject = GameObject.Find ("Pins");
		//Destroy (pinsObject);
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			Destroy(pin);
		}
	}

	void renewPins(){
		print ("renewing");
		GameObject pins = Instantiate (fullPinSetup) as GameObject;
		//pins.transform.name = "Pins";
		Pin[] allChildPins = pins.GetComponentsInChildren<Pin> ();
		//Pin[] allChildPins = fullPinSetup.GetComponentsInChildren<Pin> ();
		foreach(Pin pin in allChildPins){
			pin.transform.parent = PinsParent.transform;
			//attach animator again as there will be an error saying variable not attached...
			pin.attachAnimatorAndRigidbody();
			pin.myrigidbody.isKinematic = true;
			pin.anim.SetTrigger("RenewFall");

		}
		standingPins = 10;
	}

	void lowerPins(){
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.anim.SetTrigger("Lowered");
		}
		ball.notRolling ();
	}

	// Update is called once per frame
	void Update () {
		if (ballEntered && !pinsSettled ) {
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
		pinsSettled = true;
		standingpinsText.color = Color.green;

		ActionMaster.Action pinSetterAction = actionMaster.Bowl (10 - standingPins);

		if (pinSetterAction .Equals( ActionMaster.Action.Reset)) {
			anim.SetTrigger("ResetTrigger");
		} else if (pinSetterAction .Equals( ActionMaster.Action.Tidy)) {
			anim.SetTrigger("TidyTrigger");
		} 

		ball.ResetBall ();


	}

	public int getStandingPins (){
		return standingPins;
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
