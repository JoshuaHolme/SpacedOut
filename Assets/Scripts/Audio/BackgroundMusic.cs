using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>GameObject that has background music</summary>
public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = AudioController.bgMusicVolume;
    }
    
}
