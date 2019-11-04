using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    /// <summary>Menu that asks the player if they are sure if they want to continue.</summary>
[RequireComponent(typeof(AudioSource))]
public class AreYouSureMenuController : MenuController
{
        /// <summary>Body of text for the menu.</summary>
    public Text messageText;
    private AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    /// <summary>Handles the user keyboard input for the menu system.</summary>
    public override void HandleInput()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("right"))
        {
            audioData.Play(0);
            base.MoveToNextOption();
        }
        else if (Input.GetKeyDown("up") || Input.GetKeyDown("left"))
        {
            audioData.Play(0);
            base.MoveToPreviousOption();
        }
        else if (Input.GetKeyDown("space") || Input.GetKeyDown("return"))
        {
            audioData.Play(0);
            selectedOption.ExecuteSelectedOption();
            base.Reset();
            MenuManager.lastController = this;
        }
        //audioData.Play(0); 
    }

    /// <summary>Displays menu and passes input control to the menu.</summary>
    public override void Activate()
    {
        base.Activate();
        ResetOptionCount();
        messageText.text = MenuManager.areYouSureText;
    }


}
