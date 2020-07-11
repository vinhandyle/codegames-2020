using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Area").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "";
        }
        else
        {
            // Starting Area
            if (GlobalControl.area == "Start_")
            {
                text.text = "Start";
            }

            // Institute of Technology
            else if (GlobalControl.area == "IT_1")
            {
                text.text = "Homecoming";
            }
            else if (GlobalControl.area == "IT_2")
            {
                text.text = "Service Arm";
            }
            else if (GlobalControl.area == "IT_1S")
            {
                text.text = "Meltdown";
            }

            // Dreg Heap
            else if (GlobalControl.area == "DH_1")
            {
                text.text = "Death Basin";
            }

            // Grey Palace
            else if (GlobalControl.area == "GP_0")
            {
                text.text = "Final Destination";
            }
            else if (GlobalControl.area == "GP_1")
            {
                text.text = "Palace Entrance";
            }
            else if (GlobalControl.area == "GP_2")
            {
                text.text = "The Lift to Heaven";
            }

            // Not In-game
            else
            {
                text.text = GlobalControl.area;
            }
        }
    }
}
