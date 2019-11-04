using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Trigger responsible for opening door checkpoints.</summary>
public class OpenDoorTrigger : MonoBehaviour
{
    /// <summary>Reference to the door to be opened.</summary>
    public Door door;
    private bool isDone = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !isDone)
        {
            isDone = true;
            col.gameObject.SendMessage("DoorOpening");
            door.Activate(DoorAction.OPEN, col.gameObject);
            //Destroy(gameObject);
        }
    }
}
