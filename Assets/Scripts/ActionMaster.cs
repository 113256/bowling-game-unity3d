using UnityEngine;
using System.Collections;

public class ActionMaster : MonoBehaviour {

	//endturn is for switching to another player
	//if player gets a strike they get an extra turn
	public enum Action{Tidy, Reset, EndTurn, EndGame};
	//for player
	public enum Chance{firstChance, secondChance};

	private PinSetter pinsetter;

	Player player1 = new Player ("player1");

	public Action Bowl(int pins){
		Player currentPlayer = player1;

		if (pins < 10 && pins >= 0) {
			if(currentPlayer.getChance().Equals( Chance.firstChance)){
				currentPlayer.addScore(pins);
				//tidy and player gets second chance
				currentPlayer.setChance(2);
				return Action.Tidy;
			} else if (currentPlayer.getChance().Equals( Chance.secondChance)){
				currentPlayer.addScore(pins);
				if(pinsetter.standingPins == 0){
					print ("SPARE");
				}
				return Action.Reset;
			}
		} 

		if (pins == 10) {
			if(currentPlayer.getChance().Equals(Chance.firstChance)){
				print ("STRIKE");
				currentPlayer.setScore(10);
				return Action.Reset;
			}
		}
		return Action.Tidy;
	}

	// Use this for initialization
	void Start () {
		pinsetter = GameObject.FindObjectOfType<PinSetter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
