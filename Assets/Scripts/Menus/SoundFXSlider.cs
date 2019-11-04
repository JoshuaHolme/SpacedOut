using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Slider to adjust sound fx volume.</summary>
public class SoundFXSlider : MonoBehaviour
{
        /// <summary>Text to display value.</summary>
    public Text valueText;

 void Update()
 {
     float newValue = gameObject.GetComponent<Slider>().value;
     AudioController.soundFXVolume = newValue;
     valueText.text = (newValue * 100).ToString("0");
 }
}
