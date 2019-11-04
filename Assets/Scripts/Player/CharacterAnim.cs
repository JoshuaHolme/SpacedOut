using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Animation for character movement</summary>
public class CharacterAnim : MonoBehaviour {

	private Animator anim;
	private bool forceAnimation;

	void Start(){

		anim = GetComponent<Animator>();

	}

	void Update(){
		if(!forceAnimation)
		{
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
				anim.SetBool("isMoving", true);
			} else{
				anim.SetBool("isMoving", false);
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				anim.SetTrigger("jump");
			}
		}
	}

    /// <summary>Forces the player to animate even when there is no user input.</summary>
	public void ForceAnimation(bool b)
	{
		forceAnimation = b;
		anim.SetBool("isMoving", b);
	}

}