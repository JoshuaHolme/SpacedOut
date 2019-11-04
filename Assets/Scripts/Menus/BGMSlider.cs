using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Slider to adjust background music</summary>
public class BGMSlider : MonoBehaviour
{
    /// <summary>Text that displays the value of the slider.</summary>
    public Text valueText;
    
 void Update()
 {
     float newValue = gameObject.GetComponent<Slider>().value;
     AudioController.bgMusicVolume = newValue;
     valueText.text = (newValue * 100).ToString("0");
 }
}
