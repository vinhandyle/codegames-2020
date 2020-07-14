using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogue : MonoBehaviour
{
    private Text text;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == "dialogue")
        {
            text = GameObject.Find("Dialogue").GetComponent<Text>();
        }
        else if (type == "accept")
        {
            text = GameObject.Find("AcceptText").GetComponent<Text>();
        }
        else if (type == "decline")
        {
            text = GameObject.Find("DeclineText").GetComponent<Text>();
        }
        else if (type == "next")
        {
            text = GameObject.Find("NextText").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "";
        }
        else if (type == "dialogue")
        {
            if (GlobalControl.area == "GP_0A")
            {
                if (GlobalControl.counter_1 == 0)
                {
                    text.text = "So, you've finally awaken.";
                }
                else if (GlobalControl.counter_1 == 1)
                {
                    text.text = "I've been waiting for far too long.";
                }
                else if (GlobalControl.counter_1 == 2)
                {
                    text.text = "Come. Let us finish our mission...";
                }
                else if (GlobalControl.counter_1 == 3)
                {
                    text.text = "You will need this in order to catch the Errat.\n\nPicked up: Gentle Reactor";
                }
                else if (GlobalControl.counter_1 == 4)
                {
                    text.text = "Another anomaly to be resolved.\n\n<b>EXECUTING TASK MANAGER...</b>";
                }
            }
        }
        else
        {
            if (GlobalControl.area == "GP_0A")
            {
                if (type == "accept" && GlobalControl.counter_1 == 2)
                {
                    text.text = "Accept";
                }
                else if (type == "decline" && GlobalControl.counter_1 == 2)
                {
                    text.text = "Decline";
                }
                else if (type == "next" && GlobalControl.counter_1 < 2)
                {
                    text.text = "X";
                }
                else
                {
                    text.text = "";
                }
            }
        }
    }
}
