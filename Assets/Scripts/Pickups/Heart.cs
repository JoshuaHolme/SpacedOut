using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Pickup that restores player health.</summary>
public class Heart : PowerUp3Bullets
{

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("HpUp");
                Destroy(gameObject);
        }
    }
}
