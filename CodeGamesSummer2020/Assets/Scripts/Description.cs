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
                text.text = "<b>Battery</b> \n" +
                            "Stores the energy you collected.\n" +
                            "Max capacity: " + GlobalControl.energyMax +
                            "\n\n<i>External energy storage. On its exterior, there are faded markings spelling out: E&@.</i>";
            }
            else if (descOf == "solar" && GlobalControl.solarUnlocked)         // Solar Panel description
            {
                text.text = "<b>Solar Panel</b> \n" +
                            "Generates energy while under sunlight.\n" +
                            "Generates 1 energy per second.\n\n" +
                            "<i>Praise the Sun.</i>";
            }
            else if (descOf == "geothermal" && GlobalControl.geoUnlocked)      // Geothermal Extractor description
            {
                text.text = "<b>Geothermal Extractor</b> \n" +
                            "Generates energy while above a heat vent.\n" +
                            "Generates 5 energy per second.\n\n" +
                            "<i></i>";
            }
            else if (descOf == "dash" && GlobalControl.dashUnlocked)           // Dash item description
            {
                text.text = "<b>Booster Rocket</b> \n" +
                            "Grants the ability to dash along the ground or through the air.\n\n" +
                            "<i>Constructed from a Hyper Scrap. </i>";
            }
            else if (descOf == "cling" && GlobalControl.clingUnlocked)         // Wall jump item description
            {
                text.text = "<b>Climbing Claws</b> \n" +
                            "Grants the ability to cling to and leap off from walls.\n\n" +
                            "<i>Constructed from a Hyper Scrap. </i>";
            }
            else if (descOf == "double" && GlobalControl.doubleUnlocked)       // Double jump item description
            {
                text.text = "<b>Booster Rocket MK2</b> \n" +
                            "Grants the ability to jump again in mid-air.\n\n" +
                            "<i>Constructed from a Hyper Scrap. </i>";
            }
            else if (descOf == "gun" && GlobalControl.gunUnlocked)             // Energy Cannon description
            {
                text.text = "<b>Energy Cannon</b> \n" +
                            "Uses energy to fire an energy pellet.\n\n" +
                            "<i>A means to an end.</i>";
            }
            else if (descOf == "map" && GlobalControl.mapUnlocked)             // Map item description
            {
                text.text = "<b>Navigational Module</b> \n" +
                            "\n\n" +
                            "<i></i>";
            }
            else if (descOf == "heartless" && GlobalControl.heartlessUnlocked) // Health-Energy item description
            {
                text.text = "<b>Heartless Generator</b> \n" +
                            "\n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "scrap" && GlobalControl.scrapFound)            // Hyper Scrap description
            {
                text.text = "<b>Hyper Scrap</b> \n" +
                            "Rare parts scavenged from the Empire's strongest. Can be used to craft equipment. \n" +
                            "In Possession: " + GlobalControl.scrapNum +
                            "\n\n<i>Devour the strong.</i>";
            }
            else if (descOf == "extra" && GlobalControl.extraFound)            // Extra Battery description
            {
                text.text = "<b>Extra Battery</b> \n" +
                            "\n\n" +
                            "<i></i>";
            }
            else if (descOf == "plating" && GlobalControl.plateFound)          // Special Plating description
            {
                text.text = "<b>Special Plating</b> \n" +
                            "\n\n" +
                            "<i></i>";
            }
            else if (descOf == "basic" && GlobalControl.basicUnlocked)         // Basic Reactor description
            {
                text.text = "<b>Basic Reactor</b> ";
                if (GlobalControl.reactor == "basic")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: " + (1 + GlobalControl.scrapNum) + "  Energy Use: 1 \n\n" +
                             "<i></i>";
            }
            else if (descOf == "imperial" && GlobalControl.imperialUnlocked)   // Imperial Reactor description
            {
                text.text = "<b>Strange Reactor</b> ";
                if (GlobalControl.reactor == "imperial")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: 0  Energy Use: 1 \n\n" +
                            "<i>Constrain and detain.</i>";
            }
            else if (descOf == "familiar" && GlobalControl.familiarUnlocked)   // Familiar Reactor description
            {
                text.text = "<b>Lost Reactor</b> ";
                if (GlobalControl.reactor == "familiar")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: " + (0 + GlobalControl.data / 10) + "  Energy Use: 2 \n\n" +
                            "<i>Family inheritance.</i>";
            }
            else if (descOf == "unstable" && GlobalControl.unstableUnlocked)   // Unstable Reactor description
            {
                text.text = "<b>Unstable Reactor</b> ";
                if (GlobalControl.reactor == "unstable")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: 10  Energy Use: 1 \n\n" +
                            "<i>Could explode at any moment.</i>";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
