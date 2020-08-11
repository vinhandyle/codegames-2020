using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Remember to freeze y coord for rigid bodies
    public string attachedTo;
    public static string sticky;

    // Independent movement
    public float x;
    public float y;
    public float speed;
    public float range;
    public float range_1;
    public float range_2;

    // Used for single trigger
    public static bool once = false;
    public static bool once_1 = false;

    // Pursuit
    public static bool seeWall = false;     // Is there a wall in the way?
    public static bool leftWall = false;    // Is there any wall to the left?
    public static bool rightWall = false;   // Is there any wall to the right?
    public static string seePlayer = null;  // Can the player be seen?

    // Crusher
    public bool canCrush = false;

    // Start is called before the first frame update
    void Start()
    {
        sticky = attachedTo;

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
    }

    // Update is called once per frame
    void Update()
    {
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
                        transform.position = new Vector3(7.6526357f, 2.05f, transform.position.z);
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
                        transform.position = new Vector3(7.6526357f, -1.94f, transform.position.z);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == attachedTo)
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
                if (sticky.Substring(0, 7) == "Pursuit")
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
                                    Obstacles.refState2_1 = "";
                                }
                            }
                            // If there is no wall in the direction of the player but a wall behind self
                            else if ((Obstacles.refState_1 == "passive_left" && other.transform.position.x > Player.rb2D.position.x && other.transform.position.x > transform.position.x && !leftWall) ||
                                    (Obstacles.refState_1 == "passive_right" && other.transform.position.x < Player.rb2D.position.x && other.transform.position.x < transform.position.x && !rightWall))
                            {
                                // If no walls are in front of player
                                if (seePlayer != "false" && seePlayer != null)
                                {
                                    Obstacles.refState2_1 = "";
                                }
                            }
                            // Walls blocking sight
                            else
                            {
                                Obstacles.refState2_1 = "passive";
                                seePlayer = "false";
                            }

                            // Allows all walls to be viewed
                            if (seePlayer != "false")
                                seePlayer = "";
                        }
                        // Not facing player
                        else
                        {
                            Obstacles.refState2_1 = "passive";
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
                if (sticky.Substring(0, 7) == "Pursuit")
                {
                    if ((Obstacles.refState_1 == "passive_left" && Player.rb2D.position.x > gameObject.transform.parent.position.x) ||
                        (Obstacles.refState_1 == "passive_right" && Player.rb2D.position.x < gameObject.transform.parent.position.x))
                    {
                        Obstacles.refState2_1 = "passive";
                    }
                    else
                    {
                        Obstacles.refState2_1 = "";
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
            if (other.CompareTag("Floor"))
            {
                // Pursuit Interaction
                if (sticky.Substring(0, 7) == "Pursuit")
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
                if (sticky.Substring(0, 7) == "Pursuit")
                {
                    seeWall = false;
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
}
