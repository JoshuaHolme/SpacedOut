using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOption : MenuOption
{
    private const string levelPrefix = "Level";
    public MenuController newMenu;
    public GameObject areYouSure;
    public override void OnOptionSelected()
    {
        // SceneManager.LoadScene(levelPrefix + 1);

        
        MenuManager.areYouSureText = "Selecting this option will overwrite any previous save data. Are you sure you want to start a new game?";

        // create new thingy
        this.optionAction = delegate() 
        {
            ScoreSystem.level = 1;
            ScoreSystem.score = 0;
            ScoreSystem.Save();
            SceneManager.LoadScene(levelPrefix + 0);
        };

        MenuManager.continueOption = this.optionAction;
        areYouSure.SetActive(true);
        newMenu.Activate();
    }

    public GameObject GetGameObject()
    {
        return GetGameObject();
    }

}
