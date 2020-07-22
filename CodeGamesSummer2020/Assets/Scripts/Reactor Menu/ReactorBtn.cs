using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorBtn : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ReactorManage.open && 
            ((gameObject.name == "PrimaryBtn") || 
            (gameObject.name == "ChallengeBtn" && GlobalControl.unstableUnlocked) ||
            (gameObject.name == "SecretBtn" && GlobalControl.familiarUnlocked)))
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
