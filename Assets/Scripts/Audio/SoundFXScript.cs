using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Script for GameObjects that play sound fxs</summary>
public class SoundFXScript : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = AudioController.soundFXVolume;
    }
}
