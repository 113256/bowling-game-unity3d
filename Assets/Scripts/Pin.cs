using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standingThreshold = 3f;
	private Rigidbody myrigidbody;

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


	void OnTriggerExit(Collider collider){
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
			myrigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//isStanding ();
	}
}
