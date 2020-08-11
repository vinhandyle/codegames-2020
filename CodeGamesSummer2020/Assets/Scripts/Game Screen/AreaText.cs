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
        if (MenuBackground.inMenu)
        {
            text.text = "";
        }
        else
        {
            // Starting Area
            if (GlobalControl.area == "Start_")
            {
                text.text = "Origin";
            }

            // Institute of Technology
            else if (GlobalControl.area == "IT_1")
            {
                text.text = "Homecoming";
            }
            else if (GlobalControl.area == "IT_1S")
            {
                text.text = "Meltdown";
            }
            else if (GlobalControl.area == "IT_2")
            {
                text.text = "Service Arm";
            }

            // Dreg Heap
            else if (GlobalControl.area == "DH_1")
            {
                text.text = "Descent";
            }
            else if (GlobalControl.area == "DH_2")
            {
                text.text = "Death Basin";
            }
            else if (GlobalControl.area == "DH_3")
            {
                text.text = "Crawlway";
            }
            else if (GlobalControl.area == "DH_4")
            {
                text.text = "Encampment";
            }
            else if (GlobalControl.area == "DH_5")
            {
                text.text = "Sludge Pond";
            }
            else if (GlobalControl.area == "DH_5S") // Wall jump and dash challenge
            {
                text.text = "Rotten Depths";
            }
            else if (GlobalControl.area == "DH_6")
            {
                text.text = "Waste Deposit";
            }
            else if (GlobalControl.area == "DH_7")
            {
                text.text = "Mysterious Path";
            }
            else if (GlobalControl.area == "DH_8")
            {
                text.text = "Injection Point";
            }

            // Sunset Garden
            else if (GlobalControl.area == "SG_1")
            {
                text.text = "Lower Disposal Area";
            }
            else if (GlobalControl.area == "SG_2")
            {
                text.text = "Central Disposal Area";
            }
            else if (GlobalControl.area == "SG_2S") // Kin
            {
                text.text = "Upper Disposal Area";
            }
            else if (GlobalControl.area == "SG_3")
            {
                text.text = "Lower Prep Area";
            }
            else if (GlobalControl.area == "SG_4")
            {
                text.text = "Upper Prep Area";
            }
            else if (GlobalControl.area == "SG_5")
            {
                text.text = "Assembly Line";
            }
            else if (GlobalControl.area == "SG_6")
            {
                text.text = "Disassembly Line";
            }
            else if (GlobalControl.area == "SG_7")
            {
                text.text = "Quality Control";
            }
            else if (GlobalControl.area == "SG_8")
            {
                text.text = "Intake";
            }
            else if (GlobalControl.area == "SG_9")
            {
                text.text = "Storage Area";
            }
            else if (GlobalControl.area == "SG_9S") // Plating
            {
                text.text = "Storage Depths";
            }
            else if (GlobalControl.area == "SG_10")
            {
                text.text = "Back End";
            }
            else if (GlobalControl.area == "SG_11")
            {
                text.text = "Overpass";
            }
            else if (GlobalControl.area == "SG_11S") // Kin
            {
                text.text = "Garden Heights";
            }
            else if (GlobalControl.area == "SG_12")
            {
                text.text = "Corner Office";
            }

            // Twilight Town
            else if (GlobalControl.area == "TT_1") // Battery
            {
                text.text = "Outer Garden";
            }


            // Grey Palace
            else if (GlobalControl.area == "GP_0A")
            {
                text.text = "Audience Chamber";
            }
            else if (GlobalControl.area == "GP_0B")
            {
                text.text = "The Summit";
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
