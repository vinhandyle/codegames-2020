using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour
{
    Image img;

    public static bool inTrophy = false; // Trophy section
    public static bool inControl = false; // Controls section

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        if (gameObject.name == "ExitBtn")
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrophy || inControl)
        {
            if (gameObject.name == "ExitBtn")
            {
                img.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                img.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else
        {
            if (gameObject.name == "ExitBtn")
            {
                img.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                img.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
