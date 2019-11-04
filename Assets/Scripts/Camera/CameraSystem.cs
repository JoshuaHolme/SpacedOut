using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [RequireComponent (typeof (GameObject))]
/// <summary>Controls how the camera behaves during gameplay.</summary>
public class CameraSystem : MonoBehaviour
{
/// <summary>GameObject that the camera will focus on and travel with.</summary>
    public GameObject focusObject;
/// <summary>Minimum x value that the camera can travel.</summary>
    public float xMin;
/// <summary>Maximum x value that the camera can travel.</summary>
    public float xMax;
/// <summary>Minimum y value that the camera can travel.</summary>
    public float yMin;
/// <summary>Maximum y value that the camera can travel.</summary>
    public float yMax;
/// <summary>Cameras X position offset to the focusObject X position</summary>
    public float xOffset;

/// <summary>Cameras X margin</summary>
    public float thresholdX = 2;
/// <summary>Cameras Y margin</summary>
    public float thresholdY = 2;

    private bool focusedCamera = true;

    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.resetReached)
        {
            this.xMin = LevelManager.cameraMinX;
            this.yMin = LevelManager.cameraMinY;
            gameObject.transform.position = LevelManager.cameraStartPos;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraPos();
    }

    /// <summary>Moves camera at a specified x and y speed</summary>
    public void PanCamera(float xSpeed, float ySpeed)
    {
        if(focusedCamera)
            focusedCamera = false;

        gameObject.transform.position += new Vector3(xSpeed, ySpeed, 0);
    }

/// <summary>Returns the cameras y focus back on the focusObject</summary>
    public void RefocusCameraY()
    {
        yMin = GetPosition().y;
        focusedCamera = true;
    }

/// <summary>Returns the camera's position</summary>
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

/// <summary>Returns the cameras focus back on the focusObject</summary>
    public void RefocusCamera()
    {
        xMin = GetPosition().x;
        yMin = GetPosition().y;

        focusedCamera = true;
    }

    void UpdateCameraPos()  //updates the camera position to the gameObject it must follow
    {
    
        if(focusedCamera)
            if(focusObject.transform.position.x + xOffset > gameObject.transform.position.x - thresholdX || focusObject.transform.position.x + xOffset < gameObject.transform.position.x + thresholdX)
            {
                float x = Mathf.Clamp(focusObject.transform.position.x + xOffset, xMin, xMax);
                float y = Mathf.Clamp(focusObject.transform.position.y, yMin, yMax);


                gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
            }
    }

}
