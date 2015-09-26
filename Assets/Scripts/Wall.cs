using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Sprite dmgSprite; 
	public int hp = 4;                          //hit points for the wall.
	
	private SpriteRenderer spriteRenderer;      //Store a component reference to the attached SpriteRenderer.

	// Use this for initialization
	void Awakes () {
		//Get a component reference to the SpriteRenderer.
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	//DamageWall is called when the player attacks a wall.
	public void DamageWall (int loss)
	{
		//Set spriteRenderer to the damaged wall sprite.
		spriteRenderer.sprite = dmgSprite;
		
		//Subtract loss from hit point total.
		hp -= loss;
		
		//If hit points are less than or equal to zero:
		if(hp <= 0)
			//Disable the gameObject.
			gameObject.SetActive (false);
	}
}
