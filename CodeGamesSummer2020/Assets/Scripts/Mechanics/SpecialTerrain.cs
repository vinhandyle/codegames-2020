using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTerrain : MonoBehaviour
{
    public BoxCollider2D box;
    public SpriteRenderer sprite;
    public List<Sprite> sprites;

    // Moving Platform
    public bool moving;
    public bool discrete;
    public string type;
    public string state;
    public bool playerOn;

    public float x;
    public float y;
    public float pX;
    public float pY;

    public float speed;

    public float range;
    public float range_1;
    public float range_2;

    public static bool platform;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Set start and range
        x = transform.position.x;
        y = transform.position.y;

        if (range_1 + range_2 == 0)
        {
            range_1 = range;
            range_2 = range;
        }
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

        // Moving Platform
        if (discrete)
        {
            if (moving)
            {
                if (type == "line_h")
                {
                    if (state == "right")
                    {
                        if (transform.position.x > x + range_1)
                            state = "left";
                        else
                        {
                            transform.position += new Vector3(speed, 0);
                            if (playerOn)
                                Player.rb2D.position += new Vector2(speed, 0);
                        }
                    }
                    else if (state == "left")
                    {
                        if (transform.position.x < x - range_2)
                            state = "right";
                        else
                        {
                            transform.position += new Vector3(-speed, 0);
                            if (playerOn)
                                Player.rb2D.position += new Vector2(-speed, 0);
                        }
                    }
                }
                else if (type == "line_v")
                {
                    if (state == "up")
                    {
                        if (transform.position.y > y + range_1)
                            state = "down";
                        else
                        {
                            transform.position += new Vector3(0, speed);
                            if (playerOn)
                                Player.rb2D.position += new Vector2(0, speed);
                        }
                    }
                    else if (state == "down")
                    {
                        if (transform.position.y < y - range_2)
                            state = "up";
                        else
                        {
                            transform.position += new Vector3(0, -speed);
                            if (playerOn)
                                Player.rb2D.position += new Vector2(0, -speed);
                        }
                    }
                }                
            }
        }
        else
        {
            if (state == "right")
            {
                transform.position += new Vector3(speed, 0);
                if (playerOn)
                    Player.rb2D.position += new Vector2(speed, 0);

                if (transform.position.x > 5 + box.size.x / 2)
                {
                    transform.position = new Vector3(-6 - box.size.x / 2, transform.position.y);
                }
            }
            else if (state == "left")
            {
                transform.position += new Vector3(-speed, 0);
                if (playerOn)
                    Player.rb2D.position += new Vector2(-speed, 0);

                if (transform.position.x < -6 - box.size.x / 2)
                {
                    transform.position = new Vector3(5 + box.size.x / 2, transform.position.y);
                }
            }
            else if (state == "up")
            {
                transform.position += new Vector3(0, speed);
                if (playerOn)
                    Player.rb2D.position += new Vector2(0, speed);

                if (transform.position.x > 5 + box.size.y / 2)
                {
                    transform.position = new Vector3(transform.position.x, -6 - box.size.y / 2);
                }
            }
            else if (state == "Down")
            {
                transform.position += new Vector3(0, -speed);
                if (playerOn)
                    Player.rb2D.position += new Vector2(0, -speed);

                if (transform.position.x < -6 - box.size.y / 2)
                {
                    transform.position = new Vector3(transform.position.x, 5 + box.size.x / 2);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            if (GlobalControl.area == "TT_14")
            {
                other.gameObject.SetActive(false);
                VacPod.inMotion = true;
            }            
        }

        if (other.CompareTag("Player"))
        {
            if(moving)
                playerOn = true;

            if (GlobalControl.area == "DH_5S")
            {
                Player.rb2D.position = new Vector2(-2.81f, 2.909f);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerOn = false;
    }
}
