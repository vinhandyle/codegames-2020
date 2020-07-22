using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBtn : MonoBehaviour
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
        if (MenuBackground.inMenu)
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            if (GlobalControl.area == "GP_0A")
            {
                if (GlobalControl.counter_1 < 2 && gameObject.name == "Next")
                {                   
                    img.color = new Color(1f, 1f, 1f, 1f);                   
                }
                else if (GlobalControl.counter_1 == 2 && gameObject.name != "Next")
                {                    
                    img.color = new Color(1f, 1f, 1f, 1f);                    
                }
                else
                {
                    img.color = new Color(1f, 1f, 1f, 0f);
                }
            }
        }       
    }
}
