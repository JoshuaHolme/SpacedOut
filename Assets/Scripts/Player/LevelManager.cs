using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>Manages current level details such as camera boundaries, check point boundaries, and score at reset.</summary>

public class LevelManager
{
    public static bool resetReached = false;    // when player reaches a door this will be set true
    public static Vector2 playerStartPos;       
    public static Vector3 cameraStartPos;

    public static float cameraMinX;
    public static float cameraMinY;

    public static int scoreAtReset = 0;

    private LevelManager(){}
}
