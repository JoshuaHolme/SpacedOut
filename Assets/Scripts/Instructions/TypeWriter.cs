using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Class that writes out a message in a message box with a typewriter effect.</summary>
public class TypeWriter : MonoBehaviour
{

    private string message = "";
    private int charCount = 0;

    private int frameCount = 0;

    /// <summary>Number of frames between each letter being typed.</summary>
    public int typewriteDelay = 2;
    void Update()
    {
        if(!IsDone())
        {
            TypeWrite();
        }
    }

    private void TypeWrite()
    {
        if(frameCount++ % typewriteDelay == 0)
        {
            gameObject.GetComponent<Text>().text += message.ToCharArray()[charCount++];
        }
    }

    /// <summary>Sets message to be typed out.</summary>
    public void SetMessage(string msg)
    {
        charCount = 0;  // reset typewriter counter
        frameCount = 0;
        message = msg;
        gameObject.GetComponent<Text>().text = "";
    }

    private bool IsDone()
    {
        return charCount == message.Length;
    }
}
