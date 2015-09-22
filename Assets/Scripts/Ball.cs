using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody ballRigidBody;
	private AudioSource audioSource;
	private bool rolling = false;

	// Use this for initialization
	void Start () {
		ballRigidBody = this.GetComponent<Rigidbody> ();
		//disable gravity before launching ball so it can float in the air
		ballRigidBody.useGravity = false;

		audioSource = this.GetComponent<AudioSource> ();


	}

	public void moveStart(float amount){
		if (!rolling) {

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

		//click to launch
		/*if (Input.GetMouseButtonDown(0)) {
			if(!rolling){
				Launch (launchSpeed);
			}
		 */
		}
	}

