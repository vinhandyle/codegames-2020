using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuText : MonoBehaviour
{
    private Text text;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == "title")
        {
            text = GameObject.Find("Title").GetComponent<Text>();
        }
        else if (type == "credit")
        {
            text = GameObject.Find("Credit").GetComponent<Text>();
        }
        else if (type == "exit")
        {
            text = GameObject.Find("ExitText").GetComponent<Text>();
        }
        else if (type == "start")
        {
            text = GameObject.Find("StartText").GetComponent<Text>();
        }
        else if (type == "continue")
        {
            text = GameObject.Find("ContiText").GetComponent<Text>();
        }
        else if (type == "controls")
        {
            text = GameObject.Find("ControlsText").GetComponent<Text>();
        }
        else if (type == "trophy")
        {
            text = GameObject.Find("TrophyText").GetComponent<Text>();
        }
        else if (type == "trophy1")
        {
            text = GameObject.Find("TrophyDesc_1").GetComponent<Text>();
        }
        else if (type == "trophy2")
        {
            text = GameObject.Find("TrophyDesc_2").GetComponent<Text>();
        }
        else if (type == "trophy3")
        {
            text = GameObject.Find("TrophyDesc_3").GetComponent<Text>();
        }
        else if (type == "trophy4")
        {
            text = GameObject.Find("TrophyDesc_4").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenuBtn.inControl || MainMenuBtn.inTrophy)
        {
            if (type == "exit")
            {
                text.text = "Exit";
            }
            else if (type.Length == 7 && MainMenuBtn.inTrophy)
            {
                if (type == "trophy1" && GlobalControl.complete)
                {
                    text.text = "<b>Alpha and Omega</b> \n" +
                                "Get all endings. \n" +
                                "<i>The beginning and the end.</i>";
                }
                else if (type == "trophy2" && GlobalControl.ending_1)
                {
                    text.text = "<b>Save Humanity</b> \n" +
                                "Capture the remaining humans. \n" +
                                "<i>To infinity and beyond!</i>";
                }
                else if (type == "trophy3" && GlobalControl.ending_2)
                {
                    text.text = "<b>Return to the Past</b> \n" +
                                "Free the remaining humans. \n" +
                                "<i>Reject modernity. Embrace tradition.</i>";
                }
                else if (type == "trophy4" && GlobalControl.ending_3)
                {
                    text.text = "<b>End the Cycle</b> \n" +
                                "Kill the remaining humans. \n" +
                                "<i>No humans. No masters.</i>";
                }
            }
            else
            {
                text.text = "";
            }
        }
        else if (MainMenuBtn.confirmNew)
        {
            if (type == "title")
            {
                text.text = "Salvation"; 
            }
            else if (type == "credit")
            {
                text.text = "By: Soxar";
            }
            else if (type == "start")
            {
                text.text = "Confirm Action";
            }
            else if (type == "exit")
            {
                text.text = "Exit";
            }
            else
            {
                text.text = "";
            }
        }
        else
        {
            if (type == "title")
            {
                text.text = "Salvation"; 
            }
            else if (type == "credit")
            {
                text.text = "By: Soxar";
            }
            else if (type == "start")
            {
                    text.text = "New Game";
            }
            else if (type == "continue" && GlobalControl.canContinue)
            {
                text.text = "Continue";
            }
            else if (type == "controls")
            {
                text.text = "Controls";
            }
            else if (type == "trophy")
            {
                text.text = "Achievements";
            }
            else
            {
                text.text = "";
            }
        }
    }
}
