using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string objName;
    public static bool inRange = false;
    public static bool notif = false;
    public static string itemName = "";
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        itemName = gameObject.name;
        // Object isn't called in on scene load if already picked up
        if (GlobalControl.batteryUnlocked && objName == "battery")
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted)
        {
            InteractText.interacted = true;
            InteractText.type = itemName;
            gameObject.SetActive(false);
            notif = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inRange = true;
        sticky = itemName;
        if(!InteractText.interacted)
        InteractText.type = "item";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        sticky = "";
        if(!InteractText.interacted)
        InteractText.type = "";
    }
}
