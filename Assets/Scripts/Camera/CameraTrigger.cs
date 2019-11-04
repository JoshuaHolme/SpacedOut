using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>When the player collides with this object, the camera will be focused to a new focus object until the player catches up.</summary>
public class CameraTrigger : MonoBehaviour
{

    private CameraSystem cameraInstance;
    private GameObject player;
    /// <summary>Object for the camera to focus on when the player collides with this object moving left.</summary>
    public GameObject leftFocus;

     /// <summary>Object for the camera to focus on when the player collides with this object moving right.</summary>
    public GameObject rightFocus;
    private float leftHeight;
    private float rightHeight;
    private const float heightOffset = .25f;
    private const float screenHeightConst = 2f;
    private bool active = false;
    private float cameraVelocity;
    private float currentHeight;


    // Start is called before the first frame update
    void Start()
    {
        cameraInstance = Camera.main.GetComponent<CameraSystem>();
        leftHeight = leftFocus.transform.position.y + (leftFocus.GetComponent<BoxCollider2D>().size.y / 2) + screenHeightConst;
        rightHeight = rightFocus.transform.position.y + (rightFocus.GetComponent<BoxCollider2D>().size.y / 2)+ screenHeightConst;
    }

    void FixedUpdate()
    {
        if(active)
        {
            MoveCamera();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            if(CameraShouldMove())
                Activate(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            if(CameraShouldMove())
                Activate(true);
        }
    }

    bool CameraShouldMove()
    {
        Vector2 playerVelocity = player.gameObject.GetComponent<Rigidbody2D>().velocity;

        if(playerVelocity.x < 0)
        {
            currentHeight = leftHeight;
            if(leftHeight + heightOffset > cameraInstance.transform.position.y)
            {
                cameraVelocity = .15f;
                return true;
            }
            else if(leftHeight - heightOffset < cameraInstance.transform.position.y)
            {
                cameraVelocity = -.15f;
                return true;
            }
            else
            {
                cameraVelocity = 0;
                return false;
            }
        }
        else if(playerVelocity.x > 0)
        {
            currentHeight = rightHeight;
            if(rightHeight + heightOffset > cameraInstance.transform.position.y)
            {
                cameraVelocity = .15f;
                return true;
            }
            else if(rightHeight - heightOffset < cameraInstance.transform.position.y)
            {
                cameraVelocity = -.15f;
                return true;
            }
            else
            {
                cameraVelocity = 0;
                return false;
            }
        }

        return false;
    }

    void MoveCamera()
    {
        if(CameraInBounds(currentHeight))
        {
            Activate(false);
        }
        else
        {
            cameraInstance.PanCamera(0, cameraVelocity);
            cameraInstance.RefocusCameraY();
        }
    }

    void Activate(bool a)
    {
        active = a;
    }

    bool CameraInBounds(float height)
    {
        return (height + heightOffset > cameraInstance.transform.position.y && height - heightOffset < cameraInstance.transform.position.y);
    }
}
