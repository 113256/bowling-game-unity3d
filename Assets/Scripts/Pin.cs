using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standingThreshold = 5f;
	public Rigidbody myrigidbody;
	private PinSetter pinSetter;
	public Animator anim; 

	//return true if pins tranform rotated less than threshold from vertical
	public bool isStanding(){
		Vector3 rotationInEuler = transform.rotation.eulerAngles;
		//degree of tilt (pin standing straight : rotation = 0, tilt : rotation != 0)
		float tiltInX = Mathf.Abs(rotationInEuler.x);
		float tiltInZ = Mathf.Abs (rotationInEuler.z);

		if (tiltInX < standingThreshold || tiltInZ < standingThreshold) {
			return true;
		} else {
			return false;
		}
	}

	void finishedRaise(){
		pinSetter.anim.SetTrigger ("FinishedRaise");
	}


	void OnTriggerExit(Collider collider){
		Destroy (gameObject);
	}

	public void attachAnimatorAndRigidbody(){
		anim = GetComponent<Animator> ();
		myrigidbody = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
			myrigidbody = gameObject.GetComponent<Rigidbody>();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
	}
	
	// Update is called once per frame
	void Update () {
		//isStanding ();
	}
}
