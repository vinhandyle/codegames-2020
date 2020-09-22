using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    private Image img;
    private Text text;
    public List<Sprite> sprites; // Not unlocked (0), Usable (1), Not Usable (2)

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        text = GetComponent<Text>();

        if (gameObject.name == "Indicator_Jump")
        {
            if (Player.canJump1)
            {

                img.sprite = sprites[1];
            }
            else
            {
                img.sprite = sprites[2];
            }
        }
        else if (gameObject.name == "Indicator_Dash")
        {
            if (GlobalControl.dashUnlocked)
            {
                if (Player.canDash)
                {
                    img.sprite = sprites[1];
                }
                else
                {
                    img.sprite = sprites[2];
                }
            }
            else
            {
                img.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "Indicator_Double")
        {
            if (GlobalControl.doubleUnlocked)
            {
                if (Player.canJump2)
                {
                    img.sprite = sprites[1];
                }
                else
                {
                    img.sprite = sprites[2];
                }
            }
            else
            {
                img.sprite = sprites[0];
            }
        }
        else if (transform.parent.name == "Saving")
        {
            if (gameObject.name == "Image")
                img.color = new Color(0f, 0f, 0f, 0f);
            else if (gameObject.name == "Text")
                text.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Indicator_Jump")
        {
            if (Player.canJump1)
            {
                if (GlobalControl.clingUnlocked && Player.walled)
                {
                    if (!Input.GetKey("a") && !Input.GetKey("d"))
                        img.sprite = sprites[1];
                    else
                        img.sprite = sprites[2];
                }
                else
                {
                    img.sprite = sprites[1];
                }
            }
            else
            {
                img.sprite = sprites[2];
            }
        }
        else if (gameObject.name == "Indicator_Dash")
        {
            if (GlobalControl.dashUnlocked)
            {
                if (Player.canDash && !Player.dashing)
                {
                    img.sprite = sprites[1];
                }
                else
                {
                    img.sprite = sprites[2];
                }
            }
            else
            {
                img.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "Indicator_Double")
        {
            if (GlobalControl.doubleUnlocked)
            {
                if (Player.canJump2 && !Player.walled)
                {
                    img.sprite = sprites[1];
                }
                else
                {
                    img.sprite = sprites[2];
                }
            }
            else
            {
                img.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "Indicator_Heart")
        {
            if (GlobalControl.heartlessUnlocked)
            {
                img.color = new Color(1f, 1f, 1f, 1f);

                if (GlobalControl.h2e)
                {
                    transform.position = new Vector2(166.55f, 458.3991f);
                }
                else
                {
                    transform.position = new Vector2(50.5f, 458.3991f);
                }
            }
            else
            {

                img.color = new Color(0f, 0f, 0f, 0f);
            }
        }
        else if (transform.parent.name == "Saving")
        {
            if (GlobalControl.saving)
            {
                if (gameObject.name == "Image")
                    img.color = new Color(1f, 1f, 1f, 1f);
                else if (gameObject.name == "Text")
                    text.text = "Saving...";
            }
            else
            {
                if (gameObject.name == "Image")
                    img.color = new Color(0f, 0f, 0f, 0f);
                else if (gameObject.name == "Text")
                    text.text = "";
            }
        }
    }
}
