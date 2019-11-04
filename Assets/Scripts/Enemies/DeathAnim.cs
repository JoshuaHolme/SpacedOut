 using UnityEngine;
 using System.Collections;
 
     /// <summary>Animation for enemy death.</summary>
 public class DeathAnim : MonoBehaviour
 {
         /// <summary>How long to animate.</summary>
     public float waitTime;
 
     void Start ()
     {
         Destroy (gameObject, waitTime);
     }
 }