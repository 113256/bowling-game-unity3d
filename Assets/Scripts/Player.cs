using UnityEngine;
using System.Collections;

public class Player{

	//private bool firstChance;
	private string name;
	private int score;

	public enum Chance{firstChance, secondChance};
	private Chance currentChance;

	public void setChance(int num){
		if (num == 1) {
			currentChance = Chance.firstChance;
		} else if (num == 2) {
			currentChance = Chance.secondChance;
		}
	}
	public Chance getChance(){
		return currentChance;
	}

	public Player(string name){
		currentChance = Chance.firstChance;
		this.name = name;
	}

	public int getScore(){
		return score;
	}

	public void addScore(int score){
		this.score += score;
	}

	public void setScore(int score){
		this.score = score;
	}
	/*
	public bool SetFirstChance(bool a){
		this.firstChance = a;
	}

	public bool isFirstChance(){
		return firstChance;
	}*/

	public string getName(){
		return name;
	}
	

}
