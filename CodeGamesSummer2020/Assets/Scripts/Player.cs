using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Rigidbody2D rb2D;

    public static float moveBy = 2f; // Horizontal velocity
    private float jumpHeight = 5f; // Jump velocity

    public static float x = 0;
    public static float y = 0;
    public static float x2 = 0;
    public static float y2 = 0;

    private bool canDash = true; // Whether the player can dash
    private bool dashing = false; // Prevents moving while dashing
    private bool walled = false; // Player cannot dash when against a wall
    private bool tooLong = false; // Whether a key is held too long to dash on release
    private string direction; // Player direction used for dashing

    // Dashing requires double-pressing quickly, these checks if the next press is going to be the first
    private bool firstPress = true;
    private bool firstLeft = false;
    private bool firstRight = false;

    // Wall Interaction
    private static bool canLeft = true; // Able to move left
    private static bool canRight = true; // Able to move right

    private bool canJump1 = false; // Whether the player can jump
    private bool canJump2 = false; // Whether the player can double-jump
    private bool jumped = false; // Whether the first jump started
    private bool midJump = false; // Whether the player is mid-jump

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set new player position when entering door or moving through an opening 
        if (GlobalControl.switched)
        {
            transform.position = new Vector3(x, y, transform.position.z);
            GlobalControl.switched = false;
        }
        else
        {
            x2 = rb2D.position.x;
            y2 = rb2D.position.y;
        }

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
        else if (walled && rb2D.velocity.y == 0 && midJump && (!canJump1 || !canJump2))
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveBy, GetComponent<Rigidbody2D>().velocity.y);
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveBy, GetComponent<Rigidbody2D>().velocity.y);
            if (firstPress)
            {
                firstRight = true;
            }
            if (firstLeft)
            {
                firstLeft = false;
            }
        }

        // Use Heartless Generator
        if (Input.GetKeyDown("s"))
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
            else if(!GlobalControl.h2e && GlobalControl.healthCurr < GlobalControl.healthMax)
            {
                GlobalControl.energyCurr -= 5;
                GlobalControl.healthCurr++;
            }
        }

        // Toggle Heartless Generator conversion type
        if (Input.GetKeyDown("f"))
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

        // Checks that time between the double presses is under a certain amount of time (no holding into a dash)
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && canDash && GlobalControl.dashUnlocked && !walled)
        {
            if (firstPress)
            {
                StartCoroutine(noHoldDash());
            }
            else
            {
                dashing = true;
                // Checking for first left/right prevents dashing when rapidly alternating between left/right
                if (direction == "left" && firstLeft)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-3 * moveBy, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (direction == "right" && firstRight)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(3 * moveBy, GetComponent<Rigidbody2D>().velocity.y);

                }
                StartCoroutine(postDash());
            }
        }

        // Checks time between first release and second press
        if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && canDash && GlobalControl.dashUnlocked && firstPress && !walled && !tooLong)
        {           
            StartCoroutine(preDash());         
        }

        // Jump if player is grounded or clinging to a wall
        if (canJump1 && Input.GetKeyDown("space"))
        {
            // Prevents jumping again mid-air
            canJump1 = false;
            jumped = true;
            midJump = true;

            // The actual jump
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }

        // Jumping without landing allows for a second jump if unlocked
        if (!canJump1 && jumped)
        {
            canJump2 = true;
        }

        // Double jump
        if (!canJump1 && canJump2 && GlobalControl.doubleUnlocked && Input.GetKeyDown("space"))
        {
            // Prevents infinite jumps
            canJump2 = false;
            jumped = false;
            midJump = true;

            //The actual jump
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
    }

    // Triggers when the collisions starts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Touching a floor / wall (with cling unlocked) resets the use of jump
        if (collision.collider.tag == "Floor" || (collision.collider.tag == "Wall" && GlobalControl.clingUnlocked))
        {
            canJump1 = true;
            canJump2 = false;
            jumped = false;
        }

        if (collision.collider.tag == "Wall")
            walled = true;
        {
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
        if (collision.collider.tag == "Floor" || (collision.collider.tag == "Wall" && GlobalControl.clingUnlocked))
        {
            canJump1 = true;
            canJump2 = false;
            jumped = false;

        }

        if (collision.collider.tag == "Wall")
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
        if((collision.collider.tag == "Floor" || (collision.collider.tag == "Wall" && GlobalControl.clingUnlocked)) && !jumped)
        {
            canJump1 = false;
            canJump2 = true;
        }

        if (collision.collider.tag == "Wall")
        {
            walled = false;
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

