using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OptionAction();
public class MenuOption: MonoBehaviour
{

       public OptionAction optionAction;
       public virtual void OnOptionSelected(){}

       public GameObject GetGameObject()
       {
           return gameObject;
       }
       
       public void ExecuteSelectedOption()
       {
            OnOptionSelected();     // here in case any changes need to take place without needing to change in multiple places.
       }
}
