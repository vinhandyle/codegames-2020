using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogue : MonoBehaviour
{
    private Text text;

    public string type;
    public bool t;

    // Start is called before the first frame update
    void Start()
    {
        if (type == "intro")
        {
            if(t)
                text = GameObject.Find("Intro").GetComponent<Text>();

            if (GlobalControl.intro)
                gameObject.SetActive(false);
        }
        else if (type == "dialogue")
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
        else if (type == "free")
        {
            text = GameObject.Find("FreeText").GetComponent<Text>();
        }
        else if (type == "kill")
        {
            text = GameObject.Find("KillText").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu)
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
                    text.text = "I've been waiting for a very long time.";
                }
                else if (GlobalControl.counter_1 == 2)
                {
                    text.text = "Come. Let us save humanity together.";
                }
                else if (GlobalControl.counter_1 == 3)
                {
                    text.text = "You will need this in order to capture the rest of them.\n\nPicked up: Gentle Reactor";
                }
                else if (GlobalControl.counter_1 == 4)
                {
                    text.text = "What a shame.\n\n<b>EXECUTING TASK MANAGER...</b>";
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
            else if (GlobalControl.area == "GP_0B")
            {
                if (type == "free" && GlobalControl.masterControl)
                    text.text = "Shut Down the Ark";
                else if (type == "kill" && GlobalControl.masterControl && GlobalControl.data == 100)
                    text.text = "Blow Up the Ark";
            }
        }
    }
}
