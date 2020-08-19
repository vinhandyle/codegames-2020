using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacPod : MonoBehaviour
{
    public string type;
    public string direction;
    public float speed;
    public float time;
    public bool canMove;
    public static bool inMotion = false;
    public static bool once = false;

    public SpriteRenderer sprite;
    public List<Sprite> sprites;
    public BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 0.01f)
        {
            speed = Random.Range(0.05f, 0.1f);
        }

        if (inMotion && !once)
        {
            once = true;
            StartCoroutine(travel());
        }

        // Ai stuff
        if (type == "main" || type == "switch" || type == "indicator")
        {
            if (GlobalControl.pod_direction == "right")
            {
                sprite.sprite = sprites[0];
            }
            else if (GlobalControl.pod_direction == "left")
            {
                sprite.sprite = sprites[1];
            }

            // Cannot interact while in motion
            if (inMotion)
            {
                box.enabled = false;
            }
            else
            {
                box.enabled = true;

                // Cannot interact if on edge stations
                if (type == "switch")
                {
                    if (GlobalControl.pod_location != "main")
                        box.enabled = false;
                }

            }
        }
        else if (type == "extra")
        {
            if (direction == "right" && canMove)
            {
                sprite.sprite = sprites[0];
                if (transform.position.x < 8.37f)
                {
                    transform.position += new Vector3(speed, 0);
                }
                else
                {
                    direction = "left";
                    canMove = false;
                    speed = Random.Range(0.05f, 0.1f);
                    StartCoroutine(wait());
                }
            }
            else if (direction == "left" && canMove)
            {
                sprite.sprite = sprites[1];
                if (transform.position.x > -8.37)
                {
                    transform.position += new Vector3(-speed, 0);
                }
                else
                {
                    direction = "right";
                    canMove = false;
                    speed = Random.Range(0.05f, 0.1f);
                    StartCoroutine(wait());
                }
            }
        }
        else if (type == "background")
        {
            if (inMotion)
            {
                // Speed must be 10^n or 5x10^n
                if (GlobalControl.pod_direction == "right")
                {
                    transform.position += new Vector3(-0.1f, 0);

                    if (transform.position.x <= -5 && (gameObject.name.Substring(0, 5) == "Light" || gameObject.name.Substring(0, 4) == "Tube"))
                    {
                        transform.position = new Vector3(5f, transform.position.y, transform.position.z);
                    }
                    else if (transform.position.x <= -10 && gameObject.name.Substring(0, 4) == "Back")
                    {
                        transform.position = new Vector3(10f, transform.position.y, transform.position.z);
                    }
                }
                else if (GlobalControl.pod_direction == "left")
                {
                    transform.position += new Vector3(0.1f, 0);

                    if (transform.position.x >= 5 && (gameObject.name.Substring(0, 5) == "Light" || gameObject.name.Substring(0, 4) == "Tube"))
                    {
                        transform.position = new Vector3(-5, transform.position.y, transform.position.z);
                    }
                    else if (transform.position.x >= 10 && gameObject.name.Substring(0, 4) == "Back")
                    {
                        transform.position = new Vector3(-10, transform.position.y, transform.position.z);
                    }
                }
            }
        }
        else if (type == "door")
        {
            if (inMotion)
            {
                box.enabled = false;
            }
            else
            {
                box.enabled = true;
            }
        }
    }

    IEnumerator wait()
    {
        time = Random.Range(0, 10);
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    IEnumerator travel()
    {
        yield return new WaitForSeconds(5f);
        inMotion = false;
        once = false;

        if (GlobalControl.pod_location == "main")
        {
            if (GlobalControl.pod_direction == "right")
            {
                GlobalControl.pod_location = "seaside";
                GlobalControl.pod_direction = "left";
            }
            else if (GlobalControl.pod_direction == "left")
            {
                GlobalControl.pod_location = "far";
                GlobalControl.pod_direction = "right";
            }
        }
        else
        {
            GlobalControl.pod_location = "main";
        }
    }
}
