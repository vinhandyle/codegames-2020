using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuImg : MonoBehaviour
{
    private Image img;
    public List<Sprite> sprites;
    public bool animated;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        if (!animated)
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Main Menu 
       if ((MainMenuBtn.inControl && gameObject.name == "Controls") || 
            (MainMenuBtn.inTrophy && 
            ((gameObject.name == "TrophyImg_1" && GlobalControl.complete) || 
            (gameObject.name == "TrophyImg_2" && GlobalControl.ending_1) ||
            (gameObject.name == "TrophyImg_3" && GlobalControl.ending_2) ||
            (gameObject.name == "TrophyImg_4" && GlobalControl.ending_3))))
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (!animated)
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }

        // Controls
        if (gameObject.name == "Controls")
        {
            if (GlobalControl.heartlessUnlocked)
            {
                img.sprite = sprites[1];
            }
            else
            {
                img.sprite = sprites[0];
            }
        }

        // Endings
        if (Time.time >= 3.35f && GlobalControl.area != "Ending_1")
         {
             if (gameObject.name == "Countdown")
             {
                img.color = new Color(1f, 1f, 1f, 0f);
             }
         }      
    }
}
