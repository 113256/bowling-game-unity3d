using UnityEngine;
using System.Collections;

public class ActionMaster : MonoBehaviour {

	//endturn is for switching to another player
	//if player gets a strike they get an extra turn
	public enum Action{Tidy, Reset, EndTurn, EndGame};
	//for player
	//public enum Chance{firstChance, secondChance};

	private PinSetter pinsetter;

	Player player1 = new Player ("player1");

	public Action Bowl(int pins){
		Player currentPlayer = player1;
		print ("bowl");
		if (pins < 10 && pins >= 0) {
			if(currentPlayer.getChance().Equals( Player.Chance.firstChance)){
				print ("first chance");
				currentPlayer.addScore(pins);
				//tidy and player gets second chance
				currentPlayer.setChance(2);
				return Action.Tidy;
			} else if (currentPlayer.getChance().Equals( Player.Chance.secondChance)){
				print ("second chance");
				currentPlayer.addScore(pins);
				currentPlayer.setChance(1);
				if(pinsetter.standingPins == 0){
					print ("SPARE");
				}
				return Action.Reset;
			}
		} 

		if (pins == 10) {
			if(currentPlayer.getChance().Equals(Player.Chance.firstChance)){
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
