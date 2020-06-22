using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static bool inRange = false;
    public static string itemName = "";
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        itemName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted)
        {
            InteractText.interacted = true;
            InteractText.type = itemName;
            gameObject.SetActive(false);
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
