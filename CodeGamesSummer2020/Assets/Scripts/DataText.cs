﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataText : MonoBehaviour
{
    private Text text;
    public static int data = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Data").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "Data: " + data + "%";
        }
        else
        {
            text.text = "";
        }
    }
}
