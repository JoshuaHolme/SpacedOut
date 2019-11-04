using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundResources
{
    public static AudioClip bulletSound = Resources.Load<AudioClip>("Sounds/GunShot");
    public static AudioClip enemyDeathSound = Resources.Load<AudioClip>("Sounds/EnemyDeathSound");
    public static AudioClip doorSound = Resources.Load<AudioClip>("Sounds/DoorSectionSound");
    private SoundResources(){} // cant make instances


}
