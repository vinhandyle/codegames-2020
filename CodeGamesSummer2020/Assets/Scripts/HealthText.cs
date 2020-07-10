using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Health").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            text.text = "";
        }
        else
        {
            text.text = "Health: " + GlobalControl.healthCurr + " / " + GlobalControl.healthMax;
        }
    }
}
