using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>Class that triggers (on player collision) to display a hidden HUD and move message box down.</summary>
public class ShowHUDTriggerPoint : MonoBehaviour
{
    
    /// <summary>HUD to be displayed.</summary>
    public Canvas HUD;
     /// <summary>Message box to be moved down.</summary>
    public GameObject messageDisplayPanel;
    private bool collision = false;
    private static float startPos;
    private static bool hasBeenMoved = false;

    void Start()
    {
        startPos = messageDisplayPanel.GetComponent<RectTransform>().position.y;
        hasBeenMoved = false;
    }
    void Update()
    {
        if(collision && !hasBeenMoved)
        {
            if(messageDisplayPanel.GetComponent<RectTransform>().position.y > startPos-1.5f)
            {
                messageDisplayPanel.GetComponent<RectTransform>().position -= new Vector3 (0, .1f, 0);
            }
            else if(HUD.GetComponent<CanvasGroup>().alpha < 1)
            {
                HUD.GetComponent<CanvasGroup>().alpha += .01f;
            }
            else
            {
                Destroy(gameObject);
                hasBeenMoved = true;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            collision = true;
        }
    }
}
