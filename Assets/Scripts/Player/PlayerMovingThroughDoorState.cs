using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingThroughDoorState : PlayerMovementState
{

    private bool canMove = false;

    public PlayerMovingThroughDoorState(GameObject player):base(player)
    {
        gameObject.GetComponent<CharacterAnim>().ForceAnimation(false);
    }

    public override void HandleInput()
    {
        // player cannot move in this state at all.
        // the player must wait to collide with a door close trigger first.
        if(canMove)
            Move();
    }

    private void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }


    public override void WalkThroughDoor()
    {
        canMove = true;
        gameObject.GetComponent<CharacterAnim>().ForceAnimation(true);
    }
}
