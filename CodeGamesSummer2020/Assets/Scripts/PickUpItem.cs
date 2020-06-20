using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static bool inRange = false;
    public static string itemName = "";

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
        if(!InteractText.interacted)
        InteractText.type = "item";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        if(!InteractText.interacted)
        InteractText.type = "";
    }        
}
