using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    Image img;

    public Sprite start;
    public List<Sprite> dh;
    public List<Sprite> sg;
    public List<Sprite> tt;
    public List<Sprite> mb;
    public List<Sprite> it;
    public List<Sprite> gp;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu && GlobalControl.menu == "map")
        { 
          // If map was last open, reopen when opening menu
            img.color = new Color(1f, 1f, 1f, 1f);
            if (GlobalControl.prevArea == "Start_")
            {
                img.sprite = start;
            }
            else
            {
                img.sprite = null;
            }
        }
        else
        { // Close menu
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
