using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBtnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress(string type)
    {
        if (type == "next")
        {
            if (GlobalControl.area == "GP_0A" && GlobalControl.counter_1 < 2)
            {
                GlobalControl.counter_1++;
            }
        }
        else if (type == "positive" && GlobalControl.counter_1 == 2)
        {
            if (GlobalControl.area == "GP_0A")
            {
                GlobalControl.counter_1 = 3;
                GlobalControl.imperialUnlocked = true;
                GlobalControl.reactor = "imperial";
            }
        }
        else if (type == "negative" && GlobalControl.counter_1 == 2)
        {
            if (GlobalControl.area == "GP_0A")
            {
                GlobalControl.counter_1 = 4;
            }
        }
    }
}
