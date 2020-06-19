using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapNum : MonoBehaviour
{
    public Text text;
    public static bool scrapFound = true;
    public static int scrapNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("ScrapNum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBtn.inMenu && !Map.mapOpen && !InfoBtn.infoPage)
        {
            text.text = "" + scrapNum;
        }
        else
        {
            text.text = "";
        }
    }
}
