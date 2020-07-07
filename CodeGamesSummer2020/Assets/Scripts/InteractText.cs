using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    private Text text;
    public string attachedTo;
    public static string type = "";
    public static string stickied = ""; // Retain sticky info after object is set inactive
    public static bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find(gameObject.name).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedTo == stickied || attachedTo == PickUpItem.sticky || attachedTo == EnterDoor.sticky)
        {
            stickied = PickUpItem.sticky;
            if (interacted)
            {
                // Items
                if (type == "Item (battery)")
                {
                    text.text = "Picked up: Battery";
                    GlobalControl.batteryUnlocked = true;
                }
                else if (type == "solar")
                {
                    text.text = "Picked up: Solar Panel";
                    GlobalControl.solarUnlocked = true;
                }
                else if (type == "geothermal")
                {
                    text.text = "Picked up: Geothermal Extractor";
                    GlobalControl.geoUnlocked = true;
                }
                else if (type == "gun")
                {
                    text.text = "Picked Up: Energy Cannon";
                    GlobalControl.gunUnlocked = true;
                }
                else if (type == "map")
                {
                    text.text = "Picked up: Navigational Module";
                    GlobalControl.mapUnlocked = true;
                }
                else if (type == "heartless")
                {
                    text.text = "Picked up: Heartless Generator";
                    GlobalControl.heartlessUnlocked = true;
                }
                else if (type == "familiar")
                {
                    text.text = "Picked up: Lost Reactor";
                    GlobalControl.familiarUnlocked = true;
                }
                else if (type == "unstable")
                {
                    text.text = "Picked up: Unstable Reactor";
                    GlobalControl.unstableUnlocked = true;
                }
                else if (type == "scrap")
                {
                    text.text = "Picked up: Hyper Scrap";
                    GlobalControl.scrapFound = true;
                }
                else if (type == "batteries")
                {
                    text.text = "Picked up: Extra Battery";
                    GlobalControl.extraFound = true;
                }
                else if (type == "plating")
                {
                    text.text = "Picked up: Special Plating";
                    GlobalControl.plateFound = true;
                }
                else
                {
                    text.text = "";
                }
                StartCoroutine(wait(2f));
            }
            else
            {
                if (type == "item")
                {
                    text.text = "Pick up";
                }
                else if (type == "npc")
                {
                    text.text = "Talk";
                }
                else if (type == "door")
                {
                    text.text = "Enter";
                }

                else if (type == "misc")
                {
                    text.text = "Examine";
                }
                else
                {
                    text.text = "";
                }
            }
        }
        else if (attachedTo == "Rest" && type == "rest")
        {
            text.text = "Interact";
        }
        else if (!PickUpItem.notif)
        {
            text.text = "";
        }
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        type = "";
        stickied = "";
        text.text = "";
        interacted = false;
        PickUpItem.notif = false;
    }
}
