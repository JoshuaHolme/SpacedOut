using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    /// <summary>Script that allows for debugging. Holding shift and hitting e toggles enemies on and off. Shift + t toggles camera triggers on and off. Shift + 0-4 brings you to those levels. 0 is the instruction level</summary>
public class DebugScript : MonoBehaviour
{
    private bool enemiesAlive = true;
    private bool triggersAlive = true;
    private GameObject[] lastEnemies;
    private GameObject[] lastTriggers;

    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown("e"))
            {
                ToggleEnemies();
            }

            if(Input.GetKeyDown("t"))
            {
                ToggleEnemies();
            }

            
            if(Input.GetKeyDown(KeyCode.Alpha0))
                ChangeLevel("Instructions");

            if(Input.GetKeyDown(KeyCode.Alpha1))
                ChangeLevel(1);

            if(Input.GetKeyDown(KeyCode.Alpha2))
                ChangeLevel(2);

            if(Input.GetKeyDown(KeyCode.Alpha3))
                ChangeLevel(3);

            if(Input.GetKeyDown(KeyCode.Alpha4))
                ChangeLevel(4);
            

        }
    }


    void ToggleEnemies()
    {
            enemiesAlive = !enemiesAlive;
            GameObject[] enemies;
            if(!enemiesAlive)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                lastEnemies = enemies;
            }
            else
            {
                enemies = lastEnemies;
            }
            foreach(GameObject e in enemies)
                e.SetActive(enemiesAlive);
    }


    void ToggleTriggers()
    {
        triggersAlive = !triggersAlive;
        GameObject[] triggers;
        if(!triggersAlive)
        {
            triggers = GameObject.FindGameObjectsWithTag("CameraTrigger");
            lastTriggers = triggers;
        }
        else
        {
            triggers = lastTriggers;
        }
        foreach(GameObject e in triggers)
            e.SetActive(triggersAlive);
    }

    void ChangeLevel(int lvl)
    {
        LevelManager.resetReached = false;
        SceneManager.LoadScene("Level" + lvl);
    }

    void ChangeLevel(string s)
    {
        LevelManager.resetReached = false;
        SceneManager.LoadScene(s);
    }


}
