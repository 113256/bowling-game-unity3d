using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public int standingPins = 10;
	public Text standingpinsText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		countStanding ();
		standingpinsText.text = standingPins.ToString();
	}
	
	public void countStanding(){
		int pins = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if(pin.isStanding()){
				pins++;
			}
		}
		standingPins = pins;
	}
}
