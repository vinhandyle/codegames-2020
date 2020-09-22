using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public static bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        // Determines at which door the player will spawn if there are multiple doors in one scene
        if (GlobalControl.nextDoor == gameObject.name)
        {
            Player.x = gameObject.transform.position.x;
            Player.y = gameObject.transform.position.y;
            GlobalControl.switched = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w"))
        {
            // Open or close reactor menu
            if (ReactorManage.open)
            {
                InteractText.type = "rest";
                ReactorManage.open = false;
                Cursor.visible = false;
                GlobalControl.save();
            }
            else
            {
                InteractText.type = "";
                ReactorManage.open = true;
                Cursor.visible = true;
            }

            // Set new respawn point & full restore
            GlobalControl.checkpoint = gameObject.name;
            GlobalControl.healthCurr = GlobalControl.healthMax;
            GlobalControl.energyCurr = GlobalControl.energyMax;

            // Respawn all enemies
            GlobalControl.respawnAll();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player") {
            inRange = true;
            InteractText.type = "rest";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = false;
            InteractText.type = "";
        }
    }
}
