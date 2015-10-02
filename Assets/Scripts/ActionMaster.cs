using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionMaster : MonoBehaviour {

	//endturn is for switching to another player
	//if player gets a strike they get an extra turn
	public enum Action{Tidy, Reset, EndTurn, EndGame};
	//for player
	//public enum Chance{firstChance, secondChance};

	private PinSetter pinsetter;

	Player player1 = new Player ("player1");
	Player player2 = new Player ("player2");
	Player player3 = new Player ("player3");

	PlayerQueue queue =new PlayerQueue();

	public Text queueText;
	private int firstChanceScore;
	private int secondChanceScore;

	public Action Bowl(int pins){
		Player currentPlayer = queue.Peek();
		print ("bowl");
		if (pins < 10 && pins >= 0) {
			if(currentPlayer.getChance().Equals( Player.Chance.firstChance)){
				print ("first chance");
				firstChanceScore = pins;
				currentPlayer.addScore(firstChanceScore);
				//tidy and player gets second chance
				currentPlayer.setChance(2);
				return Action.Tidy;
			} else if (currentPlayer.getChance().Equals( Player.Chance.secondChance)){
				print ("second chance");
				secondChanceScore = (pins >= firstChanceScore) ? (pins - firstChanceScore) : (firstChanceScore- pins);
				currentPlayer.addScore(secondChanceScore);
				currentPlayer.setChance(1);
				if(pinsetter.standingPins == 0){
					print ("SPARE");
				}

				queue.Dequeue();
				queue.Enqueue(currentPlayer);

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
		queue.Enqueue (player1);
		queue.Enqueue (player2);
		queue.Enqueue (player3);

		pinsetter = GameObject.FindObjectOfType<PinSetter>();
	}
	
	// Update is called once per frame
	void Update () {
		string queueString = queue.printQueue();
		queueText.text = queueString;
	}
}
