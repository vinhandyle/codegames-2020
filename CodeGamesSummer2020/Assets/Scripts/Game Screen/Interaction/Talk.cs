using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public static bool inRange = false;
    public static string npcName = "";
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        npcName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted)
        {
            InteractText.interacted = true;
            InteractText.type = npcName;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !(gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.imperialUnlocked))
        {
            inRange = true;
            sticky = npcName;
            if (!InteractText.interacted)
                InteractText.type = "npc";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player" && !(gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.imperialUnlocked))
        {
            inRange = false;
            sticky = "";
            if (!InteractText.interacted)
                InteractText.type = "";
        }
    }
}
