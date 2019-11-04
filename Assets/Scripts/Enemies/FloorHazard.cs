using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>A stationary object that can deal a specified amount of damage to the player.</summary>
public class FloorHazard : MonoBehaviour
{

    /// <summary>Amount of HP to be dealt when collided with</summary>
    public int attackPower = 1;

    void OnTriggerStay2D(Collider2D col)
    {
            if(col.gameObject.tag == "Player")  // if you want it to damage player use the if, if you want it to damage everything remove it.
                col.gameObject.SendMessage("Damage", attackPower);  
    }
}
