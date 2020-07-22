using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    Image img;

    public static bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Open and close menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuBtnBehavior.btn = "";
            if (inMenu)
            {
                Cursor.visible = false;
                inMenu = false;
            }
            else
            {
                Cursor.visible = true;
                inMenu = true;
                if (RepairStation.inRange)
                {
                    InteractText.type = "rest";
                }
            }
        }

        if (inMenu)
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
