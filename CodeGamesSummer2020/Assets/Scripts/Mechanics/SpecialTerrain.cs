using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTerrain : MonoBehaviour
{
    public BoxCollider2D box;
    public SpriteRenderer sprite;
    public List<Sprite> sprites;

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
