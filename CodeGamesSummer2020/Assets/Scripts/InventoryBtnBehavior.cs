using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBtnBehavior : MonoBehaviour
{
    public static string btn = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Description.descOf = btn;
    }    

    // Gets button name from Inspector on press
    public void getButtonName(string name)
    {
        btn = name;
    }
}
