using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    public bool harvestable;
    public string objName = "";

    public static bool inRange = false;
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        objName = gameObject.name;

        // Remove Ego Machina on start if already harvested
        if ((GlobalControl.report_1 && gameObject.name == "Ego_1") ||
            (GlobalControl.report_2 && gameObject.name == "Ego_2") ||
            (GlobalControl.report_3 && gameObject.name == "Ego_3") ||
            (GlobalControl.report_4 && gameObject.name == "Ego_4") ||
            (GlobalControl.report_5 && gameObject.name == "Ego_5") ||
            (GlobalControl.report_6 && gameObject.name == "Ego_6") ||
            (GlobalControl.report_7 && gameObject.name == "Ego_7") ||
            (GlobalControl.report_8 && gameObject.name == "Ego_8") ||
            (GlobalControl.report_9 && gameObject.name == "Ego_9") ||
            (GlobalControl.report_10 && gameObject.name == "Ego_10"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted && sticky == objName)
        {
            InteractText.interacted = true;

            if (harvestable)
            {
                InteractText.type = "ego";
                gameObject.SetActive(false);
                InteractText.notif = true;
            }
            else
            {
                InteractText.type = objName;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = true;
            sticky = objName;
            if (!InteractText.interacted)
                InteractText.type = "misc";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = false;
            sticky = "";

            if (!harvestable)
            {
                InteractText.interacted = false;
                InteractText.type = "";
            }

            if (!InteractText.interacted)
                InteractText.type = "";
        }
    }
}
