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
            if ((gameObject.name == "BatteryBtn" && GlobalControl.batteryUnlocked) ||
                (gameObject.name == "SolarBtn" && GlobalControl.solarUnlocked) ||
                (gameObject.name == "GeothermalBtn" && GlobalControl.geoUnlocked) ||
                (gameObject.name == "GunBtn" && GlobalControl.gunUnlocked) ||
                (gameObject.name == "NavBtn" && GlobalControl.mapUnlocked) ||
                (gameObject.name == "HeartlessBtn" && GlobalControl.heartlessUnlocked) ||
                (gameObject.name == "DashBtn" && GlobalControl.dashUnlocked) ||
                (gameObject.name == "ClingBtn" && GlobalControl.clingUnlocked) ||
                (gameObject.name == "DoubleBtn" && GlobalControl.doubleUnlocked) ||
                (gameObject.name == "BasicBtn" && GlobalControl.basicUnlocked) ||
                (gameObject.name == "ImperialBtn" && GlobalControl.imperialUnlocked) ||
                (gameObject.name == "FamiliarBtn" && GlobalControl.familiarUnlocked) ||
                (gameObject.name == "UnstableBtn" && GlobalControl.unstableUnlocked) ||
                (gameObject.name == "ScrapBtn" && GlobalControl.scrapFound))
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
