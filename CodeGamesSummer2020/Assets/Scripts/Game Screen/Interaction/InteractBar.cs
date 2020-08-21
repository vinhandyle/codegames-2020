using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBar : MonoBehaviour
{
    public SpriteRenderer sprite;
    public string attachedTo;
    public List<float> x;
    public List<float> y;
    public List<float> w;
    public List<float> h;

    public static bool notif;
    public static string signal;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Turn on or off when in range of interactable object
        if (attachedTo == PickUpItem.sticky ||
            attachedTo == Talk.sticky ||
            attachedTo == Examine.sticky ||
            attachedTo == TriggerSwitch.sticky ||
            attachedTo == Craft.sticky ||
            attachedTo == EnterDoor.sticky ||
            (attachedTo == "Rest" && InteractText.type == "rest") ||
            (attachedTo == InteractText.stickied && InteractText.notif))
        {
            sprite.enabled = true;
            if (InteractText.notif)
            {
                notif = true;
                signal = attachedTo;
            }
        }
        else if (!InteractText.notif)
        {
            notif = false;
            sprite.enabled = false;
        }
        else if (notif && signal == attachedTo)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }

        // Change bar position + size for pre/post interaction
        if (InteractText.interacted)
        {
            transform.localScale = new Vector3(w[1], h[1], 1f);
            transform.position = new Vector3(x[1], y[1], transform.position.z);
        }
        else
        {
            transform.localScale = new Vector3(w[0], h[0], 1f);
            transform.position = new Vector3(x[0], y[0], transform.position.z);
        }
    }
}
