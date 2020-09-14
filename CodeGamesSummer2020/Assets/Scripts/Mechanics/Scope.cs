using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Components
    public SpriteRenderer sprite;
    public Animator anim;
    public List<Sprite> sprites;
    // Remember to freeze y coord for rigid bodies
    public string attachedTo;

    // Timer
    public int time;
    public bool hold_time = false;

    // Independent movement
    public float x;
    public float y;
    public float x1;
    public float y1;
    public float speed;
    public float range;
    public float range_1;
    public float range_2;

    // Used for single trigger
    public bool once = false;
    public bool once_1 = false;

    // Pursuit
    public string reference;                // Scope->Obstacles
    public static bool seeWall;             // Is there a wall in the way?
    public static bool leftWall;            // Is there any wall to the left?
    public static bool rightWall;           // Is there any wall to the right?
    public static string seePlayer = null;  // Can the player be seen?

    // Turret
    public static string inRange;

    // Crusher
    public bool canCrush = false;
    public static string signal;

    // Start is called before the first frame update
    void Start()
    {

        // Save starting position and set ranges
        x = transform.position.x;
        y = transform.position.y;
        if (range_1 + range_2 == 0)
        {
            range_1 = range;
            range_2 = range;
        }        

        // Set position
        if (gameObject.name == "Warning" || gameObject.name == "Warning (1)" || gameObject.name == "Warning (2)")
        {
            transform.position = new Vector3(12, 12, 0);
        }

        // Get Components
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name == "Detect_Player")
            {
                transform.position = new Vector3(x1, y1, transform.position.z);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            else if (gameObject.name == "Detect_Player (1)")
            {
                if (Obstacles.refState2_4 && inRange == transform.parent.name)
                    sprite.sprite = sprites[0];
                else
                    sprite.sprite = sprites[1];
            }
            else if (gameObject.name == "Face_Player")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
                Obstacles.refState2a_4 = transform.localEulerAngles.z;
            }
        }
        else if (transform.parent.transform.parent.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name == "Base")
            {
                Transform p = transform.parent.transform.parent;
                transform.rotation = p.rotation;
                transform.position = new Vector3(p.position.x - p.GetComponent<BoxCollider2D>().size.x * Mathf.Cos(p.localEulerAngles.z * Mathf.Deg2Rad) / 2, p.position.y - p.GetComponent<BoxCollider2D>().size.y * Mathf.Sin(p.localEulerAngles.z * Mathf.Deg2Rad), transform.position.z); ;
            }
        }

        // Overseer Machina
        if (GlobalControl.area == "SG_12")
        {
            // Scorched Earth
            if (gameObject.name == "Molten_")
            {
                // Move obj up or down
                if (Obstacles.refState_5 == "warning" && !once)
                {
                    once = true;
                    StartCoroutine(wait(2f, "refState_5"));
                }
                else if (Obstacles.refState_5 == "up")
                {
                    Obstacles.refState2_5 = "scorching";
                    if (transform.position.y < y + range_1)
                    {
                        transform.position += new Vector3(0, speed, 0);
                    }
                    else if (!once)
                    {
                        once = true;
                        StartCoroutine(wait(8f, "refState_5"));
                    }
                }
                else if (Obstacles.refState_5 == "down" && transform.position.y > y)
                {
                    if (transform.position.y > y)
                    {
                        transform.position += new Vector3(0, -speed, 0);
                    }
                    else if (!once)
                    {
                        once = true;
                        Obstacles.refState2_5 = "";
                    }
                }
            }
            else if (gameObject.name == "Warning")
            {
                if (Obstacles.refState_5 == "warning")
                {
                    transform.position = new Vector3(-1.007f, -3.76f, 0);
                }
                else if (Obstacles.refState_5 == "up")
                {
                    transform.position = new Vector3(12, 12, 0);
                }

                if (GlobalControl.downed_boss_1)
                {
                    gameObject.SetActive(false);
                }
            }

            // Charge Beam
            else if (gameObject.name == "OM_Beam_Upper")
            {
                if (Obstacles.refState1b_5 == "top")
                {
                    if (Obstacles.refState1a_5 == "warning" && !once_1)
                    {
                        once_1 = true;
                        StartCoroutine(wait(5f, "refState1a_5"));
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0, 0);
                        }
                        else if (!once_1)
                        {
                            once_1 = true;
                            StartCoroutine(wait(3f, "refState1a_5"));
                        }
                    }
                    else if (Obstacles.refState1a_5 == "finish")
                    {
                        transform.position = new Vector3(9.9426357f, 2.05f, transform.position.z);
                        Obstacles.refState1a_5 = "";
                        Obstacles.refState2a_5 = "";
                    }
                }

                if (GlobalControl.downed_boss_1)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (gameObject.name == "OM_Beam_Lower")
            {
                if (Obstacles.refState1b_5 == "bottom")
                {
                    if (Obstacles.refState1a_5 == "warning" && !once_1)
                    {
                        once_1 = true;
                        StartCoroutine(wait(5f, "refState1a_5"));
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0, 0);
                        }
                        else if (!once_1)
                        {
                            once_1 = true;
                            StartCoroutine(wait(3f, "refState1a_5"));
                        }
                    }
                    else if (Obstacles.refState1a_5 == "finish")
                    {
                        transform.position = new Vector3(9.9426357f, -1.94f, transform.position.z);
                        Obstacles.refState1a_5 = "";
                        Obstacles.refState2a_5 = "";
                    }
                }

                if (GlobalControl.downed_boss_1)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (gameObject.name == "Warning (1)")
            {
                if (Obstacles.refState1b_5 == "top")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        transform.position = new Vector3(-1.007f, 1.65f, 0);
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        transform.position = new Vector3(12, 12, 0);
                    }
                }

                if (GlobalControl.downed_boss_1)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (gameObject.name == "Warning (2)")
            {
                if (Obstacles.refState1b_5 == "bottom")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        transform.position = new Vector3(-1.007f, -1.7f, 0);
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        transform.position = new Vector3(12, 12, 0);
                    }
                }

                if (GlobalControl.downed_boss_1)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        // Containment Machina
        if (GlobalControl.area == "TT_12")
        {
            if (gameObject.name == "Sparkle")
            {
                if (Obstacles.refState_6 == "warning")
                {
                    sprite.enabled =  true;
                    anim.enabled = true;
                }
                else
                {
                    sprite.enabled = false;
                    anim.enabled = false;
                }
            }
            else if (gameObject.name == "Sparkle_1")
            {
                if (Obstacles.refState_6 == "warning_1")
                {
                    sprite.enabled = true;
                    anim.enabled = true;
                }
                else
                {
                    sprite.enabled = false;
                    anim.enabled = false;
                }
            }
        }
        // Subnautical Machina
        if (GlobalControl.area == "MB_12")
        {
            if (gameObject.name == "Rain")
            {
                if ((Obstacles.refState1b_7 == "pouring") || (Obstacles.refState1b_7 == "storming"))
                {
                    // Rainfall
                    if (time >= 10)
                    {
                        time = 0;
                        transform.position = new Vector3(0, y);
                        Obstacles.refState1b_7 = "finish";
                    }
                    else if(!hold_time)
                        StartCoroutine(addSecond());
                    else
                    {
                        if (Obstacles.refState1b_7 == "pouring")
                            transform.position += new Vector3(0, -speed);
                        else if (Obstacles.refState1b_7 == "storming")
                            transform.position += new Vector3(0, -speed * 2);
                    }
                }
            }
            else if (gameObject.name.Substring(0, 4) == "Drop")
            {
                // Droplet Randomization
                if (!once && (Obstacles.refState1b_7 == "pouring" || Obstacles.refState1b_7 == "storming"))
                {
                    once = true;
                    float rand = Random.Range(-4f, 4.1f);
                    float rand2 = Random.Range(-7f, 16.1f);

                    if (transform.parent.name == "Random")
                        transform.position = new Vector3(rand, transform.parent.position.y + rand2);
                    else
                        transform.position = new Vector3(rand, y);
                }
                else if (Obstacles.refState1b_7 == "finish")
                {
                    once = false;
                }
            }

            if (GlobalControl.downed_boss_3)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent.name == attachedTo)
        {
            if (other.name == "Player")
            {
                if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 6) == "Aerial")
                {
                    Obstacles.refState2_2 = "in";
                    signal = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 7) == "Aquatic")
                {
                    Obstacles.refState2_3 = "in";
                    signal = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 6) == "Turret")
                {
                    inRange = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player (1)" && transform.parent.name.Substring(0, 6) == "Turret" && inRange == transform.parent.name)
                {
                    Obstacles.refState2_4 = true;
                    signal = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player" && transform.parent.name == "Containment")
                {
                    Obstacles.refState2_6 = "near";
                }
            }
            else if ((other.transform.parent.name == "Top Ceiling" || other.transform.parent.name == "Bottom Floor" || 
                other.transform.parent.name == "Left Side" || other.transform.parent.name == "Right Side" || 
                other.transform.parent.name == "Destructibles") && other.transform.parent != null)
            {
                if (gameObject.name == "Detect_Outer" && transform.parent.name == "Containment")
                {
                    Obstacles.refState2a_6 = "stop";
                }
            }
        }
        else if (gameObject.name == attachedTo)
        {
            if (other.CompareTag("Floor") || other.CompareTag("Ceiling") || other.CompareTag("Wall"))
            {
                if (gameObject.name.Substring(0, 6) == "Crush_")
                {
                    if (other.name.Length < 8 || other.name.Substring(0, 7) != "Crusher" &&
                        ((other.transform.position.y > Player.rb2D.position.y && other.transform.position.y > transform.position.y && Player.rb2D.position.y > transform.position.y) ||
                        (other.transform.position.y < Player.rb2D.position.y && other.transform.position.y < transform.position.y && Player.rb2D.position.y < transform.position.y)))
                    {
                        canCrush = true;
                    }
                    else
                    {
                        canCrush = false;
                    }
                }
            }
            else if (other.name == "Player" && canCrush)
            {
                // Deal damage
                if (!GlobalControl.immune)
                {
                    GlobalControl.healthCurr -= 2;
                    StartCoroutine(IFrame());
                }

                // Push player
                if (transform.position.x > Player.rb2D.position.x)
                {
                    Player.rb2D.position = new Vector2(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 - Player.rb2D.GetComponent<CircleCollider2D>().radius, Player.rb2D.position.y);
                }
                else if (transform.position.x < Player.rb2D.position.x)
                {
                    Player.rb2D.position = new Vector2(transform.position.x + GetComponent<BoxCollider2D>().size.x / 2 + Player.rb2D.GetComponent<CircleCollider2D>().radius, Player.rb2D.position.y);
                }
                else
                {
                    float num = Random.Range(0, 2);
                    if (num == 0)
                    {
                        Player.rb2D.velocity += new Vector2(-1f, 0);
                    }
                    else
                    {
                        Player.rb2D.velocity += new Vector2(-1f, 0);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.transform.parent.name == attachedTo)
        {
            if (other.CompareTag("Wall") && !(other.gameObject.transform.parent.name == "Left Side" || other.gameObject.transform.parent.name == "Right Side"))
            {
                seeWall = true;
                // Pursuit Interaction
                if (attachedTo.Substring(0, 7) == "Pursuit")
                {
                    // Walls block vision
                    if (gameObject.name == "Detect_Player")
                    {
                        // Reset sight if player goes over wall
                        if (other.transform.position.y + other.GetComponent<BoxCollider2D>().size.y / 2 < Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius)
                            seePlayer = null;

                        // Checks if there are any walls on either side
                        if (Obstacles.refState_1 == "passive_left" && other.transform.position.x < transform.position.x)
                        {
                            leftWall = true;
                        }
                        else if (Obstacles.refState_1 == "passive_right" && other.transform.position.x > transform.position.x)
                        {
                            rightWall = true;
                        }

                        // Facing player
                        if ((Obstacles.refState_1 == "passive_left" && Player.rb2D.position.x < transform.position.x) || (Obstacles.refState_1 == "passive_right" && Player.rb2D.position.x > transform.position.x))
                        {
                            // Wall is behind player and in front of self
                            if ((Obstacles.refState_1 == "passive_left" && other.transform.position.x < Player.rb2D.position.x && other.transform.position.x < transform.position.x) ||
                                (Obstacles.refState_1 == "passive_right" && other.transform.position.x > Player.rb2D.position.x && other.transform.position.x > transform.position.x))
                            {
                                // If no walls are in front of player
                                if (seePlayer != "false" && seePlayer != null)
                                {
                                    reference = "";
                                    Obstacles.refState2_1 = reference;
                                }
                            }
                            // If there is no wall in the direction of the player but a wall behind self
                            else if ((Obstacles.refState_1 == "passive_left" && other.transform.position.x > Player.rb2D.position.x && other.transform.position.x > transform.position.x && !leftWall) ||
                                    (Obstacles.refState_1 == "passive_right" && other.transform.position.x < Player.rb2D.position.x && other.transform.position.x < transform.position.x && !rightWall))
                            {
                                // If no walls are in front of player
                                if (seePlayer != "false" && seePlayer != null)
                                {
                                    reference = "";
                                    Obstacles.refState2_1 = reference;
                                }
                            }
                            // Walls blocking sight
                            else
                            {
                                reference = "passive";
                                Obstacles.refState2_1 = reference;
                                seePlayer = "false";
                            }

                            // Allows all walls to be viewed
                            if (seePlayer != "false")
                                seePlayer = "";
                        }
                        // Not facing player
                        else
                        {
                            reference = "passive";
                            Obstacles.refState2_1 = reference;
                        }
                    }
                    // Walls stop movement
                    else if (gameObject.name == "Detect_Wall")
                    {
                        // Logic for following player when blocked by a wall
                        // If player is in the same direction as the blocking wall, stop
                        // If player is now in the opposite direction, pursue
                        // The wall's top must be above the Machina's bottom to stop it
                        if ((Obstacles.refState_1 == "hostile_left" && Player.rb2D.position.x < gameObject.transform.parent.position.x &&
                            other.transform.position.x < gameObject.transform.parent.position.x &&
                            other.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().size.y / 2 > gameObject.transform.parent.position.y - gameObject.transform.parent.GetComponent<BoxCollider2D>().size.y / 2) ||
                            (Obstacles.refState_1 == "hostile_right" && Player.rb2D.position.x > gameObject.transform.parent.position.x &&
                            other.transform.position.x > gameObject.transform.parent.position.x &&
                            other.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().size.y / 2 > gameObject.transform.parent.position.y - gameObject.transform.parent.GetComponent<BoxCollider2D>().size.y / 2))
                        {
                            Obstacles.refState2a_1 = "stop";
                        }
                    }
                }
            }
            // No walls detected
            else if (!seeWall)
            {
                if (attachedTo.Substring(0, 7) == "Pursuit")
                {
                    if ((Obstacles.refState_1 == "passive_left" && Player.rb2D.position.x > gameObject.transform.parent.position.x) ||
                        (Obstacles.refState_1 == "passive_right" && Player.rb2D.position.x < gameObject.transform.parent.position.x))
                    {
                        reference = "passive";
                        Obstacles.refState2_1 = reference;
                    }
                    else
                    {
                        reference = "";
                        Obstacles.refState2_1 = reference;
                    }
                    Obstacles.refState2a_1 = "";
                }
            }
        }
        else if (gameObject.name == attachedTo)
        {
            if (other.CompareTag("Floor") || other.CompareTag("Ceiling") || other.CompareTag("Wall"))
            {
                if (gameObject.name.Substring(0, 6) == "Crush_")
                {
                    if ((other.name.Length < 8 || other.name.Substring(0, 7) != "Crusher") &&
                        ((other.transform.position.y > Player.rb2D.position.y && other.transform.position.y > transform.position.y && Player.rb2D.position.y > transform.position.y) ||
                        (other.transform.position.y < Player.rb2D.position.y && other.transform.position.y < transform.position.y && Player.rb2D.position.y < transform.position.y)))
                    {
                        canCrush = true;
                    }
                    else if((other.transform.position.y < Player.rb2D.position.y && transform.position.y < Player.rb2D.position.y) || (other.transform.position.y > Player.rb2D.position.y && transform.position.y > Player.rb2D.position.y))
                    {
                        canCrush = false;
                    }
                }
            }
            else if (other.name == "Player" && canCrush)
            {
                // Deal damage
                if (!GlobalControl.immune)
                {
                    GlobalControl.healthCurr -= 2;
                    StartCoroutine(IFrame());
                }

                // Push player
                if (transform.position.x > Player.rb2D.position.x)
                {
                    Player.rb2D.position = new Vector2(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 - Player.rb2D.GetComponent<CircleCollider2D>().radius, Player.rb2D.position.y);
                }
                else if (transform.position.x < Player.rb2D.position.x)
                {
                    Player.rb2D.position = new Vector2(transform.position.x + GetComponent<BoxCollider2D>().size.x / 2 + Player.rb2D.GetComponent<CircleCollider2D>().radius, Player.rb2D.position.y);
                }
                else
                {
                    float num = Random.Range(0, 2);
                    if (num == 0)
                    {
                        Player.rb2D.velocity += new Vector2(-1f, 0);
                    }
                    else
                    {
                        Player.rb2D.velocity += new Vector2(-1f, 0);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.transform.parent.name == attachedTo)
        {
            if (other.CompareTag("Floor") && GlobalControl.area != "TT_12")
            {
                // Pursuit Interaction
                if (attachedTo.Substring(0, 7) == "Pursuit")
                {
                    // Logic for following player when floor ends
                    // If left or right colliders leaves floor collider, stop
                    if ((gameObject.name == "Detect_Floor_Left" && Obstacles.refState_1 == "hostile_left" && transform.position.x + GetComponent<BoxCollider2D>().size.x / 2 < other.transform.position.x - other.gameObject.GetComponent<BoxCollider2D>().size.x / 2) ||
                        (gameObject.name == "Detect_Floor_Right" && Obstacles.refState_1 == "hostile_right" && transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 > other.transform.position.x + other.gameObject.GetComponent<BoxCollider2D>().size.x / 2))
                    {
                        Obstacles.refState2a_1 = "stop";
                    }
                }
            }
            else if (gameObject.name == "Detect_Player" && other.CompareTag("Wall") && !(other.gameObject.transform.parent.name == "Left Side" || other.gameObject.transform.parent.name == "Right Side"))
            {
                // Update to see if there are still any walls blocking vision
                if (attachedTo.Substring(0, 7) == "Pursuit")
                {
                    seeWall = false;
                }
            }
            else if (other.name == "Player")
            {
                if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 6) == "Aerial")
                {
                    Obstacles.refState2_2 = "out";
                    signal = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 7) == "Aquatic")
                {
                    Obstacles.refState2_3 = "out";
                    signal = transform.parent.name;
                }
                if (gameObject.name == "Detect_Player" && transform.parent.name.Substring(0, 6) == "Turret")
                {
                    inRange = "";
                    Obstacles.refState2_4 = false;
                    signal = transform.parent.name;
                }
                else if (gameObject.name == "Detect_Player" && transform.parent.name == "Containment")
                {
                    Obstacles.refState2_6 = "far";
                }
            }
            else if (other.transform.parent.name == "Top Ceiling" || other.transform.parent.name == "Bottom Floor" ||
                other.transform.parent.name == "Left Side" || other.transform.parent.name == "Right Side" ||
                other.transform.parent.name == "Destructibles")
            {
                if (gameObject.name == "Detect_Outer" && transform.parent.name == "Containment")
                {
                    Obstacles.refState2a_6 = "";
                }
            }
            else if (gameObject.name == attachedTo)
            {
                if (gameObject.name.Substring(0, 6) == "Crush_")
                {
                    if (other.name.Substring(0, 7) != "Crusher" && (other.CompareTag("Floor") || other.CompareTag("Ceiling")))
                    {
                        canCrush = false;
                    }
                }
            }
        }
    }

    IEnumerator wait(float time, string var)
    {
        yield return new WaitForSeconds(time);

        if (var == "refState_5")
        {
            if (Obstacles.refState_5 == "warning")
            {
                Obstacles.refState_5 = "up";
            }
            else if (Obstacles.refState_5 == "up")
            {
                Obstacles.refState_5 = "down";
            }
            once = false;
        }
        else if (var == "refState1a_5")
        {
            if (Obstacles.refState1a_5 == "warning")
            {
                Obstacles.refState1a_5 = "beam";
            }
            else if (Obstacles.refState1a_5 == "beam")
            {
                Obstacles.refState1a_5 = "finish";
            }
            once_1 = false;
        }
    }

    IEnumerator IFrame()
    {
        GlobalControl.immune = true;
        yield return new WaitForSeconds(1f);
        GlobalControl.immune = false;
    }

    IEnumerator addSecond()
    {
        hold_time = true;
        yield return new WaitForSeconds(1f);
        time++;
        hold_time = false;
    }
}
