using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Remember to freeze y coord for rigid bodies
    public string attachedTo;
    public static string sticky;

    public static bool canSee = true;
    public static bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        sticky = attachedTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.transform.parent.name == attachedTo)
        {
            if (other.CompareTag("Wall") && !(other.gameObject.transform.parent.name == "Left Side" || other.gameObject.transform.parent.name == "Right Side"))
            {
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
                            Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < other.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().size.y /2) ||     // Player is shorter than the wall
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
                            Debug.Log(0);
                            Obstacles.refState2a_1 = "stop";
                        }
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
        }
    }
}
