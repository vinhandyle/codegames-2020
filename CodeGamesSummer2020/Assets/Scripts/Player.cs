﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveBy = 2f; // Horizontal velocity
    private float jumpHeight = 5f; // Jump velocity

    public static float x = 0;
    public static float y = 0;

    private bool canDash = true; // Whether the player can dash
    private bool dashing = false; // Prevents moving while dashing
    private bool walled = false; // Player cannot dash when against a wall
    private bool tooLong = false; // Whether a key is held too long to dash on release
    private string direction; // Player direction used for dashing

    // Dashing requires double-pressing quickly, these checks if the next press is going to be the first
    private bool firstPress = true;
    private bool firstLeft = false;
    private bool firstRight = false;


    private bool canJump1 = false; // Whether the player can jump
    private bool canJump2 = false; // Whether the player can double-jump
    private bool jumped = false; //Whether the first jump started

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Set new player position when entering door or moving through an opening 
        if (GlobalControl.switched)
        {
            transform.position = new Vector2(x, y);
            GlobalControl.switched = false;
        }

        // Moves player left
        if (Input.GetKey("a") && !dashing)
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
        if (Input.GetKey("d") && !dashing)
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
            else if(!GlobalControl.h2e && GlobalControl.energyCurr >= 5)
            {
                GlobalControl.energyCurr -= 5;
                if (GlobalControl.healthCurr < GlobalControl.healthMax)
                {
                    GlobalControl.healthCurr++;
                }
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
        {
            walled = true;
        }
    }
    
    //Triggers when the collision ends
    private void OnCollisionExit2D(Collision2D collision)
    { 
        // Player fell off a floor without jumping, allowing for an air jump if unlocked
        if((collision.collider.tag == "Floor" || collision.collider.tag == "Wall") && !jumped)
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

