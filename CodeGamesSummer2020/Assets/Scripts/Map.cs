﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    Image img;
    public static bool mapUnlocked = true;
    public static bool mapOpen = false;

    // The Institute
    public Sprite home;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu && mapUnlocked && mapOpen)
        { 
          // If map was last open, reopen when opening menu
            img.color = new Color(1f, 1f, 1f, 1f);
            img.sprite = home;           
        }
        else
        { // Close menu
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
