using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake2d : MonoBehaviour
{
	//snake
	public Transform snake_prefab;
   	private List<Transform> snake_list = new List<Transform>();
   	
   	public float X;
	public float Y;
	private Vector3 velocity;
	public float speed; 
	private int moveH;
	private float xMovdir;
	private float yMovdir;
   	private float snake_block = 1f;
   	private bool start = false;
   	private float sTime;
   	private float deltasT = 0.1f;
   	private bool start_game = false;
   	
   	private int Length_of_snake = 1;
   	
   	//box
   	public Transform box_prefab;
	public Transform boxv_prefab;
	public Transform[] abox_prefab ;
   	private List<Transform> box_list = new List<Transform>();
   	private int Nb_box = 30;
   	private int count_box = 0;
   	   	
   	//game
   	private bool gameclose = false;
   	public Text PerduText;
   	public Text WinText;
   	private bool win = false;

	bool IsIN_box_list(float x, float y){
	
		for(int i = 0;i < box_list.Count;i++){
			if(box_list[i].position.Equals(new Vector3(x, y, 0))){
				return true;			
			
			}
		}
			
		return false;

	}
	
	float RandSnakeFoodPos(float r, float l){
		return Mathf.Round(Random.Range(r,l) / snake_block) * snake_block;
	
	}
   	
   	void Instantiate_Box(){
   		//foodx = round(random.randrange(0, dis_width - snake_block) / snake_block) * snake_block
		//foody = round(random.randrange(0, dis_height - snake_block) / snake_block) * snake_block
		float fx, fy;
		for(int i = 0;i < Nb_box;i++){
			fx = RandSnakeFoodPos(-8.3f, 8.3f);
			fy = RandSnakeFoodPos(-4.4f, 4.4f);
			while(IsIN_box_list(fx, fy) || (fx == 0 && fy == 0)){
				fx = RandSnakeFoodPos(-8.3f, 8.3f);
				fy = RandSnakeFoodPos(-4.4f, 4.4f);
			}
			box_list.Add(Instantiate(abox_prefab[Random.Range(0, abox_prefab.Length)], new Vector3(fx,fy,0), Quaternion.identity));
		
		}
   	
   	}
   	
	void Start()
	{
		X = 0;
		Y = 0;
		moveH = 0;
		
		Instantiate_Box();
	}
	
	void Reset()
	{
		X = 0;
		Y = 0;
		moveH = 0;
		PerduText.enabled = false;
		WinText.enabled = false;
		win = false;
		Length_of_snake = 1;
		start_game = false;
		start = false;
		
		foreach(Transform sb in snake_list){
			Destroy(sb.gameObject);
		}
		snake_list.Clear();
		
		foreach(Transform bb in box_list){
			Destroy(bb.gameObject);
		}
		box_list.Clear();
		count_box = 0;
		
		Instantiate_Box();
		
	}


	void FixedUpdate()
	{
		if(gameclose){
			if(win)
				WinText.enabled = true;
			else
				PerduText.enabled = true;
			
			
			if (Input.GetKey("r")){
				Reset();
				gameclose = false;
			
			}

		}
		else
		{
			
			//moveH = 0;
			float xMov = Input.GetAxisRaw("Horizontal"); //gauche droite -1 1 q d
			float yMov = Input.GetAxisRaw("Vertical");
		
			if(start == false){
				snake_list.Add(Instantiate(snake_prefab, new Vector3(X, Y, 0), Quaternion.identity));
				start = true;
				return;
			}
		
			if(xMov != 0){
				moveH = 1;
				xMovdir = xMov;
				start_game = true;
			
			}
			else if(yMov != 0){
				moveH = 2;
				yMovdir = yMov;
				start_game = true;
			
			}
		
			sTime += Time.deltaTime;
			if(sTime > deltasT && start_game){
				if(moveH == 1){
					X += snake_block * xMovdir;
				}
				else if(moveH == 2){
					Y += snake_block * yMovdir;
				}
					
				snake_list.Add(Instantiate(snake_prefab, new Vector3(X, Y, 0), Quaternion.identity));
			
				if(snake_list.Count > Length_of_snake)
				{
					//List<Transform>.Enumerator enums = snake_list.GetEnumerator();
					//enums.MoveNext();
					//Destroy(enums.Current.gameObject);
					Destroy(snake_list[0].gameObject);
					snake_list.RemoveAt(0);
				}
		
				/*List<Transform>.Enumerator enumsk = snake_list.GetEnumerator();
				enumsk.MoveNext();
				Transform first_elem = enumsk.Current;
				while(enumsk.MoveNext()){
					Transform el = enumsk.Current;
					if(el.position.Equals(first_elem.position)){
						gameclose = true;
						Debug.Log("Perdu");
					}
				}*/
				
				for(int i = 0;i < snake_list.Count-1;i++){
					if(snake_list[i].position.Equals(snake_list[snake_list.Count-1].position)){
						gameclose = true;
						win = false;
						Debug.Log("Perdu");
					}
				}
			
				if( Y > 4.4f || Y < -4.4f || X > 8.3f || X < -8.3f ){
					gameclose = true;
				}
					
				for(int i = 0;i < box_list.Count;i++){
					if(box_list[i].position.Equals(snake_list[snake_list.Count-1].position)){
						Destroy(box_list[i].gameObject);
						box_list.RemoveAt(i);
						count_box++;
						if(count_box == Nb_box){
							gameclose = true;
							win = true;
						}
						Length_of_snake++;
						break;
					}
				
				}
			
						
				sTime = 0f;
			}
		
		
		}
		

	}
}
