using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>GameObjects that can send a "Damage" messsage to foes. Enemy objects have HP, speed, and can drop pickups.</summary>
public class BasicEnemy : MonoBehaviour
{

    /// <summary>Enemy HP.</summary>
    public int health = 5;
    /// <summary>Enemy speed.</summary>
    public int speed = -2;
    /// <summary>Random generator that randomly assigns enemies a pickup (or not!) to drop after death.</summary>
    public PickUpGenerator pickUpGenerator;
    private bool dead = false;
    private SpriteRenderer sprite;
    private bool activate = false;
    int points = 100;
    /// <summary>Death animation for the enemy.</summary>
    public GameObject deathAnimation;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!activate)
            if(sprite.isVisible)
                activate = true;
        
        if(activate)
            Move();
    }

    // This method is invoked when an enemy collides with something
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag != "Enemy" && col.gameObject.tag != "Ground")
            col.gameObject.SendMessage("Damage", 1, SendMessageOptions.DontRequireReceiver);        // This will run the method Damage(int) on whatever object it collides with. (We only care about the player for now).
        
        
    }

    void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);  // give the enemy some speed
    }

    void Damage(int amount)
    {
        // remove health by specified amount
        health -= amount;

        // if the enemy has no health left, destroy it.
        if(isDead())
        {
            die();
        }
    }

    private bool isDead()
    {
        return health <= 0;
    }

    private void die()
    {
        if(!dead)
        {
            generatePickUp();
            
            deathAnimation = (GameObject)Instantiate(deathAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
            ScoreSystem.IncreaseScore(points);
        }

        dead = true;
    }

    private void generatePickUp()
    {
        PowerUp3Bullets temp = pickUpGenerator.GetPickUp();

        if(temp != null)
        {
            PowerUp3Bullets newPowerUp = Instantiate(temp);
            newPowerUp.transform.position = gameObject.transform.position;
            newPowerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
        }
    }

    // private void ()
    // {
    //     gameObject.GetComponent<AudioSource>().PlayOneShot(SoundResources.enemyDeathSound);
    // }
}
