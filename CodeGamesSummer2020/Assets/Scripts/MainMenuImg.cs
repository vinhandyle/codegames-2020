using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuImg : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((MainMenuBtn.inControl && gameObject.name == "Controls") || 
            (MainMenuBtn.inTrophy && 
            ((gameObject.name == "TrophyImg_1" && GlobalControl.complete) || 
            (gameObject.name == "TrophyImg_2" && GlobalControl.ending_1) ||
            (gameObject.name == "TrophyImg_3" && GlobalControl.ending_2) ||
            (gameObject.name == "TrophyImg_4" && GlobalControl.ending_3))))
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
