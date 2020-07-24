using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string objName;
    public static bool inRange = false;
    public string itemName = "";
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        itemName = gameObject.name;
        // Object isn't called in on scene load if already picked up
        if ((GlobalControl.batteryUnlocked && objName == "battery") ||
            (GlobalControl.solarUnlocked && objName == "starter") ||
            (GlobalControl.geoUnlocked && objName == "geo") ||
            (GlobalControl.mapUnlocked && objName == "map") ||
            (GlobalControl.heartlessUnlocked && objName == "heartless") ||
            (GlobalControl.familiarUnlocked && objName == "familiar") ||
            (GlobalControl.unstableUnlocked && objName == "unstable") ||
            (GlobalControl.extra_1 && objName == "extra_1") ||
            (GlobalControl.extra_2 && objName == "extra_2") ||
            (GlobalControl.extra_3 && objName == "extra_3") ||
            (GlobalControl.plating_1 && objName == "plating_1") ||
            (GlobalControl.plating_2 && objName == "plating_2"))
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
            InteractText.notif = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = true;
            sticky = itemName;
            if (!InteractText.interacted)
                InteractText.type = "item";
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = false;
            sticky = "";
            if (!InteractText.interacted)
                InteractText.type = "";
        }        
    }
}
