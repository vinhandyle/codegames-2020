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
        else if (objName == "Switch_SG_11S")
        {
            if (GlobalControl.state_SG_11S == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_SG_11S;
        }
        else if (objName == "Switch_SG_11S (1)")
        {
            if (GlobalControl.state_SG_11S_ == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_SG_11S_;
        }
        else if (objName == "Switch_TT_11")
        {
            if (GlobalControl.state_TT_11 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_TT_11;
        }
        else if (objName == "Switch_MB_4")
        {
            if (GlobalControl.state_MB_4 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_MB_4;
        }
        else if (objName == "Switch_MB_7")
        {
            if (GlobalControl.state_MB_7 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_MB_7;
        }
        else if (objName == "Switch_MB_8")
        {
            if (GlobalControl.state_MB_8 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_MB_8;
        }
        else if (objName == "Switch_MB_11")
        {
            if (GlobalControl.state_MB_11 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_MB_11;
        }
        else if (objName == "Switch_IT_4")
        {
            if (GlobalControl.state_IT_4 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_IT_4;
        }
        else if (objName == "Switch_IT_6")
        {
            if (GlobalControl.state_IT_6 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_IT_6;
        }
        else if (objName == "Switch_IT_9")
        {
            if (GlobalControl.state_IT_9 == "inactive")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            state = GlobalControl.state_IT_9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w") && !InteractText.interacted && objName == sticky)
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
            else if (objName == "Switch_SG_11S")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.block_SG_11S = false;
                    GlobalControl.state_SG_11S = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_SG_11S (1)")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.block_SG_11S_ = false;
                    GlobalControl.state_SG_11S_ = state;
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
            else if (objName == "Switch_MB_4")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_MB_4 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_MB_7")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_MB_7 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_MB_8")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_MB_8 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_MB_11")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_MB_11 = state;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_IT_4")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_IT_4 = state;
                    GlobalControl.block_IT_4_ = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_IT_6")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_IT_6 = state;
                    GlobalControl.block_IT_6 = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
            else if (objName == "Switch_IT_9")
            {
                if (state == "active")
                {
                    state = "inactive";
                    GlobalControl.state_IT_9 = state;
                    GlobalControl.block_IT_9 = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
            }
        }

        if (state == "inactive")
        {
            GetComponent<BoxCollider2D>().enabled = false;
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
            else
                InteractText.interacted = false;
        }
    }
}
