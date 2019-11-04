using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingOption : MenuOption
{
    public GameObject settingMenu;
    public MenuController newMenuController;
    public override void OnOptionSelected()
    {
        settingMenu.SetActive(true);
        newMenuController.Activate();
    }

}