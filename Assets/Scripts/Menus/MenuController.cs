using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>General menu controller that handles keyboard input and menu option actions.</summary>
[RequireComponent(typeof(AudioSource))]
public class MenuController : MonoBehaviour
{

    /// <summary>List of menu options.</summary>
    public List<MenuOption> options;

        /// <summary>Selector icon that displays next to the currently selected option.</summary>
    public GameObject selectorIcon;
    private static MenuOption continueOption;
    private int count = -1;
    /// <summary>Currently selected option.</summary>
    public MenuOption selectedOption;
    private bool isActive = false;
    private bool delayIsOver = false;
    private AudioSource audioData;

    private int delayCount = 0;
    private const int delayFrames = 15;


    // Start is called before the first frame update
    void Start()
    {
        if(options.Count == 0)
            Debug.LogError("MenuController MUST have at least 1 option.");

        MoveToNextOption();     // start at the first option
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Tick())
        {
            HandleInput();

        }       
    }

    private bool Tick()
    {
        if(delayCount >= delayFrames)
            return true;
        else
            delayCount++;
        
        return false;

    }

    /// <summary>Handles all input for menu including deciding which option to pick and executing the action associated with it.</summary>
    public virtual void HandleInput()
    { 
        if (Input.GetKeyDown("down") || Input.GetKeyDown("right"))
        {
            audioData.Play(0);
            MoveToNextOption();
        }
        else if(Input.GetKeyDown("up") || Input.GetKeyDown("left"))
        {
            audioData.Play(0);
            MoveToPreviousOption();
        }
        else if(Input.GetKeyDown("space") || Input.GetKeyDown("return"))
        {
            audioData.Play(0);
            selectedOption.ExecuteSelectedOption();
            Reset();
            MenuManager.lastController = this;
        }
    }

    /// <summary>Resets the menu so the selector is at the first option.</summary>
    public void Reset()
    {
        this.isActive = false;
        this.delayIsOver = false;
        this.delayCount = 0;
    }

    /// <summary>Moves selector back an option.</summary>
    public void MoveToPreviousOption()
    {
        count--;

        if(count < 0)
        {
            count = options.Count - 1;
        }

        selectedOption = options.ToArray()[count];
        UpdateSelector();
    }

    /// <summary>Moves selector to next option.</summary>
    public void MoveToNextOption()
    {
        count++;

        if(count >= options.Count)
        {
            count = 0;
        }

        selectedOption = options.ToArray()[count];
        UpdateSelector();
    }
    
        /// <summary>Resets options.</summary>
    public void ResetOptionCount()
    {
        this.count = -1;
        this.MoveToNextOption();
    }


    private void UpdateSelector()
    {
        selectorIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectorIcon.GetComponent<RectTransform>().anchoredPosition.x, selectedOption.GetGameObject().GetComponent<RectTransform>().anchoredPosition.y);
    }

    /// <summary>Activates menu</summary>
    public virtual void Activate()
    {
        isActive = true;
        MenuManager.currentMenu = gameObject.transform.parent.gameObject;
    }

    /// <summary>Determines if menu is active.</summary>
    public bool IsActive()
    {
        return isActive;
    }

}
