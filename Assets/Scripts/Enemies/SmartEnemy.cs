using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Enemy that can move back and forth between 2 moveSpots. Can send a "Damage" messsage to foes. Enemy objects have HP, speed, and can drop pickups.</summary>
public class SmartEnemy : MonoBehaviour
{

    /// <summary>HP</summary>
   	public int health = 5;

    /// <summary>Random generator that randomly assigns enemies a pickup (or not!) to drop after death.</summary>
   	public PickUpGenerator pickUpGenerator;
   	private bool dead = false;
    	private SpriteRenderer sprite;
   	private bool activate = false;
    	int points = 100;

    /// <summary>Speed</summary>
	public float speed = 3;
	private float waitTime;
    public float startWaitTime = 0;

    /// <summary>Spots that enemy can move between.</summary>
	public Transform[] moveSpots;
	private int randomSpot;

    /// <summary>Animation for death.</summary>
    public GameObject deathAnimation;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
	    randomSpot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!activate)
            if(sprite.isVisible)
                activate = true;
        
	transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

	if(Vector2.Distance(transform.position, moveSpots[0].position) < 0.2f){
		if(waitTime <= 0){
			randomSpot = 1;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            waitTime = startWaitTime;
		}else{
			waitTime -= Time.deltaTime;
		}
	}

        if (Vector2.Distance(transform.position, moveSpots[1].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = 0;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }

    // This method is invoked when an enemy collides with something
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag != "Enemy" && col.gameObject.tag != "Ground")
            col.gameObject.SendMessage("Damage", 1, SendMessageOptions.DontRequireReceiver);        // This will run the method Damage(int) on whatever object it collides with. (We only care about the player for now).
        //Debug.Log("Collision");
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
}
