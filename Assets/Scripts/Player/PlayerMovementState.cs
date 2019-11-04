using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState
{
    public GameObject gameObject;

    public PlayerMovementState(GameObject player)
    {
        if(player.tag == "Player")
        {
            gameObject = player;
        }
        else
        {
            Debug.LogError("PlayerMovementState requires a gameobject with a player tag.", player);
        }
    }

    public abstract void HandleInput();
    public virtual void WalkThroughDoor(){}
}
