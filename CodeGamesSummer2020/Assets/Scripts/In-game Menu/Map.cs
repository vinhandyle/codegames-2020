using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    Image img;

    // For areas where the nav module shouldn't work in lore
    public Sprite unknown;      

    // World map, areas load in when player goes there for the first time
    // DH (6 variants), SG (5 variants), TT (4 variants), MB (3 variants), IT (2 variants), GP (1 variant)
    // 6 unique variants with additional variants for highlighting area
    // DH: 0-5, SG: 6-10, TT: 11-14, MB: 15-17, IT: 18-19, GP: 20
    // Load in order: DH -> SG -> TT -> MB -> IT -> GP
    public List<Sprite> world;

    // Local maps, all areas are loaded in
    // SG: 1-12 (14 unique- 9 & 9S share)
    // TT: 1-12 & 16 (10 unique- 6 & 6S, 7 & 7S, 8 & 9, 10 & 11 share), 13-15 (3 unique, 14 shares with other 3)
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

            if (GlobalControl.prevArea == null || GlobalControl.prevArea == "")
                img.sprite = null;
            else if (GlobalControl.prevArea.Substring(0, 2) == "IT" || GlobalControl.prevArea == "Start_")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.gp)
                        img.sprite = world[19];
                    else if (GlobalControl.it)
                        img.sprite = world[18];
                }
                else
                {

                }
            }
            else if (GlobalControl.prevArea.Substring(0, 2) == "GP")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.gp)
                        img.sprite = world[20];
                }
                else
                {

                }
            }
            else if (GlobalControl.prevArea.Substring(0, 2) == "SG")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.gp)
                        img.sprite = world[10];
                    else if (GlobalControl.it)
                        img.sprite = world[9];
                    else if (GlobalControl.mb)
                        img.sprite = world[8];
                    else if (GlobalControl.tt)
                        img.sprite = world[7];
                    else if (GlobalControl.sg)
                        img.sprite = world[6];
                }
                else
                {
                    if (GlobalControl.prevArea == "SG_1")
                        img.sprite = sg[0];
                    else if (GlobalControl.prevArea == "SG_2")
                        img.sprite = sg[1];
                    else if (GlobalControl.prevArea == "SG_2S")
                        img.sprite = sg[2];
                    else if (GlobalControl.prevArea == "SG_3")
                        img.sprite = sg[3];
                    else if (GlobalControl.prevArea == "SG_4")
                        img.sprite = sg[4];
                    else if (GlobalControl.prevArea == "SG_5")
                        img.sprite = sg[5];
                    else if (GlobalControl.prevArea == "SG_6")
                        img.sprite = sg[6];
                    else if (GlobalControl.prevArea == "SG_7")
                        img.sprite = sg[7];
                    else if (GlobalControl.prevArea == "SG_8")
                        img.sprite = sg[8];
                    else if (GlobalControl.prevArea == "SG_9" || GlobalControl.prevArea == "SG_9S")
                        img.sprite = sg[9];
                    else if (GlobalControl.prevArea == "SG_10")
                        img.sprite = sg[10];
                    else if (GlobalControl.prevArea == "SG_11")
                        img.sprite = sg[11];
                    else if (GlobalControl.prevArea == "SG_11S")
                        img.sprite = sg[12];
                    else if (GlobalControl.prevArea == "SG_12")
                        img.sprite = sg[13];
                }
            }
            else if (GlobalControl.prevArea.Substring(0, 2) == "TT")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.gp)
                        img.sprite = world[14];
                    else if (GlobalControl.it)
                        img.sprite = world[13];
                    else if (GlobalControl.mb)
                        img.sprite = world[12];
                    else if (GlobalControl.tt)
                        img.sprite = world[11];
                }
                else
                {
                    if (GlobalControl.prevArea == "TT_1")
                        img.sprite = tt[0];
                    else if (GlobalControl.prevArea == "TT_2")
                        img.sprite = tt[1];
                    else if (GlobalControl.prevArea == "TT_4")
                        img.sprite = tt[2];
                    else if (GlobalControl.prevArea == "TT_5")
                        img.sprite = tt[3];
                    else if (GlobalControl.prevArea == "TT_6" || GlobalControl.prevArea == "TT_6S")
                        img.sprite = tt[4];
                    else if (GlobalControl.prevArea == "TT_7" || GlobalControl.prevArea == "TT_7S")
                        img.sprite = tt[5];
                    else if (GlobalControl.prevArea == "TT_8" || GlobalControl.prevArea == "TT_9")
                        img.sprite = tt[6];
                    else if (GlobalControl.prevArea == "TT_10" || GlobalControl.prevArea == "TT_11")
                        img.sprite = tt[7];
                    else if (GlobalControl.prevArea == "TT_12")
                        img.sprite = tt[8];
                    else if (GlobalControl.prevArea == "TT_13" || (GlobalControl.prevArea == "TT_14" && GlobalControl.pod_location == "main"))
                        img.sprite = tt[9];
                    else if (GlobalControl.prevArea == "TT_14S" || (GlobalControl.prevArea == "TT_14" && GlobalControl.pod_location == "far"))
                        img.sprite = tt[10];
                    else if (GlobalControl.prevArea == "TT_15" || (GlobalControl.prevArea == "TT_14" && GlobalControl.pod_location == "seaside"))
                        img.sprite = tt[11];
                    else if (GlobalControl.prevArea == "TT_16")
                        img.sprite = tt[12];
                }
            }
            else if (GlobalControl.prevArea.Substring(0, 2) == "MB")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.gp)
                        img.sprite = world[17];
                    else if (GlobalControl.it)
                        img.sprite = world[16];
                    else if (GlobalControl.mb)
                        img.sprite = world[15];
                }
                else
                {

                }
            }
            else if (GlobalControl.prevArea.Substring(0, 2) == "DH" || GlobalControl.prevArea.Substring(0, 2) == "FS")
            {
                if (GlobalControl.map == "world")
                {
                    if (GlobalControl.prevArea.Substring(0, 2) == "DH")
                    {
                        if (GlobalControl.gp)
                            img.sprite = world[5];
                        else if (GlobalControl.it)
                            img.sprite = world[4];
                        else if (GlobalControl.mb)
                            img.sprite = world[3];
                        else if (GlobalControl.tt)
                            img.sprite = world[2];
                        else if (GlobalControl.sg)
                            img.sprite = world[1];
                        else
                            img.sprite = world[0];
                    }
                    else if(GlobalControl.gp)
                        img.sprite = world[20];
                }
                else
                {
                    img.sprite = unknown;
                }
            }
            else
                img.sprite = null;
        }
        else
        { // Close menu
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
