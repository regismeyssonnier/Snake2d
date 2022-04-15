using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	public float X;
	public float Y;
	private Vector3 velocity;
	public float speed; 
	private int moveH;
	private float xMovdir;
	private float yMovdir;

	void Start()
	{
		moveH = 0;
	}


	void FixedUpdate()
	{
		float xMov = Input.GetAxisRaw("Horizontal"); //gauche droite -1 1 q d
		float yMov = Input.GetAxisRaw("Vertical");
		
		if(xMov != 0){
			moveH = 1;
			xMovdir = xMov;
		}
		if(yMov != 0){
			moveH = 2;
			yMovdir = yMov;
		}
		
		Vector3 moveHorizontal = Vector3.zero; 
		Vector3 moveVertical = Vector3.zero; 
		
		if(moveH == 0){
			velocity = Vector3.zero;
		}
		else if(moveH == 1){
			moveHorizontal = transform.right * xMovdir;
			moveVertical = Vector3.zero;
		}
		else if(moveH == 2){
			moveHorizontal = Vector3.zero;
			moveVertical = transform.up * yMovdir;
		}
		
		velocity = (moveHorizontal + moveVertical).normalized * speed;
		
		Vector3 tp = transform.position+velocity*Time.fixedDeltaTime;
		if((tp.y < 4.4f) && (tp.y > -4.4f) && (tp.x < 8.3f) && (tp.x > -8.3f)){
			transform.position = tp;
		}
		//Debug.Log(transform.position);
		
		
		
	}
	
}
