using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public static bool inRange = false;
    public static string sticky;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted)
        {
            InteractText.interacted = true;
            if (GlobalControl.scrapNum > 0)
            {
                InteractText.type = "post-craft";
                InteractText.notif = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = true;
            sticky = gameObject.name;
            if (!InteractText.interacted)
                InteractText.type = "pre-craft";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (InteractText.type != "post-craft")
                InteractText.type = "pre-craft";
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
