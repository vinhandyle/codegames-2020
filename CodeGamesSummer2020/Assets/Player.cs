using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveBy = 2f; // Horizontal velocity
    private float jumpHeight = 5f; // Jump velocity

    private bool canJump1 = false; // Whether the player can jump
    private bool canJump2 = false; // Whether the player can double-jump
    private bool jumped = false; //Whether the first jump started
    private bool jumped2 = false; //Whether the second jump started

    private bool dashUnlocked = true; // Whether dashing is unlocked
    private bool clingUnlocked = true; //Whether clinging to walls is unlocked
    private bool doubleUnlocked = true; //Whether double-jumping is unlocked

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This is gonna take a while...");
    }

    // Update is called once per frame
    void Update()
    {
  
        // Moves player left
        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveBy, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Moves player right
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveBy, GetComponent<Rigidbody2D>().velocity.y);
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
        if (!canJump1 && canJump2 && !jumped2 && doubleUnlocked && Input.GetKeyDown("space"))
        {
            Debug.Log("Jumped again!");
            canJump2 = false;
            jumped2 = true;

            //The actual jump
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
    }

    // Triggers when the collisions starts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Touching a floor / wall (with cling unlocked) resets the use of jump
        if (collision.collider.name.Substring(0, 5) == "Floor" || (collision.collider.name.Substring(0, 4) == "Wall" && clingUnlocked))
        {
            canJump1 = true;
            canJump2 = false;
            jumped = false;
            jumped2 = false;
        }
    }

    //Triggers when the collision ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Player fell off a floor without jumping, allowing for an air jump if unlocked
        if((collision.collider.name.Substring(0, 5) == "Floor" || collision.collider.name.Substring(0, 4) == "Wall") && !jumped)
        {
            canJump1 = false;
            canJump2 = true;
        }
    }
}

