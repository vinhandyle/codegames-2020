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
            if (GlobalControl.area == "GP_0A")
            {
                GlobalControl.counter_1++;
            }
        }
        else if (type == "positive")
        {
            if (GlobalControl.area == "GP_0A")
            {
                GlobalControl.counter_1 = 5;
            }
        }
        else if (type == "negative")
        {
            if (GlobalControl.area == "GP_0A")
            {
                GlobalControl.counter_1 = 6;
            }
        }
    }
}
