﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody ballRigidBody;
	private AudioSource audioSource;
	private bool rolling = false;
	private Vector3 defaultPos;

	private ActionMaster actionMaster;
	private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
		ballRigidBody = this.GetComponent<Rigidbody> ();
		//disable gravity before launching ball so it can float in the air
		ballRigidBody.useGravity = false;

		audioSource = this.GetComponent<AudioSource> ();
		defaultPos = this.transform.position;
		actionMaster = GameObject.FindObjectOfType<ActionMaster> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
	}

	public void ResetBall(){
		transform.position = defaultPos;
		ballRigidBody.velocity = Vector3.zero;
		ballRigidBody.angularVelocity = Vector3.zero;
		//pinSetter.pinsSettled = false;
		pinSetter.ballEntered = false;
	}
	

	public void moveStart(float amount){
		if (!rolling) {
			print ("nudge");
			//this.transform.position = new Vector3 (this.transform.position.x + amount, this.transform.position.y, this.transform.position.z);
			transform.Translate(new Vector3(amount, 0, 0));
			transform.position = new Vector3( Mathf.Clamp(transform.position.x, -40, 40), transform.position.y, transform.position.z);
		}
	}

	public void Launch(Vector3 velocity){
		ballRigidBody.useGravity = true;
		ballRigidBody.velocity = velocity;
		audioSource.Play ();
		rolling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pinSetter.ballEntered){
			if(transform.position.x < -70 || transform.position.x > 70){
				print ("ball fell");
				//ball fell, no pits hit
				ActionMaster.Action pinSetterAction = actionMaster.Bowl(0);
				if (pinSetterAction .Equals( ActionMaster.Action.Reset)) {
					pinSetter.anim.SetTrigger("ResetTrigger");
				} else if (pinSetterAction .Equals( ActionMaster.Action.Tidy)) {
					//dont need to tidy since no pins hit
					//pinSetter.anim.SetTrigger("TidyTrigger");
				} 
				ResetBall();

			}
		}
	}
}
