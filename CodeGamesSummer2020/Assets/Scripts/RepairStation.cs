using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public static bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w"))
        {
            if (ReactorManage.open)
            {
                InteractText.type = "rest";
                ReactorManage.open = false;
                Cursor.visible = false;
            }
            else
            {
                InteractText.type = "";
                ReactorManage.open = true;
                Cursor.visible = true;
            }
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
