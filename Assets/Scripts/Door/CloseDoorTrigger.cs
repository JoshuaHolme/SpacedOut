using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Trigger responsible for closing door checkpoints.</summary>
public class CloseDoorTrigger : MonoBehaviour
{
    /// <summary>Reference to the door to be closed.</summary>
    public Door door;

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("DoorClosing");
            door.Activate(DoorAction.CLOSE, col.gameObject);
            Destroy(gameObject);
        }
    }
}
