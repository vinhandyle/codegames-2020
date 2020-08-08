using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorBtn : MonoBehaviour
{
    private Image img;
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);

        if (gameObject.name == "Selector")
        {
            if (GlobalControl.reactor == "unstable")
            {
                transform.position = new Vector3(383.7f, 300f, 0);
            }
            else if (GlobalControl.reactor == "familiar")
            {
                transform.position = new Vector3(383.7f, 170f, 0);
            }
            else
            {
                transform.position = new Vector3(383.7f, 235, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ReactorManage.open && 
            ((gameObject.name == "Selector") || (gameObject.name == "PrimaryBtn") ||
            (gameObject.name == "ChallengeBtn" && GlobalControl.unstableUnlocked) ||
            (gameObject.name == "SecretBtn" && GlobalControl.familiarUnlocked)))
        {
            img.color = new Color(1f, 1f, 1f, 1f);
            if (gameObject.name == "Selector")
            {
                if (GlobalControl.reactor == "unstable")
                {
                    transform.position = new Vector3(383.7f, 300f, 0);
                }
                else if (GlobalControl.reactor == "familiar")
                {
                    transform.position = new Vector3(383.7f, 170f, 0);
                }
                else
                {
                    transform.position = new Vector3(383.7f, 235, 0);
                }
            }
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }

        if (gameObject.name == "PrimaryBtn")
        {
            if (GlobalControl.imperialUnlocked)
            {
                img.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "SecretBtn")
        {
            if (GlobalControl.data == 100)
            {
                img.sprite = sprites[1];
            }
            else if (GlobalControl.data >= 50)
            {
                img.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "Selector")
        {
            if (GlobalControl.reactor == "unstable")
            {
                transform.position = new Vector3(383.7f, 300f, 0);
            }
            else if (GlobalControl.reactor == "familiar")
            {
                transform.position = new Vector3(383.7f, 170f, 0);
            }
            else
            {
                transform.position = new Vector3(383.7f, 235, 0);
            }
        }

    }
}
