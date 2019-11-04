using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private MenuController pauseController;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseController = GameObject.Find("Pause Menu Controller").GetComponent<MenuController>();

        if(!PauseMenuControllerExists())
            Debug.LogError("Error! No pause menu controller found by HUD!");
    }

    void Update()
    {
        HandleInput();
    }

    private void ActivatePauseMenu()
    {
        if(PauseMenuControllerExists())
        {
            PauseSystem.isPaused = true;
            Debug.Log("Test");
            pauseController.gameObject.transform.parent.GetComponent<CanvasGroup>().alpha = 1;
            //pauseController.gameObject.SetActive(true);
            pauseController.Activate();
            pauseController.ResetOptionCount();
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogError("Error! No pause menu controller found by HUD!");
        }
    }

    public void DeactivatePauseMenu()
    {
            PauseSystem.isPaused = false;
            pauseController.gameObject.transform.parent.GetComponent<CanvasGroup>().alpha = 0;
            //pauseController.gameObject.SetActive(true);
            pauseController.Reset();
            Time.timeScale = 1;
    }

    private bool PauseMenuControllerExists()
    {
        return pauseController != null;
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!pauseController.IsActive())
                ActivatePauseMenu();
            else
                DeactivatePauseMenu();
        }

    }
}
