using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtnText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("MenuBtnText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Hides button when not in menu
        if (MenuBtn.inMenu)
        {
            // Button text changes depending on which menu player is in
            if (Map.mapUnlocked)
            {
                if (Map.mapOpen)
                {
                    text.text = "Inventory";
                }
                else
                {
                    text.text = "Map";
                }
            }
            else
            {
                text.text = "";
            }
        }
        else
        {
            text.text = "";
        }
    }
}
