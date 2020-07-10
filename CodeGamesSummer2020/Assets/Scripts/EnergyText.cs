﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    { 
        text = GameObject.Find("Energy").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "";
        }
        else
        { 
            text.text = "Energy: " + GlobalControl.energyCurr + " / " + GlobalControl.energyMax;
        }
    }
}
