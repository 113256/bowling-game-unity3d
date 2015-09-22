using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;
	private Ball ball;

	// Use this for initialization
	void Start () {
		ball = this.GetComponent<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DragStart(){
		//capture time and position of drag start 
		dragStart = Input.mousePosition;
		print (dragStart.x + " " + dragStart.y);
		startTime = Time.time; //Time.time = time since game started running
	}

	public void DragEnd(){
		//launch the ball
		dragEnd = Input.mousePosition;
		endTime = Time.time;
		print (dragEnd.x + " " + dragEnd.y);
		float dragDuration = endTime - startTime;
		/*float dragLength = Mathf.Pow((dragEnd.y - dragStart.y),2) +  Mathf.Pow((dragEnd.x - dragStart.x),2);
		print ("duration " + dragDuration);
		print ("length " + dragLength);*/

		float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
		float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

		ball.Launch (new Vector3(launchSpeedX, 0f, launchSpeedZ));
	}

}
