using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Heads up display that shows the health and score of the player.</summary>
public class HUD : MonoBehaviour
{

    /// <summary>Text to display score</summary>
    public Text score;
    
    /// <summary>Image to display hearts</summary>
    public Image heart;
    private MenuController pauseController;
    private List<Image> hearts = new List<Image>();
    private float spacing  = 1f;

    /// <summary>Updates score text</summary>
    public void UpdateScoreText(int s)
    {
        score.text = "Score: " + s;
    }

    /// <summary>Updates health</summary>
    public void UpdateHealth(int s)
    {
        foreach (Image item in hearts)
        {
            Destroy(item);
        }

        hearts.Clear();

        for(int i = 1; i < s; i++)
        {
            Image temp = Instantiate(heart);
            hearts.Add(temp);
            temp.transform.SetParent(gameObject.transform, false);
            temp.GetComponent<RectTransform>().position = new Vector3(heart.GetComponent<RectTransform>().position.x + i*spacing, heart.GetComponent<RectTransform>().position.y, 0);
        }
    }
    

}
