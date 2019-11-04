using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackOption : MenuOption
{
        public override void OnOptionSelected()
    {
        MenuManager.currentMenu.SetActive(false);
        MenuManager.lastController.Activate();
    }

    public GameObject GetGameObject()
    {
        return GetGameObject();
    }
}