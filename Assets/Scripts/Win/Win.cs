using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    /// <summary>Object that brings the player to the next level.</summary>
public class Win : MonoBehaviour
{

    /// <summary>The number of the next level</summary>
    public int nextLevelNumber;
    private const string levelPrefix = "Level";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
           ScoreSystem.level = nextLevelNumber;
           ScoreSystem.Save();
           LevelManager.resetReached = false;
           SceneManager.LoadScene(levelPrefix + nextLevelNumber);
        }
    }
}
