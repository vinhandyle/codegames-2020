using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTerrain : MonoBehaviour
{
    public BoxCollider2D box;
    public SpriteRenderer sprite;
    public List<Sprite> sprites;
    public static bool platform;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Scanner" && GlobalControl.area == "TT_13" && GlobalControl.keyUnlocked)
        {
            box.enabled = false;
        }
        else if (gameObject.name == "On/Off" && GlobalControl.area == "TT_14")
        {
            if (VacPod.inMotion)
            {
                sprite.sprite = sprites[1];
            }
            else
            {
                sprite.sprite = sprites[0];
            }
        }
        else if (gameObject.name == "Bay_Light_0")
        {
            if (GlobalControl.state_MB_4 == "inactive")
            {
                sprite.sprite = sprites[0];
            }
            else
            {
                sprite.sprite = sprites[1];
            }
        }
        else if (gameObject.name == "Bay_Light_1")
        {
            if (GlobalControl.state_MB_7 == "inactive")
            {
                sprite.sprite = sprites[0];
            }
            else
            {
                sprite.sprite = sprites[1];
            }
        }
        else if (gameObject.name == "Bay_Light_2")
        {
            if (GlobalControl.state_MB_8 == "inactive")
            {
                sprite.sprite = sprites[0];
            }
            else
            {
                sprite.sprite = sprites[1];
            }
        }
        else if (gameObject.name == "Bay_Light_3")
        {
            if (GlobalControl.state_MB_11 == "inactive")
            {
                sprite.sprite = sprites[0];
            }
            else
            {
                sprite.sprite = sprites[1];
            }
        }
        else if (GlobalControl.area == "MB_12")
        {
            if (GlobalControl.downed_boss_3)
            {
                platform = true;
                sprite.enabled = true;
            }
            else
            {
                platform = false;
                sprite.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            other.gameObject.SetActive(false);
            VacPod.inMotion = true;
        }
    }
}
