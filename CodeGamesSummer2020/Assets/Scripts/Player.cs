using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Rigidbody2D rb2D;
    public Animator anim;
    public SpriteRenderer sprite;
    public List<Sprite> sprites;

    public static float moveBy = 2f; // Horizontal velocity
    private float jumpHeight = 5f; // Jump velocity

    public static float x = 0;
    public static float y = 0;
    public static float x2 = 0;
    public static float y2 = 0;
    public static float v_x = 0;
    public static float v_y = 0;

    public static bool canDash = true; // Whether the player can dash
    public static bool dashing = false; // Prevents moving while dashing
    public static bool walled = false; // Player cannot dash when against a wall
    public static bool floored = false; // Player is on floor or wall
    private bool tooLong = false; // Whether a key is held too long to dash on release
    public static string direction; // Player direction used for dashing

    // Dashing requires double-pressing quickly, these checks if the next press is going to be the first
    private bool firstPress = true;
    private bool firstLeft = false;
    private bool firstRight = false;

    // Wall Interaction
    private static bool canLeft = true; // Able to move left
    private static bool canRight = true; // Able to move right

    public static bool canJump1 = false;
    public static bool canJump1_ = false; // Whether the player can jump
    public static bool canJump2 = false;
    public static bool canJump2_ = false; // Whether the player can double-jump
    private bool jumped = false; // Whether the first jump started
    private bool midJump = false; // Whether the player is mid-jump
    private bool walledJump = false; // Prevents vertical ascension against wall

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set new player position when entering door or moving through an opening 
        if (GlobalControl.switched)
        {
            transform.position = new Vector3(x, y, transform.position.z);
            dashing = false;
            canDash = true;
            GlobalControl.switched = false;
        }
        else
        {
            x2 = rb2D.position.x;
            y2 = rb2D.position.y;
            v_x = rb2D.velocity.x;
            v_y = rb2D.velocity.y;
        }

        /*-----Player sprite-----*/

        // I-Frame
        if (GlobalControl.immune)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;

            // Set default sprite
            sprite.sprite = sprites[0];
        }

        /*-----Heartless Generator-----*/

        // Use Heartless Generator
        if (Input.GetKeyDown("s") && GlobalControl.heartlessUnlocked)
        {
            // 1 HP to 3 Energy
            if (GlobalControl.h2e && GlobalControl.energyCurr < GlobalControl.energyMax)
            {
                GlobalControl.healthCurr--;
                if (GlobalControl.energyCurr <= GlobalControl.energyMax - 3)
                {
                    GlobalControl.energyCurr += 3;
                }
                else
                {
                    GlobalControl.energyCurr = GlobalControl.energyMax;
                }
            }

            // 5 energy to 1 HP
            else if (!GlobalControl.h2e && GlobalControl.healthCurr < GlobalControl.healthMax)
            {
                GlobalControl.energyCurr -= 5;
                GlobalControl.healthCurr++;
            }
        }

        // Toggle Heartless Generator conversion type
        if (Input.GetKeyDown("f") && GlobalControl.heartlessUnlocked)
        {
            if (GlobalControl.h2e)
            {
                GlobalControl.h2e = false;
            }
            else
            {
                GlobalControl.h2e = true;
            }
        }


        /*-----Mobility-----*/

        // Updates player ability to move left/right
        if (GlobalControl.clingUnlocked)
        {
            canLeft = true;
            canRight = true;
        }
        else if (!walled)
        {
            canLeft = true;
            canRight = true;
        }
        else if (walled && rb2D.velocity.y != 0)
        { // Prevents clinging mid-jump or while falling
            if (direction == "left")
            {
                canLeft = false;
                canRight = true;
            }
            else if (direction == "right")
            {
                canRight = false;
                canLeft = true;
            }
        }
        else if (walled && rb2D.velocity.y == 0 && midJump && (!canJump1_ || !canJump2_))
        { // Prevents clinging during the apex of a jump
            if (direction == "left")
            {
                canLeft = false;
                canRight = true;
            }
            else if (direction == "right")
            {
                canRight = false;
                canLeft = true;
            }
        }
        else if (!midJump)
        { // If player is on a wall, unrestrict all
            canLeft = true;
            canRight = true;
        }

        // Moves player left
        if (Input.GetKey("a") && !dashing && canLeft)
        {
            direction = "left";
            rb2D.velocity = new Vector2(-moveBy, rb2D.velocity.y);
            if (firstPress)
            {
                firstLeft = true;
            }
            if (firstRight)
            {
                firstRight = false;
            }
        }

        // Moves player right
        if (Input.GetKey("d") && !dashing && canRight)
        {
            direction = "right";
            rb2D.velocity = new Vector2(moveBy, rb2D.velocity.y);
            if (firstPress)
            {
                firstRight = true;
            }
            if (firstLeft)
            {
                firstLeft = false;
            }
        }

        // Checks that time between the double presses is under a certain amount of time (no holding into a dash)
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && canDash && GlobalControl.dashUnlocked && !walled)
        {
            if (firstPress)
            {
                StartCoroutine(noHoldDash());
            }
            else
            {
                // Checking for first left/right prevents dashing when rapidly alternating between left/right
                if (direction == "left" && firstLeft)
                {
                    rb2D.velocity = new Vector2(-3 * moveBy, rb2D.velocity.y);
                    dashing = true;
                }
                else if (direction == "right" && firstRight)
                {
                    rb2D.velocity = new Vector2(3 * moveBy, rb2D.velocity.y);
                    dashing = true;
                }
                
                if(dashing)
                StartCoroutine(postDash());
            }
        }

        // Checks time between first release and second press
        if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && canDash && GlobalControl.dashUnlocked && firstPress && !walled && !tooLong)
        {           
            StartCoroutine(preDash());         
        }

        // Allows jumping off wall after releasing cling
        if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && walled)
        {
            walledJump = false;
            floored = false;
        }

        // Jump if player is grounded or clinging to a wall
        if (canJump1_ && (Input.GetKey("space") || Input.GetKeyDown("space")))
        {
            // Prevents jumping again mid-air
            jumped = true;
            midJump = true;

            // The actual jump
            if (canJump1 && !walledJump)
            {
                if (GlobalControl.clingUnlocked && walled)
                {
                    // Straight jump if against and not moving into wall
                    if (floored)
                    {
                        walledJump = true;
                        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
                    }
                    // Jump off wall if moving into it
                    else if (!Input.GetKey("a") && !Input.GetKey("d"))
                    {
                        if (direction == "left")
                        {
                            rb2D.velocity = new Vector2(moveBy, jumpHeight);
                        }
                        else if (direction == "right")
                        {
                            rb2D.velocity = new Vector2(-moveBy, jumpHeight);
                        }
                    }                    
                }
                else
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
                }
                canJump1 = false;
            }
        }

        // Double jump
        if (GlobalControl.doubleUnlocked && ((canJump2 && Input.GetKeyDown("space")) || (!canJump2 && canJump2_ && Input.GetKey("space"))))
        {
            // Prevents infinite jumps
            jumped = false;
            midJump = true;

            //The actual jump
            if (canJump2)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight);
                canJump2 = false;
            }
        }

        // On jump release
        if (Input.GetKeyUp("space"))
        {
            if (canJump1_)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y / 2);
                canJump1_ = false;
                canJump2 = true;
                canJump2_ = true;
            }
            else if (canJump2_)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y / 2);
                canJump2_ = false;
            }
        }
    }

    // Triggers when the collisions starts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Touching a floor / wall (with cling unlocked) resets the use of jump
        if (collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && GlobalControl.clingUnlocked))
        {
            canJump1 = true;
            canJump1_ = true;
            canJump2 = false;
            canJump2_ = false;

            if(jumped)
            {
                if (direction == "left")
                    direction = "right";
                else
                    direction = "left";
            }

            jumped = false;
            midJump = false;

            if (collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && transform.position.y - rb2D.GetComponent<CircleCollider2D>().radius > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2))
            {
                floored = true;
                walledJump = false;
            }
        }
        // Allows jumping if on wall edge
        else if (collision.collider.CompareTag("Wall") && collision.collider.transform.parent.name != "Destructibles" && transform.position.y - gameObject.GetComponent<CircleCollider2D>().radius > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 - rb2D.GetComponent<CircleCollider2D>().radius * 2)
        {
            canJump1 = true;
            canJump1_ = true;
            canJump2 = false;
            canJump2_ = false;

            jumped = false;
            midJump = false;
        }
        // If the player is almost on the ledge, teleports them on
        else if (collision.collider.CompareTag("Wall") && collision.collider.transform.parent.name != "Destructibles" && collision.collider.transform.parent.name != "Hazards" && collision.collider.transform.parent.name.Substring(0, 4) != "Belt" &&
            transform.position.y < collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 && 
            transform.position.y > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 - gameObject.GetComponent<CircleCollider2D>().radius * 1.75)
        {
            if (direction == "left")
            {
                transform.position = new Vector2(transform.position.x - 0.3f, collision.collider.transform.position.y + collision.collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f);
            }
            else if (direction == "right")
            {
                transform.position = new Vector2(transform.position.x + 0.3f, collision.collider.transform.position.y + collision.collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f);
            }
        }

        if (collision.collider.CompareTag("Wall"))
        {
            walled = true;
            dashing = false;

            // If player is not on a wall, player is still mid air (player y-coord > wall y-coord + wall height / 2)
            if (rb2D.position.y >= collision.collider.gameObject.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2)
            {
                midJump = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Prevents jumping bug when touching floor and wall
        if (collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && GlobalControl.clingUnlocked))
        {
            canJump1 = true;
            canJump1_ = true;
            canJump2 = false;
            canJump2_ = false;

            jumped = false;
            midJump = false;

            if (collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && transform.position.y - rb2D.GetComponent<CircleCollider2D>().radius > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2))
            {
                floored = true;
                walledJump = false;
            }
        }
        // Allows jumping if on wall edge
        else if (collision.collider.CompareTag("Wall") && collision.collider.transform.parent.name != "Destructibles" &&
            transform.position.y - gameObject.GetComponent<CircleCollider2D>().radius > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 - Player.rb2D.GetComponent<CircleCollider2D>().radius)
        {
            canJump1 = true;
            canJump1_ = true;
            canJump2 = false;
            canJump2_ = false;

            jumped = false;
            midJump = true;
        }

        // If the player is almost on the ledge, teleports them on
        else if (collision.collider.CompareTag("Wall") && collision.collider.transform.parent.name != "Destructibles" && collision.collider.transform.parent.name != "Hazards" && collision.collider.transform.parent.name.Substring(0, 4) != "Belt" &&
            transform.position.y < collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 &&
            transform.position.y > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2 - gameObject.GetComponent<CircleCollider2D>().radius * 1.75)
        {
            if (direction == "left")
            {
                transform.position = new Vector2(transform.position.x - 0.3f, collision.collider.transform.position.y + collision.collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f);
            }
            else if (direction == "right")
            {
                transform.position = new Vector2(transform.position.x + 0.3f, collision.collider.transform.position.y + collision.collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.1f);
            }
        }

        if (collision.collider.CompareTag("Wall"))
        {
            walled = true;
            // If player is not on a wall, player is still mid air (player y-coord > wall y-coord + wall height / 2)
            if (rb2D.position.y >= collision.collider.gameObject.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2)
            {
                midJump = false;
            }
        }
    }

    //Triggers when the collision ends
    private void OnCollisionExit2D(Collision2D collision)
    { 
        // Player fell off a floor without jumping, allowing for an air jump if unlocked
        if((collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && GlobalControl.clingUnlocked)) && !jumped)
        {
            canJump1 = false;
            canJump1_ = false;
            canJump2 = true;
            canJump2_ = true;
        }
        else if (collision.collider.CompareTag("Floor") || (collision.collider.CompareTag("Wall") && transform.position.y - rb2D.GetComponent<CircleCollider2D>().radius > collision.collider.transform.position.y + collision.collider.GetComponent<BoxCollider2D>().size.y / 2))
        {
            if (!walled)
            {
                floored = false;
            }
        }

        if (collision.collider.CompareTag("Wall"))
        {
            walled = false;

            canJump1 = false;
            canJump1_ = false;
            canJump2 = true;
            canJump2_ = true;
        }
    }

    // Time between key press and key release, preventing a key hold into a dash
    IEnumerator noHoldDash()
    {
        tooLong = false;

        yield return new WaitForSeconds(0.1f);

        tooLong = true;
    }

    // Time between first release and second press, meaning dashes are quick-presses
    IEnumerator preDash()
    {
        firstPress = false;

        yield return new WaitForSeconds(0.5f);

        firstPress = true;
    }

    // Cooldown for dashing
    IEnumerator postDash()
    {
        yield return new WaitForSeconds(0.5f);

        dashing = false;
        canDash = false;

        yield return new WaitForSeconds(1.5f);

        canDash = true;
    }
}

