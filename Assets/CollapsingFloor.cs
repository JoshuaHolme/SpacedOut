using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingFloor : MonoBehaviour
{
    private bool active = false;
    private int count = 0;
    private int frames = 5;

    void Update()
    {
        if(active)
            if(count++ > frames)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                active = false;
                count = 0;
            }
    }

    //if touched by player begin to fall
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ContactPoint2D contact = collision.contacts[0];

            if(Vector2.Dot(contact.normal, Vector2.down) > .5)        // if specifically hits the top of the platform
                active = true;
        }
    }


}
