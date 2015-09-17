using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody ballRigidBody;
	public float launchSpeed;
	private AudioSource audioSource;
	private bool rolling = false;

	// Use this for initialization
	void Start () {
		ballRigidBody = this.GetComponent<Rigidbody> ();
		audioSource = this.GetComponent<AudioSource> ();




	}

	public void Launch(){
		ballRigidBody.velocity = new Vector3(0, 1f, launchSpeed);
		audioSource.Play ();
		rolling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(!rolling){
			Launch ();
			}
		}
	}
}
