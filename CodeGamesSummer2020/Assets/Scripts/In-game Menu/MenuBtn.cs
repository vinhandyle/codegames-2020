using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    Image img;

    public Sprite item;
    public Sprite enemy;
    public Sprite report;

    public Sprite extra_1;
    public Sprite extra_2;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (MenuBackground.inMenu)
        {
            // Section Headers
            if (gameObject.name == "InvBtn" ||
                gameObject.name == "InfoBtn" ||
                (gameObject.name == "MapBtn" && GlobalControl.mapUnlocked) ||
                (gameObject.name == "EnemyBtn" && GlobalControl.counter_1 > 0) ||
                (gameObject.name == "RepBtn" && GlobalControl.data > 0))
            {
                img.color = new Color(1f, 1f, 1f, 1f);
            }
            // Inventory
            else if (GlobalControl.menu == "inventory" &&
                    ((gameObject.name == "Btn_1" && GlobalControl.batteryUnlocked) ||
                    (gameObject.name == "Btn_2" && GlobalControl.solarUnlocked) ||
                    (gameObject.name == "Btn_3" && GlobalControl.geoUnlocked) ||
                    (gameObject.name == "Btn_4" && GlobalControl.heartlessUnlocked) ||
                    (gameObject.name == "Btn_5" && GlobalControl.mapUnlocked) ||
                    (gameObject.name == "Btn_6" && GlobalControl.dashUnlocked) ||
                    (gameObject.name == "Btn_7" && GlobalControl.clingUnlocked) ||
                    (gameObject.name == "Btn_8" && GlobalControl.doubleUnlocked) ||
                    (gameObject.name == "Btn_9" && GlobalControl.gunUnlocked) ||
                    (gameObject.name == "Btn_10" && GlobalControl.basicUnlocked) ||
                    (gameObject.name == "Btn_11" && GlobalControl.imperialUnlocked) ||
                    (gameObject.name == "Btn_12" && GlobalControl.familiarUnlocked) ||
                    (gameObject.name == "Btn_13" && GlobalControl.unstableUnlocked) ||
                    (gameObject.name == "Btn_14" && GlobalControl.plateFound) ||
                    (gameObject.name == "Btn_15" && GlobalControl.extraFound) ||
                    (gameObject.name == "Btn_16" && GlobalControl.scrapFound)))
            {
                img.color = new Color(1f, 1f, 1f, 1f);
                if (gameObject.name == "Btn_12")
                {
                    if (GlobalControl.data == 100)
                    {
                        img.sprite = extra_2;
                    }
                    else if (GlobalControl.data >= 50)
                    {
                        img.sprite = extra_1;
                    }
                    else
                    {
                        img.sprite = item;
                    }
                }
                else
                {
                    img.sprite = item;
                }
            }

            // Enemies
            else if (GlobalControl.menu == "enemies" &&
                ((gameObject.name == "Btn_1" && GlobalControl.downed_patrol) ||
                (gameObject.name == "Btn_2" && GlobalControl.downed_pursuit) ||
                (gameObject.name == "Btn_3" && GlobalControl.downed_aerial) ||
                (gameObject.name == "Btn_6" && GlobalControl.downed_aquatic) ||
                (gameObject.name == "Btn_7" && GlobalControl.downed_turret) ||
                (gameObject.name == "Btn_8" && GlobalControl.found_errat) ||
                (gameObject.name == "Btn_9" && GlobalControl.downed_boss_1) ||
                (gameObject.name == "Btn_10" && GlobalControl.downed_boss_2) ||
                (gameObject.name == "Btn_11" && GlobalControl.downed_boss_3) ||
                (gameObject.name == "Btn_15" && GlobalControl.downed_boss_4)))
            {
                img.color = new Color(1f, 1f, 1f, 1f);
                img.sprite = enemy;
            }

            // Reports
            else if (GlobalControl.menu == "reports" &&
                ((gameObject.name == "Btn_1" && GlobalControl.report_1) ||
                (gameObject.name == "Btn_2" && GlobalControl.report_2) ||
                (gameObject.name == "Btn_3" && GlobalControl.report_3) ||
                (gameObject.name == "Btn_6" && GlobalControl.report_4) ||
                (gameObject.name == "Btn_7" && GlobalControl.report_5) ||
                (gameObject.name == "Btn_8" && GlobalControl.report_6) ||
                (gameObject.name == "Btn_9" && GlobalControl.report_7) ||
                (gameObject.name == "Btn_10" && GlobalControl.report_8) ||
                (gameObject.name == "Btn_11" && GlobalControl.report_9) ||
                (gameObject.name == "Btn_15" && GlobalControl.report_10)))
            {
                img.color = new Color(1f, 1f, 1f, 1f);
                img.sprite = report;
            }
            else
            {
                img.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
