using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOption : MenuOption
{
    private const string levelPrefix = "Level";
    public MenuController newMenuController;
    public GameObject areYouSurePrompt;
    public override void OnOptionSelected()
    {
        // SceneManager.LoadScene(levelPrefix + 1);

        
        MenuManager.areYouSureText = "Are you sure you want to exit the game?";

        // create new thingy
        this.optionAction = delegate() 
        {
            // if(UnityEditor.EditorApplication.isPlaying)
            //     UnityEditor.EditorApplication.isPlaying = false;
            // else
                Application.Quit();
        };

        MenuManager.continueOption = this.optionAction;
        areYouSurePrompt.SetActive(true);
        newMenuController.Activate();
    }

    public GameObject GetGameObject()
    {
        return GetGameObject();
    }
}
