using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryBtnBehavior.btn = "";
            if (inMenu)
            {
                Cursor.visible = false;
                img.color = new Color(1f, 1f, 1f, 0f);
                inMenu = false;
            }
            else
            {
                Cursor.visible = true;
                img.color = new Color(1f, 1f, 1f, 1f);
                inMenu = true;
                if (RepairStation.inRange)
                {
                    InteractText.type = "rest";
                }
            }
        }
    }
}
