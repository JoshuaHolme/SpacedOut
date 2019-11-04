using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{

    private enum State
    {
        normal = 0,
        invincible = 1
    }

    private static Vector2 startPos;

    public HUD hud;
    public Bullet bullet;
    public int health = 3;
    private const int maxHealth = 5;

    private PlayerShotState shotState;
    private PlayerMovementState moveState;

    private State state = State.normal;

    private SpriteRenderer sprite;

    private AudioSource audioData;

    // ATTRIBUTES FOR PLAYER'S INVINCIBILITY STATE
    private Color startColor;
    private int invincibleCounter = 0;
    private const int invincibleFrames = 60;
    private bool invincibleSwitch = false;

    // ATTRIBUTES FOR PLAYER'S MOVEMENT
    public int speed = 5;
    private bool facingRight = true;
    public float jumpPower = 11.5f;
    public float secondJumpPower;
    private float minJumpPower;
    private float moveX;

    // ATTRIBUTES FOR PLAYER'S SHOOTING
    private int frameCount = 0;
    private bool fired = false;
    public int fireDelayFrames = 3;
    private const float fireJumpRadiusX = 3f;

    // KEEPS TRACK OF WHEN PLAYER WAS LAST ON THE GROUND BY JUMP OR SHOT JUMP
    private bool isGrounded = false;
    private bool jumpByShot = false;
    private float shotJumpAngle = 0.0f;
    private int count = 0;
    public int shotJumpFrames = 20;

    // Start is called before the first frame update
    void Start()
    {
        jumpPower = 11.5f;
        ReversePlayer();
        secondJumpPower = jumpPower * 1.25f;
        minJumpPower = jumpPower *.5f;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        hud.UpdateHealth(health);
        audioData = GetComponent<AudioSource>();

        if (LevelManager.resetReached)
        {
            gameObject.transform.position = LevelManager.playerStartPos;
        }

    }

    // method to be calle when a player takes damage (does nothing when the player is in the invisible state)
    void Damage(int amount)
    {
        audioData.Play(0);
        if (state != State.invincible)
        {
            health -= amount;
            hud.UpdateHealth(health);
        }

        if(isDead())
        {
            Die();
        }
        else if(state != State.invincible)
        {
            setInvincible(true);
        }
    }

    void HpUp()
    {
        if (health < maxHealth)
        {
            health++;
            hud.UpdateHealth(health);
        }
    }

    private void Die()
    {
        ScoreSystem.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseSystem.isPaused)
        {
            HandleInput();
            DeterminekState();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ContactPoint2D contact = collision.contacts[0];
            // Debug.Log(contact);

            if(Vector2.Dot(contact.normal, Vector2.up) > .5)
            {
                // Debug.Log("Grounded");
                isGrounded = true;
                jumpByShot = false;
                count = 0;
            }
        }
        if (collision.gameObject.tag == "FloorHazard")
        {
            Damage(1);
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(isGrounded)
            {
                jumpByShot = false;
                count = 0;
            }
            // else
            //     jumpByShot = true;

            isGrounded = false;
        }
    }


    void HandleInput()
    {
        if(moveState == null)
        {
            CheckMovement();
            Fire();
            checkIfReadyToFire();
        }
        else
        {
            moveState.HandleInput();
        }
    }
    void CheckMovement()
    {
        // CONTROLS
        moveX = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            // Debug.Log("JUMP!");
        }

        if(Input.GetButtonUp("Jump"))
        {
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y > minJumpPower)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, minJumpPower);
            }
        }

        // ANIMATE

        // PLAYER DIRECTION
        if(moveX < 0.0f && facingRight == false)
            ReversePlayer();
        else if(moveX > 0.0f && facingRight == true)
            ReversePlayer();

        if(jumpByShot)
        {
            if(count++ < shotJumpFrames)
            {
                SetShotJumpVelocity(shotJumpAngle);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed,  gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

    }

    void Fire()
    {
        if(Input.GetButton("Fire1") && !fired)
        {
            fired = true;

            float angle = facingRight ? 180 : 0;//getShotAngle();

            if(shotState == null)
            {
                Bullet newBullet = Instantiate(bullet);
                newBullet.setPosition(gameObject.transform.position);
                newBullet.setAngle(angle);
            }
            else
                shotState.Fire(angle);

            //handleJumpByShot(angle);

        }
    }

    private void handleJumpByShot(float angle)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(!jumpByShot && !isGrounded)
        {
            if(mousePos.y < gameObject.transform.position.y && mousePos.x < gameObject.transform.position.x + fireJumpRadiusX && mousePos.x > gameObject.transform.position.x - fireJumpRadiusX)
            {
                jumpByShot = true;
                shotJumpAngle = angle;
                //SetShotJumpVelocity(angle);
            }
        }
    }

    private void checkIfReadyToFire()
    {
        int delay;

        if(shotState == null)
            delay = fireDelayFrames;
        else
            delay = shotState.getFireDelay();

        if(frameCount++ > delay)
        {
            fired = false;
            frameCount = 0;
        }
    }

    public void ThreeShotPowerUp()
    {
        // Debug.Log("THREE SHOT");
        shotState = new ThreeShotState(gameObject, bullet);
    }


    private float getShotAngle()
    {
        
            Vector2 mousePos = Input.mousePosition;
            float den = (mousePos.x - Camera.main.WorldToScreenPoint(gameObject.transform.position).x);
            float num = (mousePos.y - Camera.main.WorldToScreenPoint(gameObject.transform.position).y);
            float degrees = Mathf.Rad2Deg * Mathf.Atan(num/ den);

            if(degrees < 0 && den < 0 && num > 0)
                degrees += 180;

            if(degrees > 0 && num < 0 && den < 0)
                degrees += 180;

            return degrees;
    }

    void Jump()
    {
        //isGrounded = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + jumpPower);
    }


    void SetShotJumpVelocity(float angle)
    {
        //isGrounded = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2( -secondJumpPower * Mathf.Cos(angle*Mathf.Deg2Rad), -secondJumpPower * Mathf.Sin(angle*Mathf.Deg2Rad) );
    }

    void ReversePlayer()
    {
        facingRight = !facingRight;
        gameObject.GetComponent<SpriteRenderer>().flipX = facingRight;
    }


    private bool isDead()
    {
        return health <= 0;
    }

    private void setInvincible(bool b)
    {
        if(b)
        {
            state = State.invincible;
            gameObject.layer = LayerMask.NameToLayer("Invinsible");
        }
        else
        {
                state = State.normal;
                gameObject.layer = LayerMask.NameToLayer("Default");
                gameObject.GetComponent<SpriteRenderer>().color = startColor;
                invincibleCounter = 0;
                invincibleSwitch = false;
        }
    }

    private void DeterminekState()
    {
        if(state == State.invincible)
        {
            if(invincibleCounter++ > invincibleFrames)
            {
                setInvincible(false);
            }
            else
            {
                if(invincibleCounter % 5 == 0)
                {
                    invincibleSwitch = !invincibleSwitch;

                    if(invincibleSwitch)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = startColor;
                    }
                }
            }
        }

        DetermineShotState();
    }

    private void DetermineShotState()
    {
        if(shotState != null)
        {
            if(shotState.GetCount() > shotState.GetFinishTime())
                shotState = null;
            else
                shotState.IncreaseTime(1);
        }
    }

    public void DoorOpening()
    {
        moveState = new PlayerMovingThroughDoorState(gameObject);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    public void DoorOpened()
    {
        if(moveState is PlayerMovingThroughDoorState)
        {
            moveState.WalkThroughDoor();
        }
    }

    public void DoorClosing()
    {
        moveState = new PlayerMovingThroughDoorState(gameObject);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    public void DoorClosed()
    {
        moveState = null;
    }

    public void SetStartPos()
    {
        Player.startPos = this.transform.position;
    }

    public void PassThroughDoor()
    {

    }
}
