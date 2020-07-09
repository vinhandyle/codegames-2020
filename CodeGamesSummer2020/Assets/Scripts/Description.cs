using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private Text text;
    public static string descOf = ""; // Description of clicked icon

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Description").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu && !Map.mapOpen && !InfoBtn.infoPage)
        {
            if (descOf == "")                                                  // Empty description
            {
                text.text = "";
            }
            else if (descOf == "battery" && GlobalControl.batteryUnlocked)     // Battery description
            {
                text.text = "Battery \n\n" +
                            "Stores the energy you collected. " +
                            "<i>Quite hefty.</i>\n\n" +
                            "Max capacity: " + GlobalControl.energyMax;
            }
            else if (descOf == "solar" && GlobalControl.solarUnlocked)         // Solar Panel description
            {
                text.text = "Solar Panel \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "geothermal" && GlobalControl.geoUnlocked)      // Geothermal Extractor description
            {
                text.text = "Geothermal Extractor \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "dash" && GlobalControl.dashUnlocked)           // Dash item description
            {
                text.text = "Booster Rocket \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "cling" && GlobalControl.clingUnlocked)         // Wall jump item description
            {
                text.text = "Climbing Claws \n\n" +
                            "<i></i>";
            }
            else if (descOf == "double" && GlobalControl.doubleUnlocked)       // Double jump item description
            {
                text.text = "Booster Rocket MK2 \n\n" +
                            "<i>To new heights!</i>";
            }
            else if (descOf == "gun" && GlobalControl.gunUnlocked)             // Energy Cannon description
            {
                text.text = "Energy Cannon \n\n" +
                            "<i>A means to an end.</i>";
            }
            else if (descOf == "map" && GlobalControl.mapUnlocked)             // Map item description
            {
                text.text = "Navigational Module \n\n" +
                            "<i></i>";
            }
            else if (descOf == "heartless" && GlobalControl.heartlessUnlocked) // Health-Energy item description
            {
                text.text = "Heartless Generator \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "scrap" && GlobalControl.scrapFound)            // Hyper Scrap description
            {
                text.text = "Hyper Scrap \n\n" +
                            "Rare parts scavenged from the Empire's strongest. Can be used to craft equipment. " +
                            "<i>Devour the strong.</i>\n\n" +
                            "In Possession: " + GlobalControl.scrapNum;
            }
            else if (descOf == "extra" && GlobalControl.extraFound)            // Extra Battery description
            {
                text.text = "Extra Battery \n\n";
            }
            else if (descOf == "plating" && GlobalControl.plateFound)          // Special Plating description
            {
                text.text = "Special Plating \n\n";
            }
            else if (descOf == "basic" && GlobalControl.basicUnlocked)         // Basic Reactor description
            {
                text.text = "Basic Reactor \n\n" +
                            "<i></i> \n\n" +
                            "Damage: " + (1 + GlobalControl.scrapNum) + "  Energy Use: 1";
            }
            else if (descOf == "imperial" && GlobalControl.imperialUnlocked)   // Imperial Reactor description
            {
                text.text = "Strange Reactor \n\n" +
                            "<i></i> \n\n" +
                            "Damage: 0 Energy Use: 1";
            }
            else if (descOf == "familiar" && GlobalControl.familiarUnlocked)   // Familiar Reactor description
            {
                text.text = "Lost Reactor \n\n" +
                            "<i>Family inheritance.</i> \n\n" +
                            "Damage: " + (0 + GlobalControl.data / 10) + " Energy Use: 2";
            }
            else if (descOf == "unstable" && GlobalControl.unstableUnlocked)   // Unstable Reactor description
            {
                text.text = "Unstable Reactor \n\n" +
                            "<i>Could explode at any moment.</i> \n\n" +
                            "Damage: 10 Energy Use: 1";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
