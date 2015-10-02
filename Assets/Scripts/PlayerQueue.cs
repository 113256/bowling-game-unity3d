using UnityEngine;
using System.Collections;

//ienumerable and ienumerator interface to make it usable in a foreach statement
public class PlayerQueue : IEnumerable{
	
	
	private int length;
	
	private Node front = null;//points to front
	private Node back = null;//points to back (2 so that cost of both enqueueing and dequeueing will be O(1))
	
	
	public IEnumerator GetEnumerator()
	{
		Node node = front;
		while(node != null)
		{
			yield return node.item;
			node = node.next;
		}
	}
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	
	
	
	
	
	private class Node{
		public Player item;
		public Node next;
	}
	
	public bool isEmpty(){
		return front == null;
	}
	
	//front(item, link) ---->(item, link) ---->(item, link) ---->(item, link) ---->BACK (item, NULL)
	public void Enqueue(Player item){
		Node oldBack = back;//point to front 
		back = new Node();
		back.item = item;
		back.next = null;
		
		if (isEmpty()) front = back;
		else oldBack.next = back;
	}
	
	public Player Dequeue(){
		Node deletedNode = front;
		front = front.next; 
		if (isEmpty()) back = null;
		return deletedNode.item;
	}
	
	
	
	public Player Peek(){
		return front.item;
	}
	
	/*
	return 1 if removing from front
	return 2 if not front
	 */
	public int removePlayer(Player player){
		Debug.Log ("removing");
		
		Node current = front;
		Node nodeToDelete = null;
		//if at front
		if (current.item.getName () == player.getName ()) {
			//nodeToDelete = front;
			//front = front.next;
			Debug.Log("front");
			Dequeue();
			return 1;
		} else {
			while(current.item.getName() != player.getName()){
				
				if(current.next.item.getName()==( player.getName())){
					Debug.Log("found");
					break;
				}
				Debug.Log("current next");
				current = current.next;//current = node before the node we want to delete
			}
			nodeToDelete = current.next;
			Debug.Log(nodeToDelete.item.getName());
			Debug.Log(nodeToDelete.next);
			//rearrange links
			current.next = nodeToDelete.next;
			return 2;
		}
	}
	
	public string printQueue()
	{	string queueString;
		Node current = front;
		queueString = "";
		while(current!=null){
			queueString = queueString + "  " + current.item.getName() + ":: "+ current.item.getScore() +"\n"; 
			current = current.next;
		}
		return queueString;
		
	}
	
	public int checkLength(){
		length = 0;
		Node current = front;
		while(current!=null){
			length +=1;
			current = current.next;
		}
		Debug.Log ("length is " + length);
		return length;
	}
	
	public void checkQueue(){
		string checkString ="";
		Node current = front;
		while(current!=null){
			checkString = checkString + "-->" + current.item.getName(); 
			current = current.next;
		}
		Debug.Log (checkString);
	}
}





