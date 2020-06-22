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
                    Player.batteryUnlocked = true;
                }
                else if (type == "solar")
                {

                }
                else if (type == "geothermal")
                {

                }
                else if (type == "gun")
                {

                }
                else if (type == "map")
                {

                }
                else if (type == "heartless")
                {

                }
                else if (type == "unstable")
                {

                }
                else if (type == "scrap")
                {

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
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        type = "";
        stickied = "";
        text.text = "";
        interacted = false;
    }
}
