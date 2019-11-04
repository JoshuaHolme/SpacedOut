using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    /// <summary>ScoreSystem is responsible for maintaining record of the players score, current level, saving, and loading.</summary>
public class ScoreSystem : MonoBehaviour
{

    private static int initialScore = 0;

    /// <summary>Players score</summary>
    public static int score = 0;

    /// <summary>Current level's HUD</summary>
    public HUD hud;

    /// <summary>Static reference to current HUD</summary>
    public static HUD sHud;
    
    /// <summary>Current level</summary>
    public static int level = 1;


    // Start is called before the first frame update
    void Start()
    {
        sHud = hud;
        initialScore = score;
        sHud.UpdateScoreText(score);
    }

    public static void IncreaseScore(int amount)
    {
        score += amount;
        sHud.UpdateScoreText(score);
    }

    /// <summary>Resets score to what the players score was at the latest checkpoint</summary>
    public static void ResetScore()
    {
        if(!LevelManager.resetReached)
            score = initialScore;
        else
            score = LevelManager.scoreAtReset;
    }

        // exactly the same as the Save() method except it checks to see if the file exists. Called at the title screen.
        /// <summary>Creates a save file on the user's device</summary>
    public static void CreateSaveFile()
    {
        if(!File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");

            SaveData data = new SaveData();
            data.level = ScoreSystem.level;
            data.score = ScoreSystem.score;

            bf.Serialize(file, data);
            file.Close();
        }
    }

    // saves game
    /// <summary>Saves data to user's device</summary>
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");

        // create new class to sore data in
        SaveData data = new SaveData();
        data.level = ScoreSystem.level;
        data.score = ScoreSystem.score;

        // write that class to a file
        bf.Serialize(file, data);
        file.Close();
    }

    // loads game
    /// <summary>Loads save data from user's device</summary>
    public static bool Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);

            // reads file and creates a new instance based on the contents of that file
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            // set the players score and level to match what was saved.
            ScoreSystem.score = data.score;
            ScoreSystem.level = data.level;
            SceneManager.LoadScene("Level" + data.level);

            return true;
        }
        else
            return false;

    }

}

    /// <summary>Class that arranges save data so it can be saved and loaded</summary>
// class only used to save and load data
[System.Serializable]
class SaveData
{
    /// <summary>Level to be saved or loaded</summary>
    public int level;

    /// <summary>Score to be saved or loaded</summary>
    public int score;
}
