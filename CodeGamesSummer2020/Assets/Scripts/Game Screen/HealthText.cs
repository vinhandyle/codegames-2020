using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text text;
    public bool boss;

    // Start is called before the first frame update
    void Start()
    {
        if(boss)
            text = GameObject.Find("HP Ind").GetComponent<Text>();
        else
            text = GameObject.Find("Health").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Boss HP")
        {
            if ((GlobalControl.area == "SG_12" && GlobalControl.downed_boss_1) ||
                (GlobalControl.area == "TT_12" && GlobalControl.downed_boss_2) ||
                (GlobalControl.area == "MB_12" && GlobalControl.downed_boss_3) ||
                (GlobalControl.area == "GP_0B" && GlobalControl.downed_boss_4))
                gameObject.SetActive(false);
        }

        if (MenuBackground.inMenu)
        {
            text.text = "";
        }
        else if (gameObject.name == "Health")
        {
            text.text = "<b>Health:</b> " + GlobalControl.healthCurr + " / " + GlobalControl.healthMax;
        }
        else if (gameObject.name == "HP Ind")
        {
            text.text = "<b>Boss Health: </b>" + Obstacles.hp_Curr + " / " + Obstacles.hp_Max;
        }
    }
}
