using UnityEngine;
using System.Collections;


//this base class is created as an abstract class to show that we can't make an instance of Character directly.
abstract public class Character : MonoBehaviour,
								  IWalkable,
								  IAttackable,
								  IJumpable {
								  
	public float speed = 0.01F; 	// game designer can play with the values of speed
	public int dir = 0;		
	
	public bool onGround = true;
	public bool alreadyUsed = false; 	// jump speciality
	public bool isAxisInUse = false;    // to make Jump Axis works like getkeydown
	
	public Rigidbody2D rb;
	
	// can be changed from alice and bob's scripts if the game designer wants to play 
	public float specialVertical = 0F;
	public float specialHorizontal= 0F;
	
	
	void Start() {
		onGround = true;
		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	void Update() {
	
		if(Input.GetAxisRaw("Horizontal") > 0) {
			dir = 1;
			Walk(speed, dir);
		}
		else if(Input.GetAxisRaw("Horizontal") < 0) {
			dir = -1;
			Walk(speed, dir);
		}
		else if(Input.GetAxisRaw("Jump") != 0 && isAxisInUse ==false) {
			
			if(onGround) {
				Jump();
				print ("Normal Jump");
			}
			
			else if (!onGround && !alreadyUsed){
				SpecialJump();
				print ("Special Jump");
			}
			
			isAxisInUse = true;
		
		}
		
		if(Input.GetAxis("Jump") == 0)
			isAxisInUse = false;
	}
	
	public void Walk(float speed, int dir) {
	
		if(dir>0) {
			transform.position += new Vector3(speed,0,0);
		}
		else {
			transform.position -= new Vector3(speed,0,0);
		}
										  								  								  								  								  								  			  								  								  								  								  								  
	}	
	
	public void Attack() {}		
	
	public void Jump() {
			rb.velocity += new Vector2(0F,10F);
			onGround = false;
	}
	
	public void SpecialJump () {
	
		rb.velocity += new Vector2(specialVertical, specialHorizontal);
		alreadyUsed = true;
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
	
		if(collision.gameObject.CompareTag("Ground"))
			onGround = true;
			alreadyUsed = false;
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
	
		rb.velocity += new Vector2(0F,-20F);;
	}

}
