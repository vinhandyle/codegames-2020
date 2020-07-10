using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectiveText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Directive").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "Objective: ";
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
        else
        {
            text.text = "";
        }
    }
}
