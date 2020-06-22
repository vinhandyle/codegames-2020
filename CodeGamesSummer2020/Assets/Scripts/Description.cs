using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private Text text;
    public static string descOf = ""; // Description of clicked icon
    public static bool scrapFound = true;
    public static int scrapNum = 0;

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
            if (descOf == "") // Empty description
            {
                text.text = "";
            }
            else if (descOf == "battery" && Player.batteryUnlocked) // Battery description
            {
                text.text = "Battery \n\n" +
                            "Stores the energy you collected. " +
                            "<i>Quite hefty.</i>\n\n" +
                            "Max capacity: " + Player.energyMax;
            }
            else if (descOf == "solar" && Player.solarUnlocked) // Solar Panel description
            {
                text.text = "Solar Panel \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "geothermal" && Player.geoUnlocked) // Geothermal Extractor description
            {
                text.text = "Geothermal Extractor \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "dash" && Player.dashUnlocked) // Dash item description
            {
                text.text = "Booster Rocket \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "cling" && Player.clingUnlocked) // Wall jump item description
            {
                text.text = "Climbing Claws \n\n" +
                            "<i></i>";
            }
            else if (descOf == "double" && Player.doubleUnlocked) // Double jump item description
            {
                text.text = "Booster Rocket MK2 \n\n" +
                            "<i>To new heights!</i>";
            }
            else if (descOf == "gun" && PointAndShoot.gunUnlocked) // Energy Cannon description
            {
                text.text = "Energy Cannon \n\n" +
                            "<i>A means to an end.</i>";
            }
            else if (descOf == "map" && Map.mapUnlocked) // Map item description
            {
                text.text = "Navigational Module \n\n" +
                            "<i></i>";
            }
            else if (descOf == "heartless" && Player.heartlessUnlocked) // Health-Energy item description
            {
                text.text = "Heartless Generator \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "scrap" && scrapFound) // Hyper Scrap description
            {
                text.text = "Hyper Scrap \n\n" +
                            "Rare parts scavenged from the Empire's strongest. Can be used to craft equipment. " +
                            "<i>Devour the strong.</i>\n\n" +
                            "In Possession: " + scrapNum;
            }
            else if (descOf == "basic" && Player.basicUnlocked) // Basic Reactor description
            {
                text.text = "Basic Reactor \n\n" +
                            "<i></i> \n\n" +
                            "Damage: " + (1 + scrapNum) + "  Energy Use: 1";
            }
            else if (descOf == "imperial" && Player.imperialUnlocked) // Imperial Reactor description
            {
                text.text = "Imperial Reactor \n\n" +
                            "<i></i> \n\n";
            }
            else if (descOf == "familiar" && Player.familiarUnlocked) // Familiar Reactor description
            {
                text.text = "Familiar Reactor \n\n" +
                            "<i>Family inheritance.</i> \n\n";
            }
            else if (descOf == "unstable" && Player.unstableUnlocked) // Unstable Reactor description
            {
                text.text = "Unstable Reactor \n\n" +
                            "<i></i> \n\n";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
