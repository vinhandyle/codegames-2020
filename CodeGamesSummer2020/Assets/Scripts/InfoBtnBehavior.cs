using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBtnBehavior : MonoBehaviour
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
            if (InfoBtn.infoPage)
            {
                InfoBtn.infoPage = false;
            }
            else
            {
                InfoBtn.infoPage = true;
            }
        }
    }
}
