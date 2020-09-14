using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectiveText : MonoBehaviour
{
    private Text text;

    public bool alt;

    // Start is called before the first frame update
    void Start()
    {
        if(alt)
            text = GameObject.Find("Resistance").GetComponent<Text>();
        else
            text = GameObject.Find("Directive").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu)
        {
            if (alt)
            {
                if (GlobalControl.doubleUnlocked)
                {
                    text.text = "<b>Intrusion-Proof:</b> 100%";
                    text.color = new Color(255, 170, 0);
                }
                else
                {
                    text.text = "<b>Intrusion-Proof:</b> 50%";
                    text.color = new Color(255, 170, 0);
                }
            }
            else
            {
                if (GlobalControl.counter_1 == 5)
                {
                    text.text = "<b>Errats Left: " + GlobalControl.humansLeft;
                }
                else
                {
                    text.text = "<b>Objective:</b> ";
                    if (GlobalControl.data < 50)
                    {
                        text.text += "Save Humanity";
                    }
                    else if (GlobalControl.data < 100)
                    {
                        text.text += "...Humanity";
                    }
                    else
                    {
                        text.text += "Eradicate Humanity";
                    }
                }
            }            
        }
        else
        {
            text.text = "";
        }
    }
}
