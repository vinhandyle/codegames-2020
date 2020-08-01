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
    public static string stickied2 = "";
    public static bool interacted = false;
    public static bool notif = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find(gameObject.name).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu)
        {
            text.text = "";
        }
        else
        { 
            if (attachedTo == stickied || attachedTo == stickied2 ||
                attachedTo == EnterDoor.sticky || 
                attachedTo == PickUpItem.sticky ||
                attachedTo == Talk.sticky ||
                attachedTo == TriggerSwitch.sticky ||
                attachedTo == Examine.sticky)
            {
                stickied = PickUpItem.sticky;
                if(attachedTo.Substring(0, 3) == "Ego")
                    stickied2 = Examine.sticky;

                if (interacted)
                {
                    // Post-interaction

                    /*-----Items that can be picked up-----*/
                    if (type == "Item (battery)")
                    {
                        text.text = "Picked up: Battery";
                        GlobalControl.batteryUnlocked = true;
                    }

                    // In-game
                    else if (type == "Starter")
                    {
                        text.text = "Picked up: Battery \n" +
                                    "Picked up: Solar Panel \n" +
                                    "Picked up: Energy Cannon";
                        GlobalControl.batteryUnlocked = true;
                        GlobalControl.solarUnlocked = true;
                        GlobalControl.gunUnlocked = true;
                    }
                    else if (type == "geothermal")
                    {
                        text.text = "Picked up: Geothermal Extractor";
                        GlobalControl.geoUnlocked = true;
                    }
                    else if (type == "Map")
                    {
                        text.text = "Picked up: Navigational Module";
                        GlobalControl.mapUnlocked = true;
                    }
                    else if (type == "Heartless")
                    {
                        text.text = "Picked up: Heartless Generator";
                        GlobalControl.heartlessUnlocked = true;
                    }
                    else if (type == "Lost")
                    {
                        text.text = "Picked up: Lost Reactor";
                        GlobalControl.familiarUnlocked = true;
                    }
                    else if (type == "Unstable")
                    {
                        text.text = "Picked up: Unstable Reactor";
                        GlobalControl.unstableUnlocked = true;
                    }
                    else if (type == "scrap")
                    {
                        text.text = "Picked up: Hyper Scrap";
                        GlobalControl.scrapFound = true;
                    }
                    else if (type == "extra")
                    {
                        text.text = "Picked up: Extra Battery";
                        GlobalControl.extraFound = true;
                    }
                    else if (type == "plating")
                    {
                        text.text = "Picked up: Special Plating";
                        GlobalControl.plateFound = true;
                    }

                    /*-----Items that can be examined-----*/

                    // Ego Series Harvest
                    else if (type == "ego")
                    {
                        text.text = "Harvested: Memory Drive";
                        GlobalControl.data += 10;

                        // Obtain Ego report
                        if (attachedTo == "Ego_1")
                        {
                            GlobalControl.report_1 = true;
                        }
                        else if (attachedTo == "Ego_2")
                        {
                            GlobalControl.report_2 = true;
                        }
                        else if (attachedTo == "Ego_3")
                        {
                            GlobalControl.report_3 = true;
                        }
                        else if (attachedTo == "Ego_4")
                        {
                            GlobalControl.report_4 = true;
                        }
                        else if (attachedTo == "Ego_5")
                        {
                            GlobalControl.report_5 = true;
                        }
                        else if (attachedTo == "Ego_6")
                        {
                            GlobalControl.report_6 = true;
                        }
                        else if (attachedTo == "Ego_7")
                        {
                            GlobalControl.report_7 = true;
                        }
                        else if (attachedTo == "Ego_8")
                        {
                            GlobalControl.report_8 = true;
                        }
                        else if (attachedTo == "Ego_9")
                        {
                            GlobalControl.report_9 = true;
                        }
                        else if (attachedTo == "Ego_10")
                        {
                            GlobalControl.report_10 = true;
                        }
                    }

                    /*-----NPC dialogue-----*/

                    // Errat
                    else if (GlobalControl.counter_1 != 5)
                    {
                        if (type == "Errat_0")
                        {
                            text.text = "A Machina! What are you doing down here?";
                        }
                        else if (type == "Errat_1")
                        {
                            text.text = "There's a crack in this part of the wall.";
                        }
                        else if (type == "Errat_2")
                        {
                            text.text = "The passageway here is blocked by rocks.";
                        }
                        else if (type == "Errat_3")
                        {
                            text.text = "We are the last humans. Please help us end this nightmare.";
                        }
                        else if (type == "Errat_4")
                        {
                            text.text = "It's been so long since I've seen the sky...";
                        }
                        else if (type == "Errat_5")
                        {
                            text.text = "You do not want to fall down there.";
                        }
                    }

                    // Blank everything else
                    else
                    {
                        text.text = "";
                    }

                    // Lingering textbox on item pickup
                    if (type == "ego" || type == "Starter" || type == "geothermal" || type == "Map" || type == "Heartless" ||
                        type == "Lost" || type == "Unstable" || type == "plating" || type == "extra" || type == "scrap")
                    {
                        StartCoroutine(wait(2f));
                    }
                    else if (type == "trigger")
                    {
                        type = "";
                        stickied = "";
                        stickied2 = "";
                        text.text = "";
                        interacted = false;
                        notif = false;
                    }

                }
                else
                {
                    // Pre-interaction
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
                        if (EnterDoor.locked)
                        {
                            text.text = "Locked";
                        }
                        else
                        {
                            text.text = "Enter";
                        }
                    }
                    else if (type == "trigger")
                    {
                        if (TriggerSwitch.refState == "inactive")
                        {
                            text.text = "";
                        }
                        else
                        {
                            text.text = "Interact";
                        }
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
                text.text = "Recharge";
            }
            else if (!notif)
            {
                text.text = "";
            }
        }    
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        type = "";
        stickied = "";
        stickied2 = "";
        text.text = "";
        interacted = false;
        notif = false;
    }
}
