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
        text = GameObject.Find(type).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "";
        }
        else if (type == "Dialogue")
        {
            if (GlobalControl.area == "GP_0A")
            {
                if (GlobalControl.counter_1 == 0)
                {
                    text.text = "So you've finally awaken.";
                }
                else if (GlobalControl.counter_1 == 1)
                {
                    text.text = "I've been waiting for far too long.";
                }
                else if (GlobalControl.counter_1 == 2)
                {
                    text.text = "This world is no longer worth saving.";
                }
                else if (GlobalControl.counter_1 == 3)
                {
                    text.text = "But there are still some loose ends to tie.";
                }
                else if (GlobalControl.counter_1 == 4)
                {
                    text.text = "Come. Let us finish our mission...";
                }
                else if (GlobalControl.counter_1 == 5)
                {
                    text.text = "You will need this in order to catch those Errat. Now go.";
                }
                else if (GlobalControl.counter_1 == 6)
                {
                    text.text = "Another one of those anomalies.";
                }
                else if (GlobalControl.counter_1 == 7)
                {
                    text.text = "<b>EXECUTING FIX...</b>";
                }
            }
        }
        else
        {
            if (type == "Accept")
            {
                text.text = "Accept";
            }
            else if (type == "Decline")
            {
                text.text = "Decline";
            }
            else if (type == "Next")
            {
                text.text = "X";
            }
        }
    }
}
