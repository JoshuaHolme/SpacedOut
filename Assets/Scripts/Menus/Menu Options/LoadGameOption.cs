using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameOption : MenuOption
{
    public GameObject areYouSure;
    public MenuController newMenu;
    public override void OnOptionSelected()
    {

        MenuManager.areYouSureText = "Are you sure you want to load your previous save data? If no save data exists, continuing will result in starting new game.";

        // create new thingy
        this.optionAction = delegate() 
        {
            if(!ScoreSystem.Load())
            {
                Debug.Log("No save data");
            }
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

