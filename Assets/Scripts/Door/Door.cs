using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Enum for all possible door actions (OPEN, CLOSE)</summary>
    public enum DoorAction
    {
        OPEN = 0,
        CLOSE = 1
    }

/// <summary>This game's checkpoint. When a player collides with the openDoorTrig, the player no longer has control and a fixed sequence of the player moving through the door occurs.</summary>
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    /// <summary>The trigger to open the door.</summary>
    public OpenDoorTrigger openDoorTrig;
    private GameObject player;
    private CameraSystem cameraInstance;
    private bool activated = false;
    private bool animationOver = false;
    private bool openDoor = true;   // door opens
    private int numberOfDoorPieces = 0;
    private int animationTime = 60;
    private int interval;
    private int intervalCount;
    private int count;
    private AudioSource audioData;

    void Start()
    {
        // get door pieces by name
        numberOfDoorPieces = gameObject.transform.childCount;
        interval = animationTime / numberOfDoorPieces;
        audioData = GetComponent<AudioSource>();
        intervalCount = interval;
        cameraInstance = Camera.main.gameObject.GetComponent<CameraSystem>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(activated)
        {
            animate();
        }

        if(animationOver)
        {
            activated = false;
        }
    }

    /// <summary>Begins the opening the door process.</summary>
    public void Activate(DoorAction da, GameObject play)
    {
        if(!activated)
        {
            player = play;
            if(da == DoorAction.OPEN)
            {
                count = 0;
                openDoor = true;
                intervalCount = interval;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            }
            else
            {
                count = animationTime;
                openDoor = false;
                intervalCount = animationTime - interval;
                gameObject.transform.GetChild(numberOfDoorPieces-1).GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            }

            activated = true;
            animationOver = false;
        }
    }


    private void animate()
    {
        if(openDoor)
        {
            animateOpen();
        }
        else
        {
            animateClose();
            AdjustCamera();
        }
    }


    private void animateOpen()
    {
        if (count++ > intervalCount)
        {
            audioData.Play(0);
            gameObject.transform.GetChild(count/(interval)).GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);

            if(count != 0)
                intervalCount += interval;
        }

        if(count > animationTime)
        {
            audioData.Play(0);
            animationOver = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            player.SendMessage("DoorOpened");
        }
    }

    private void animateClose()
    {
        
        if (count-- < intervalCount)
        {
            audioData.Play(0);
            Debug.Log(count/interval);
            gameObject.transform.GetChild((count)/(interval)).GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            if (count != animationTime)
                intervalCount -= interval;
        }

        if(count == 0)
        {
            audioData.Play(0);
            animationOver = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            player.SendMessage("DoorClosed");
            cameraInstance.RefocusCamera();
        }
    }

    private void AdjustCamera()
    {
        if(openDoorTrig.GetComponent<SpriteRenderer>().isVisible)
            cameraInstance.PanCamera(.15f, 0);
        else
            SetNewStartPositions();
    }


    private void SetNewStartPositions()
    {
            LevelManager.scoreAtReset = ScoreSystem.score;
            LevelManager.cameraMinX = cameraInstance.transform.position.x;
            LevelManager.cameraMinY = cameraInstance.transform.position.y;
            LevelManager.cameraStartPos = cameraInstance.transform.position;
            LevelManager.playerStartPos = player.transform.position;
            LevelManager.resetReached = true;
    }

    private void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(SoundResources.doorSound);
    }
}
