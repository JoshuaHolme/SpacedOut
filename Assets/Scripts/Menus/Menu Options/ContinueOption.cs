using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueOption : MenuOption
{
        public override void OnOptionSelected()
    {
        MenuManager.continueOption();
        // MenuManager.currentMenu.SetActive(false);
        // MenuManager.lastController.Activate();
    }

    public GameObject GetGameObject()
    {
        return GetGameObject();
    }
}
