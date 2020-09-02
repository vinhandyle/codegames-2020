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
            else if (GlobalControl.area == "IT_3")
            {
                text.text = "The Lift From Hell";
            }
            else if (GlobalControl.area == "IT_4")
            {
                text.text = "Security Breach";
            }
            else if (GlobalControl.area == "IT_5")
            {
                text.text = "Defense Line";
            }
            else if (GlobalControl.area == "IT_6")
            {
                text.text = "Research Center";
            }
            else if (GlobalControl.area == "IT_7")
            {
                text.text = "Lightning Hall";
            }
            else if (GlobalControl.area == "IT_8")
            {
                text.text = "Roundabout";
            }
            else if (GlobalControl.area == "IT_9")
            {
                text.text = "Convergence";
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
            else if (GlobalControl.area == "DH_5S") 
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
            else if (GlobalControl.area == "SG_2S") 
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
            else if (GlobalControl.area == "SG_9S")
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
            else if (GlobalControl.area == "SG_11S") 
            {
                text.text = "Garden Heights";
            }
            else if (GlobalControl.area == "SG_12")
            {
                text.text = "Corner Office";
            }

            // Twilight Town
            else if (GlobalControl.area == "TT_1") 
            {
                text.text = "Outer Garden";
            }
            else if (GlobalControl.area == "TT_2") 
            {
                text.text = "Central Plaza";
            }
            else if (GlobalControl.area == "TT_4")
            {
                text.text = "City Intersection";
            }
            else if (GlobalControl.area == "TT_5")
            {
                text.text = "Residential Section";
            }
            else if (GlobalControl.area == "TT_6")
            {
                text.text = "Dead End";
            }
            else if (GlobalControl.area == "TT_6S")
            {
                text.text = "Secluded Area";
            }
            else if (GlobalControl.area == "TT_7")
            {
                text.text = "Alley Way";
            }
            else if (GlobalControl.area == "TT_7S")
            {
                text.text = "Canopy";
            }
            else if (GlobalControl.area == "TT_8")
            {
                text.text = "Azimuth Hall";
            }
            else if (GlobalControl.area == "TT_9") 
            {
                text.text = "Loft";
            }
            else if (GlobalControl.area == "TT_10")
            {
                text.text = "Breakout";
            }
            else if (GlobalControl.area == "TT_11")
            {
                text.text = "Closed-Off Section";
            }
            else if (GlobalControl.area == "TT_12")
            {
                text.text = "Machina Vault";
            }
            else if (GlobalControl.area == "TT_13")
            {
                text.text = "Main Station";
            }
            else if (GlobalControl.area == "TT_14")
            {
                text.text = "Vacuum Pod";
            }
            else if (GlobalControl.area == "TT_14S") 
            {
                text.text = "Far Station";
            }
            else if (GlobalControl.area == "TT_15")
            {
                text.text = "Seaside Station";
            }
            else if (GlobalControl.area == "TT_16")
            {
                text.text = "City Outskirts";
            }

            // Midnight Bay
            else if (GlobalControl.area == "MB_1")
            {
                text.text = "Stormy Beach";
            }
            else if (GlobalControl.area == "MB_2")
            {
                text.text = "Great Maw";
            }
            else if (GlobalControl.area == "MB_3")
            {
                text.text = "Hull";
            }
            else if (GlobalControl.area == "MB_3S") 
            {
                text.text = "Helm";
            }
            else if (GlobalControl.area == "MB_3S2") 
            {
                text.text = "Stowaway";
            }
            else if (GlobalControl.area == "MB_4")
            {
                text.text = "Far Northern Arm";
            }
            else if (GlobalControl.area == "MB_5")
            {
                text.text = "Northern Arm";
            }
            else if (GlobalControl.area == "MB_6")
            {
                text.text = "Eastern Arm";
            }
            else if (GlobalControl.area == "MB_7")
            {
                text.text = "Far Eastern Arm";
            }
            else if (GlobalControl.area == "MB_8")
            {
                text.text = "Far Western Arm";
            }
            else if (GlobalControl.area == "MB_9")
            {
                text.text = "Western Arm";
            }
            else if (GlobalControl.area == "MB_10")
            {
                text.text = "Southern Arm";
            }
            else if (GlobalControl.area == "MB_11")
            {
                text.text = "Far Southern Arm";
            }
            else if (GlobalControl.area == "MB_12")
            {
                text.text = "Heart of the Beast";
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
