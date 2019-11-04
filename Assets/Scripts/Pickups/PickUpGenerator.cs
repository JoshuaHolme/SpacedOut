using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    /// <summary>Generates a random pickup that an enemy can drop.</summary>
public class PickUpGenerator : MonoBehaviour
{
    /// <summary>Different types of pickups</summary>
    public PowerUp3Bullets[] pickups;
    private int total;

    /// <summary>Chance that a pick up will even be generated.</summary>
    public int chanceToGetPickUpPercentage = 8;     // 8% chance to get a pickup

    void Start()
    {
        foreach (PowerUp3Bullets g in pickups)
        {
            total += g.GetChance();
        }
        // Debug.Log("Total: " + total);

    }

    /// <summary>Returns a random pickup based on chance</summary>
    public PowerUp3Bullets GetPickUp()
    {

        System.Random rand = new System.Random();

        int number = rand.Next(1, 100);

        if(number < chanceToGetPickUpPercentage)
        {
            // Debug.Log("Total: " + total);
            number = rand.Next(1, total);
            
            int sum = 0;

            foreach (PowerUp3Bullets g in pickups)
            {
                int chance = g.GetChance();


            // Debug.Log("If: " + chance + sum + " >= " +number);
                if(chance + sum >= number)
                {
                    return g;
                }

                sum += chance;
            }
            

        }


        return null;
    }
}
