using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    /// <summary>Generated from player and does damage to enemies</summary>
public class Bullet : MonoBehaviour
{

    public int speed = 16;

        /// <summary>How long the bullets last before being destroyed</summary>
    public int aliveFrames = 100;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>Sets position of bullet</summary>
    public void setPosition(Vector2 pos)
    {
        gameObject.transform.position = new Vector2 (pos.x, pos.y +.1f);
    }

    /// <summary>Sets the angle the bullet should travel based on the mouse click</summary>
    public void setAngle(float angle)
    {
        // rotate bullet
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // set x and y velocity of bullet based on angle
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(angle*Mathf.Deg2Rad), speed * Mathf.Sin(angle*Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // apply damage to collision object as long as its not the player.
        if(col.gameObject.tag != "Player" && col.gameObject.tag != "Bullet" && col.gameObject.tag != "PowerUp" && col.gameObject.tag != "CameraTrigger" && col.gameObject.tag != "MoveSpot")
        {
            Destroy(gameObject);    // destroy the bullet
            col.gameObject.SendMessage("Damage", 1, SendMessageOptions.DontRequireReceiver);        // This will run the method Damage(int) on whatever object it collides with. (enemies).
        }
    }

    void FixedUpdate()
    {
        if(count++ > aliveFrames)
        {
            Destroy(gameObject);
        }
    }

    private void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(SoundResources.bulletSound);
    }
}
