using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPage : MonoBehaviour
{
    Image img;

    public Sprite pre;
    public Sprite post;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);

        // Updates control page to avoid spoilers
        if (GlobalControl.heartlessUnlocked)
        {
            gameObject.GetComponent<Image>().sprite = post;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = pre;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu && GlobalControl.menu == "help")
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
