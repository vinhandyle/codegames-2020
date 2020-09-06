﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBar : MonoBehaviour
{
    public SpriteRenderer sprite;
    public string attachedTo;
    public List<float> x;
    public List<float> y;
    public List<float> w;
    public List<float> h;

    public static bool notif;
    public static string signal;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedTo == "Emperor")
        {
            if (GlobalControl.counter_1 < 5)
            {
                sprite.enabled = true;
                transform.localScale = new Vector3(w[GlobalControl.counter_1], h[GlobalControl.counter_1], 1f);
                transform.position = new Vector3(x[GlobalControl.counter_1], y[GlobalControl.counter_1], transform.position.z);
            }
            else
                sprite.enabled = false;
        }
        // Turn on or off when in range of interactable object
        else if (attachedTo == PickUpItem.sticky ||
            attachedTo == Talk.sticky ||
            attachedTo == Examine.sticky ||
            attachedTo == TriggerSwitch.sticky ||
            attachedTo == Craft.sticky ||
            attachedTo == EnterDoor.sticky ||
            (attachedTo == "Rest" && InteractText.type == "rest") ||
            InteractText.notif)
        {
            if (InteractText.notif)
            {
                if (attachedTo == InteractText.stickied)
                {
                    notif = true;
                    signal = attachedTo;
                    sprite.enabled = true;
                }
            }
            else
            {
                sprite.enabled = true;
            }
        }
        else if (!InteractText.notif)
        {
            notif = false;
            sprite.enabled = false;
        }
        else if (notif && signal == attachedTo)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }

        // Change bar position + size for pre/post interaction
        if (InteractText.interacted)
        {
            if (attachedTo == "Constructor" && GlobalControl.doubleUnlocked)
            {
                transform.localScale = new Vector3(w[2], h[1], 1f);
                transform.position = new Vector3(x[1], y[1], transform.position.z);
            }
            else
            {
                transform.localScale = new Vector3(w[1], h[1], 1f);
                transform.position = new Vector3(x[1], y[1], transform.position.z);
            }            
        }
        else if(attachedTo != "Emperor")
        {
            transform.localScale = new Vector3(w[0], h[0], 1f);
            transform.position = new Vector3(x[0], y[0], transform.position.z);
        }
    }
}
