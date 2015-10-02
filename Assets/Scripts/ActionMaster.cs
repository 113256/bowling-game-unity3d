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
	//public so that ACtionMAsterTest.cs can access
	public PlayerQueue queue =new PlayerQueue();

	public Text queueText;
	private int firstChanceScore;
	private int secondChanceScore;

	private Player currentPlayer;

	public Text actionText;
	private string actionString;

	public Text playerText;
	private string playerString;

	private Ball ball;

	private float startTime;
	private bool timerStarted;
	public Text timerText;
	//private int timeDifference;



	public void startTimer(){
		timerStarted = true;
		startTime = Time.time;
	}

	public float getStartTime(){
		return startTime;
	}

	public void resetTimer(){
		timerStarted = false;
	}



	public void setActionString(string astring){
		actionString = astring;
	}

	public Action Bowl(int pins){
		currentPlayer = queue.Peek();
		int actionNo = 0;//1 = tidy, 2 = reset...

		print ("bowl");
		if (pins < 10 && pins >= 0) {
			if(currentPlayer.getChance().Equals( Player.Chance.firstChance)){
				print ("first chance");
				firstChanceScore = pins;
				currentPlayer.addScore(firstChanceScore);
				//tidy and player gets second chance
				currentPlayer.setChance(2);
				actionNo = 1;//tidy

			} else if (currentPlayer.getChance().Equals( Player.Chance.secondChance)){
				print ("second chance");
				secondChanceScore = (pins >= firstChanceScore) ? (pins - firstChanceScore) : (firstChanceScore- pins);
				currentPlayer.addScore(secondChanceScore);
				currentPlayer.setChance(1);
				if(pinsetter.getStandingPins () == 0){
					print ("SPARE");
				}

				queue.Dequeue();
				queue.Enqueue(currentPlayer);


				actionNo = 2;//reset
			}
		} 

		if (pins == 10) {
			if(currentPlayer.getChance().Equals(Player.Chance.firstChance)){
				print ("STRIKE");
				currentPlayer.setScore(10);
				queue.Dequeue();
				queue.Enqueue(currentPlayer);
				actionNo = 2;//reset
			}
		}

		print ("resettimer");
		resetTimer();

		switch (actionNo) {
		case 1:
			return Action.Tidy;
			break;
		case 2:
			return Action.Reset;
		}

		return Action.Tidy;
	}

	// Use this for initialization
	void Start () {
		queue.Enqueue (player1);
		queue.Enqueue (player2);
		queue.Enqueue (player3);

		pinsetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball> ();

		currentPlayer = queue.Peek ();
	}
	
	// Update is called once per frame
	void Update () {
		string queueString = queue.printQueue();
		queueText.text = queueString;
		actionText.text = actionString;


		if (ball.checkRolling () == false) {
			playerString = queue.Peek().getName() + "'s turn. Drag ball forward to launch";
		}
		playerText.text = playerString;
	
		if (timerStarted) {
			timerText.text = "Ball must enter pin area before timer reaches 6: "+((int)(Time.time - startTime)).ToString();
			if ((Time.time - startTime) >= 6f) {
				Action pinSetterAction = Bowl(0);
				if (pinSetterAction .Equals( Action.Reset)) {
					setActionString("Resetting pins");
					pinsetter.anim.SetTrigger("ResetTrigger");
				} else if (pinSetterAction .Equals( ActionMaster.Action.Tidy)) {
					//dont need to tidy since no pins hit
					//pinSetter.anim.SetTrigger("TidyTrigger");
				} 
				//if ball fell on 1st chance, enable launch on 2nd chance
				if(queue.Peek().getChance().Equals(Player.Chance.secondChance)){
					ball.notRolling();
				}
				ball.ResetBall();
				timerStarted = false;
			}
		}
	}

	public float timeDifference(){
		return Time.time - startTime;
	}
}
