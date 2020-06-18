using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBtnText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("InfoBtnText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu)
        {
            if (InfoBtn.infoPage)
            {
                text.text = "Close";
            }
            else
            {
                text.text = "Help";
            }
        }
        else
        {
            text.text = "";
        }
    }
}
