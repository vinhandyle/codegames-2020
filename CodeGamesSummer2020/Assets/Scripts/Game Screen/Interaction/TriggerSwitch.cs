using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    public string objName;
    public string state;
    public List<Sprite> sprites;

    public static string refState;
    public static bool inRange = false;
    public static string sticky = "";


    // Start is called before the first frame update
    void Start()
    {
        objName = gameObject.name;
        // Set things based on pre-existing state
        if (objName == "Switch_SG_8")
        {
            if (GlobalControl.state_SG_8 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_SG_8;
        }
        else if (objName == "Switch_SG_10")
        {
            if (GlobalControl.state_SG_10 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_SG_10;
        }
        else if (objName == "Switch_SG_11")
        {
            if (GlobalControl.state_SG_11 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_SG_11;
        }
        else if (objName == "Switch_TT_11")
        {
            if (GlobalControl.state_TT_11 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_TT_11;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted)
        {
            InteractText.interacted = true;

            if (objName == "Switch_SG_8")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.block_SG_9 = false;
                    GlobalControl.state_SG_8 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_SG_10")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.secret_SG_9 = false;
                    GlobalControl.state_SG_10 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_SG_11")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.block_SG_11 = false;
                    GlobalControl.state_SG_11 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_TT_11")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.block_TT_2 = false;
                    GlobalControl.block_TT_11 = false;
                    GlobalControl.state_TT_11 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_TT_14")
            {
                if (GlobalControl.pod_direction == "right")
                {
                    GlobalControl.pod_direction = "left";
                }
                else if (GlobalControl.pod_direction == "left")
                {
                    GlobalControl.pod_direction = "right";
                }
            }
        }
        refState = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = true;
            sticky = objName;
            if (!InteractText.interacted)
                InteractText.type = "trigger";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (objName == "Switch_TT_14")
            {
                InteractText.type = "trigger";

                if (InteractText.interacted)
                {
                    InteractText.interacted = false;
                }
            }
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
