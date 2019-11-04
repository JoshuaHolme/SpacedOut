using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Powerup that allows player to have multiple shot bursts.</summary>
[RequireComponent(typeof(AudioSource))]
public class PowerUp3Bullets : MonoBehaviour
{
    // power ups will only stay alive for so long
    private int count = 0;
    /// <summary>Length of time before pickup disapears</summary>
    public int aliveTime = 300;

        /// <summary>The chance that an enemy will spawn this pickup</summary>
    public int chanceToSpawn = 50; // higher the number, the greater the chance
    private int firstPhase;
    private int secondPhase;
    private Color startColor;
    private bool invincibleSwitch = true;
    private bool activate = false;
    private SpriteRenderer sprite;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        firstPhase = (int)(aliveTime *.33f);
        secondPhase = (int)(aliveTime *.66f);
        audioData = GetComponent<AudioSource>();

        if (chanceToSpawn < 1)
            chanceToSpawn = 1;
        
    }

    // Update is called once per frame
    void Update()
    {

        // only starts to activate the "alive" timer once the camera sees the powerup
        if(!activate)
            if(sprite.isVisible)
                activate = true;

        // carry out timer if the powerup has been activated
        if(activate && !PauseSystem.isPaused)
            Tick();
        
    }


    // blinking animation. Flashing speeds up as time goes on.
    private void animate()
    {
        int blinkRate = count+1;

        if(count < firstPhase)
            return;
        
        else if(count < secondPhase)
        {
            blinkRate = 10;
        }
        else if(count < aliveTime)
        {
            blinkRate = 5;
        }

        if(count % blinkRate == 0)
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

    private void Tick()
    {
        if(count++ > aliveTime)
            Destroy(gameObject);

        animate();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            audioData.Play(0);
            col.gameObject.SendMessage("ThreeShotPowerUp");
            Destroy(gameObject);
        }
    }

    public int GetChance()
    {
        return chanceToSpawn;
    }
}
