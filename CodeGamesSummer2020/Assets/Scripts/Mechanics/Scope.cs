using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Remember to freeze y coord for rigid bodies
    public string attachedTo;
    public static string sticky;

    // Independent movement
    public static float x;
    public static float y;
    public float speed;
    public float range;
    public float range_1;
    public float range_2;

    public static bool seeWall = false;

    // Start is called before the first frame update
    void Start()
    {
        sticky = attachedTo;

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
                if (Obstacles.refState_5 == "warning")
                {
                    StartCoroutine(wait(2f, "refState_5"));
                }
                else if (Obstacles.refState_5 == "up")
                {
                    Obstacles.refState2_5 = "scorching";
                    if (transform.position.y < y + range_1)
                    {
                        transform.position += new Vector3(0, speed, 0);
                    }
                    else
                    {
                        StartCoroutine(wait(8f, "refState_5"));
                    }
                }
                else if (Obstacles.refState_5 == "down" && transform.position.y > y)
                {
                    if (transform.position.y > y)
                    {
                        transform.position += new Vector3(0, -speed, 0);
                    }
                    else
                    {
                        Obstacles.refState2_5 = "";
                    }
                }
            }
            else if (gameObject.name == "Warning")
            {
                if (Obstacles.refState_5 == "warning")
                {
                    transform.position = new Vector3(-1.007f, -3.75f, 0);
                }
                else if (Obstacles.refState_5 == "up")
                {
                    transform.position = new Vector3(12, 12, 0);
                }
            }

            // Charge Beam
            else if (gameObject.name == "OM_Beam_Upper")
            {
                if (Obstacles.refState1b_5 == "top")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        StartCoroutine(wait(5f, "refState1a_5"));
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0, 0);
                        }
                        else
                        {
                            StartCoroutine(wait(3f, "refState1a_5"));
                        }
                    }
                    else if (Obstacles.refState1a_5 == "finish")
                    {
                        transform.position = new Vector3(7.6526357f, 2.06f, transform.position.z);
                        Obstacles.refState1a_5 = "";
                        Obstacles.refState2a_5 = "";
                    }
                }                
            }
            else if (gameObject.name == "OM_Beam_Lower")
            {
                if (Obstacles.refState1b_5 == "bottom")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        StartCoroutine(wait(5f, "refState1a_5"));
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0, 0);
                        }
                        else
                        {
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
            }
            else if (gameObject.name == "Warning (1)")
            {
                if (Obstacles.refState1b_5 == "top")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        transform.position = new Vector3(-1.007f, 1.11f, 0);
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        transform.position = new Vector3(12, 12, 0);
                    }
                }                
            }
            else if (gameObject.name == "Warning (2)")
            {
                if (Obstacles.refState1b_5 == "bottom")
                {
                    if (Obstacles.refState1a_5 == "warning")
                    {
                        transform.position = new Vector3(-1.007f, -1.742f, 0);
                    }
                    else if (Obstacles.refState1a_5 == "beam")
                    {
                        transform.position = new Vector3(12, 12, 0);
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
                        // Logic for determining if player should be seen
                        if ((Obstacles.refState_1 == "passive_left" &&                                                                                                                                                // Facing left
                            gameObject.transform.parent.position.x > other.transform.position.x &&                                                                                                                  // To the right of the wall
                            Player.rb2D.position.x < gameObject.transform.parent.position.x &&                                                                                                                      // Player is to the left 
                            Player.rb2D.position.x + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < other.transform.position.x - other.gameObject.GetComponent<BoxCollider2D>().size.x / 2 &&     // Player is to the left of the wall
                            Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < other.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().size.y / 2) ||     // Player is shorter than the wall
                            (Obstacles.refState_1 == "passive_right" &&                                                                                                                                               // Facing right
                            gameObject.transform.parent.position.x < other.transform.position.x &&                                                                                                                  // To the left of the wall
                            Player.rb2D.position.x > gameObject.transform.parent.position.x &&                                                                                                                      // Player is to the right
                            Player.rb2D.position.x - Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius > other.transform.position.x + other.gameObject.GetComponent<BoxCollider2D>().size.x / 2 &&     // Player is to the right of the wall
                            Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < other.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().size.y / 2) ||    // Player is shorter than the wall
                            (Obstacles.refState_1 == "passive_left" && Player.rb2D.position.x > gameObject.transform.parent.position.x) ||                                                                            // Player is behind
                            (Obstacles.refState_1 == "passive_right" && Player.rb2D.position.x < gameObject.transform.parent.position.x))
                        {
                            Obstacles.refState2_1 = "passive";
                        }
                        else
                        {
                            Obstacles.refState2_1 = "";
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
            else if(!seeWall)
            {
                if(sticky.Substring(0, 7) == "Pursuit")
                {
                    Obstacles.refState2_1 = "";
                    Obstacles.refState2a_1 = "";
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
            else if (other.CompareTag("Wall") && !(other.gameObject.transform.parent.name == "Left Side" || other.gameObject.transform.parent.name == "Right Side"))
            {
                // Update to see if there are still any walls blocking vision
                if(sticky.Substring(0, 7) == "Pursuit")
                {
                    seeWall = false;
                }
            }
        }
    }

    IEnumerator wait(float time, string var)
    {
        string s = "";

        if (var == "refState_5")
        {
            s = Obstacles.refState_5;
            Obstacles.refState_5 = "";
        }
        else if (var == "refState1a_5")
        {
            s = Obstacles.refState1a_5;
            Obstacles.refState1a_5 = "";
        }
        yield return new WaitForSeconds(time);
        if (var == "refState_5")
        {
            if (s == "warning")
            {
                Obstacles.refState_5 = "up";
            }
            else if (s == "up")
            {
                Obstacles.refState_5 = "down";
            }
        }
        else if (var == "refState1a_5")
        {
            if (s == "warning")
            {
                Obstacles.refState1a_5 = "beam";
            }
            else if (s == "beam")
            {
                Obstacles.refState1a_5 = "finish";
            }
        }
    }
}
