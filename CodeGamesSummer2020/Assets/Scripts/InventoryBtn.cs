using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBtn : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu && !Map.mapOpen && !InfoBtn.infoPage)
        {
            if ((gameObject.name == "BatteryBtn" && Player.batteryUnlocked) ||
                (gameObject.name == "SolarBtn" && Player.solarUnlocked) ||
                (gameObject.name == "GeothermalBtn" && Player.geoUnlocked) ||
                (gameObject.name == "GunBtn" && PointAndShoot.gunUnlocked) ||
                (gameObject.name == "NavBtn" && Map.mapUnlocked) ||
                (gameObject.name == "HeartlessBtn" && Player.heartlessUnlocked) ||
                (gameObject.name == "DashBtn" && Player.dashUnlocked) ||
                (gameObject.name == "ClingBtn" && Player.clingUnlocked) ||
                (gameObject.name == "DoubleBtn" && Player.doubleUnlocked) ||
                (gameObject.name == "BasicBtn" && Player.basicUnlocked) ||
                (gameObject.name == "ImperialBtn" && Player.imperialUnlocked) ||
                (gameObject.name == "FamiliarBtn" && Player.familiarUnlocked) ||
                (gameObject.name == "UnstableBtn" && Player.unstableUnlocked) ||
                (gameObject.name == "ScrapBtn" && Description.scrapFound))
            {
                img.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
