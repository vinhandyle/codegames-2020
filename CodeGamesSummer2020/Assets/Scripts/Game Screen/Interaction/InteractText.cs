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
    public static bool triggerOnce = false;
    public static bool locked = false;

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
                attachedTo == Craft.sticky ||
                attachedTo == Examine.sticky)
            {
                stickied = PickUpItem.sticky;
                if(attachedTo.Substring(0, 3) == "Ego")
                    stickied2 = Examine.sticky;

                if (interacted)
                {
                    // Post-interaction

                    // Lingering textbox on item pickup
                    if (type == "ego" || type == "Starter" || type == "Geothermal" || type == "Map" || type == "Heartless" || type == "Key" ||
                        type == "Lost" || type == "Unstable" || type.Substring(0, 7) == "Plating" || type.Substring(0, 5) == "Extra" ||
                        type.Substring(0, 5) == "Scrap" || (type == "post-craft" && !triggerOnce))
                    {
                        triggerOnce = true;
                        StartCoroutine(wait(2f));
                    }
                    else if (type == "trigger" || type == "pre-craft")
                    {
                        type = "";
                        stickied = "";
                        stickied2 = "";
                        text.text = "";
                        interacted = false;
                        notif = false;
                    }

                    // Specific

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
                    else if (type == "Geothermal")
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
                    else if (type == "Key")
                    {
                        text.text = "Picked up: Access Key";
                        GlobalControl.keyUnlocked = true;
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

                    /*-----Crafting-----*/
                    else if (type == "post-craft" && !triggerOnce)
                    {
                        if (!GlobalControl.dashUnlocked)
                        {
                            text.text = "Obtained: Booster Rocket";
                            GlobalControl.dashUnlocked = true;
                        }
                        else if (!GlobalControl.clingUnlocked)
                        {
                            text.text = "Obtained: Climbing Claws";
                            GlobalControl.clingUnlocked = true;
                        }
                        else if (!GlobalControl.doubleUnlocked)
                        {
                            text.text = "Obtained: Booster Rocket MK2";
                            GlobalControl.doubleUnlocked = true;
                        }
                        GlobalControl.scrapNum--;
                    }

                    /*-----NPC dialogue-----*/

                    // Azimuth
                    if (type == "Azimuth_0")
                    {
                        text.text = "<i>Purpose fulfilled. Leave pending.</i>";
                    }
                    else if (type == "Azimuth_1")
                    {
                        text.text = "<i>Fatigue levels slowly dropping.</i>";
                    }
                    else if (type == "Azimuth_2")
                    {
                        text.text = "<i>Processing incoming reports.</i>";
                    }
                    else if (type == "Azimuth_3")
                    {
                        text.text = "<i>Updating task list.</i>";
                    }
                    else if (type == "Azimuth_4")
                    {
                        text.text = "<i>The area past here is dangerous. Avoid at all cost.</i>";
                    }
                    else if (type == "Azimuth_5")
                    {
                        text.text = "<i>Access Key no longer in possession...</i>";
                    }
                    else if (type == "Azimuth_6")
                    {
                        text.text = "<i>The pod will begin moving after receiving some ignition energy.</i>";
                    }
                    else if (type == "Azimuth_7")
                    {
                        text.text = "<i>The last trip has been made.</i>";
                    }

                    // Group

                    /*-----Items that can be examined-----*/

                    // Signs
                    else if (type.Substring(0, 4) == "Sign")
                    {
                        if (type == "Sign_Station")
                        {
                            text.text = "<i>Access Key required to pass through.</i>";
                        }
                        else if (type == "Sign_Station_1")
                        {
                            text.text = "<b>Update:</b> The Midnight Bay has returned.";
                        }
                        else if (type == "Sign_Station_2")
                        {
                            text.text = "Station no longer in use. Depart immediately.";
                        }
                    }

                    /*-----Items that can be picked up-----*/
                    else if (type.Substring(0, 5) == "Scrap")
                    {
                        text.text = "Picked up: Hyper Scrap";
                        if (attachedTo == "Scrap_1")
                        {
                            GlobalControl.scrap_1 = true;
                        }
                        else if (attachedTo == "Scrap_2")
                        {
                            GlobalControl.scrap_2 = true;
                        }
                        else if (attachedTo == "Scrap_3")
                        {
                            GlobalControl.scrap_3 = true;
                        }
                        GlobalControl.scrapFound = true;
                        GlobalControl.scrapNum++;
                    }
                    else if (type.Substring(0, 5) == "Extra")
                    {
                        text.text = "Picked up: Extra Battery";
                        if (attachedTo == "Extra_1")
                        {
                            GlobalControl.extra_1 = true;
                        }
                        else if (attachedTo == "Extra_2")
                        {
                            GlobalControl.extra_2 = true;
                        }
                        else if (attachedTo == "Extra_3")
                        {
                            GlobalControl.extra_3 = true;
                        }
                        GlobalControl.extraFound = true;
                        GlobalControl.extraNum++;
                        GlobalControl.update = 2;
                    }
                    else if (type.Substring(0, 7) == "Plating")
                    {
                        if (attachedTo == "Plating_1")
                        {

                        }
                        else if (attachedTo == "Plating_2")
                        {

                        }
                        text.text = "Picked up: Special Plating";
                        GlobalControl.plateFound = true;
                        GlobalControl.plateNum++;
                        GlobalControl.update = 1;
                    }

                    /*-----NPC dialogue-----*/

                    // Errat
                    else if (GlobalControl.counter_1 != 5)
                    {
                        if (type == "Errat_0_")
                        {
                            text.text = "How did a Machina get down here?";
                        }
                        else if (type == "Errat_1_")
                        {
                            text.text = "There's a crack in this part of the wall.";
                        }
                        else if (type == "Errat_2_")
                        {
                            text.text = "The passageway here caved in.";
                        }
                        else if (type == "Errat_3_")
                        {
                            text.text = "We need to free the others. Those fools...";
                        }
                        else if (type == "Errat_4_")
                        {
                            text.text = "To \"save\" us, they want to take our freedom. <b>Never.</b>";
                        }
                        else if (type == "Errat_5_")
                        {
                            text.text = "You do not want to fall down there.";
                        }
                    }

                    // Blank everything else
                    else
                    {
                        text.text = "";
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
                        if (attachedTo == EnterDoor.doorName)
                        {
                            if (locked)
                            {
                                text.text = "Locked";
                            }
                            else
                            {
                                text.text = "Enter";
                            }
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
                    else if (type == "pre-craft")
                    {
                        text.text = "Insert Hyper Scrap";
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
        if (notif)
        {
        type = "";
        stickied = "";
        stickied2 = "";
        text.text = "";
        interacted = false;
        notif = false;
        triggerOnce = false;
        }
    }
}
