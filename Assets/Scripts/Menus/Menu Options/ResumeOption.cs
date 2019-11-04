using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResumeOption : MenuOption
{
    public PauseSystem pauseSys;

    void Start()
    {
        if(pauseSys == null)
            Debug.LogError("Pause system's resume option requires a reference to pause system.");
    }
    public override void OnOptionSelected()
    {
        pauseSys.DeactivatePauseMenu();
    }

    public GameObject GetGameObject()
    {
        return GetGameObject();
    }
}
