using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Class that triggers (on player collision) text to be displayed on a message box</summary>
public class MessageTriggerPoint : MonoBehaviour
{

    /// <summary>The canvas that has the messagebox</summary>
    public Canvas messageDisplay;

    /// <summary>The typewriter text object that will write out the message</summary>
    public TypeWriter messageTextObject;

    /// <summary>The message to be written</summary>
    public string messageContents = "This is the opening message!";

    private bool collision = false;


    void Update()
    {
        if(collision)
        {
            if(messageDisplay.GetComponent<CanvasGroup>().alpha < 1)
            {
                messageDisplay.GetComponent<CanvasGroup>().alpha += .01f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //messageDisplay.GetComponent<CanvasGroup>().alpha = 1;
            collision = true;
            messageTextObject.SetMessage(messageContents);
            // Destroy(gameObject);
        }
    }


}
