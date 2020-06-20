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
            if (descOf == "") // Empty description
            {
                text.text = "";
            }
            else if (descOf == "battery" && Player.batteryUnlocked) // Battery description
            {
                text.text = "Battery";
            }
            else if (descOf == "solar" && Player.solarUnlocked) // Solar Panel description
            {
                text.text = "Solar Panel";
            }
            else if (descOf == "geothermal" && Player.geoUnlocked) // Geothermal Extractor description
            {
                text.text = "Geothermal Extractor";
            }
            else if (descOf == "dash" && Player.dashUnlocked) // Dash item description
            {
                text.text = "Booster Rocket";
            }
            else if (descOf == "cling" && Player.clingUnlocked) // Wall jump item description
            {
                text.text = "Climbing Claws";
            }
            else if (descOf == "double" && Player.doubleUnlocked) // Double jump item description
            {
                text.text = "Booster Rocket MK2";
            }
            else if (descOf == "gun" && PointAndShoot.gunUnlocked) // Energy Cannon description
            {
                text.text = "Energy Cannon \n\n" +
                            "A means to an end.";
            }
            else if (descOf == "map" && Map.mapUnlocked) // Map item description
            {
                text.text = "Navigational Module";
            }
            else if (descOf == "heartless" && Player.heartlessUnlocked) // Health-Energy item description
            {
                text.text = "Heartless Generator";
            }
            else if (descOf == "scrap" && ScrapNum.scrapFound) // Hyper Scrap description
            {
                text.text = "Hyper Scrap";
            }
            else if (descOf == "basic" && Player.basicUnlocked) // Basic Reactor description
            {
                text.text = "Basic Reactor";
            }
            else if (descOf == "imperial" && Player.imperialUnlocked) // Imperial Reactor description
            {
                text.text = "Imperial Reactor";
            }
            else if (descOf == "familiar" && Player.familiarUnlocked) // Familiar Reactor description
            {
                text.text = "Familiar Reactor";
            }
            else if (descOf == "unstable" && Player.unstableUnlocked) // Unstable Reactor description
            {
                text.text = "Unstable Reactor";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
