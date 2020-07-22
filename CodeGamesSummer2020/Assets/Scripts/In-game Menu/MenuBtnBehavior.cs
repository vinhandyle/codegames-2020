using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnBehavior : MonoBehaviour
{
    public static string btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Description.descOf = btn;
    }

    // Section Header Button Press
    public void ChangeMenu(string menu)
    {
        if (MenuBackground.inMenu && 
            (menu == "inventory" || menu == "help" || 
            (menu == "map" && GlobalControl.mapUnlocked) ||
            (menu == "enemies" && GlobalControl.counter_1 > 0) ||
            (menu == "reports" && GlobalControl.data > 0)))
        {
            btn = "";
            GlobalControl.menu = menu;
            Debug.Log(GlobalControl.menu);
        }
    }

    // Button Press for Description
    public void ChangeDesc(string name)
    {
        btn = name;
    }
}
