using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        if (MenuBtn.inMenu)
        {
            InventoryBtnBehavior.btn = "";
            InfoBtn.infoPage = false;
            if (Map.mapOpen)
            {
                Map.mapOpen = false;
            }
            else
            {
                Map.mapOpen = true;
            }
        }
    }
}
