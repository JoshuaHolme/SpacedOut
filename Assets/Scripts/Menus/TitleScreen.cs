using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>The games title screen controller</summary>

[RequireComponent(typeof(AudioSource))]
public class TitleScreen : MonoBehaviour
{
    private static bool isActive = true;
    /// <summary>Controller to handle input</summary>
    public MenuController nextMenu;
    private AudioSource audioData;

    void Start()
    {
        ScoreSystem.CreateSaveFile();
        audioData = GetComponent<AudioSource>();

        if (!isActive)
        {
            DeactivateScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            HandleInput();
    }

    void HandleInput()
    {
        if(Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            audioData.Play(0);
            DeactivateScreen();
        }

    }

    private void DeactivateScreen()
    {
        nextMenu.Activate();
        TitleScreen.isActive = false;
        Destroy(gameObject);
    }
}
