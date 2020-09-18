using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuImg : MonoBehaviour
{
    private Image img;
    public List<Sprite> sprites;
    public bool animated;
    public bool once;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        time = Time.time;
        if (!animated || gameObject.name == "Awaken")
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
        if (Time.time - time >= 2.5f && GlobalControl.area == "Ending_1")
        {
            if (gameObject.name == "Gaze Above" || gameObject.name == "Star")
                img.color = new Color(1f, 1f, 1f, 0f);
            else if (gameObject.name == "Blast Off" && !once)
            {
                once = true;
                GetComponent<Animator>().Play("Blast Off", -1, 0.0f);
            }
        }
        else if (Time.time - time >= 1.5f && GlobalControl.area == "Ending_2")
        {
            if (gameObject.name == "Freedom")
                img.color = new Color(1f, 1f, 1f, 0f);
            else if (gameObject.name == "Awaken" && !once)
            {
                once = true;
                img.color = new Color(1f, 1f, 1f, 1f);
                GetComponent<Animator>().Play("Awaken", -1, 0.0f);
            }
        }
        else if (Time.time - time >= 1.8f && GlobalControl.area == "Ending_3")
        {
            if (gameObject.name == "Exploding")
                img.color = new Color(1f, 1f, 1f, 0f);
            else if (gameObject.name == "Explosion" && !once)
            {
                once = true;
                GetComponent<Animator>().Play("Explosion", -1, 0.0f);
            }
        }
    }
}
