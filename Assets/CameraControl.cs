using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Ball ball;
	private Vector3 offset;
	private GameObject pins;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0,25,-150);
		pins = GameObject.Find ("Pins");
	}
	
	// Update is called once per frame
	void Update () {
		//if ball reaches the pins stop the camera from following it
		if (ball.transform.position.z <= pins.transform.position.z) {
			this.transform.position = ball.transform.position + offset;
		} else {
			print ("stop camera");
		}
	}
}
