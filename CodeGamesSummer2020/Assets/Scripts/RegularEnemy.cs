using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : MonoBehaviour
{
    public static int healthMax;
    public static int healthCurr;
    public static int damage;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize enemy health and damage

        // Patrol Machina
        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            if (gameObject.name.Substring(6, 8) == "_1")
            { // Tier 1: IT (1st), Sunset Garden
                healthMax = 4;
                damage = 1;
            }
            else if (gameObject.name.Substring(6, 8) == "_2")
            { // Tier 2: Twilight Town, Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 8) == "_3")
            { // Tier 3: IT (2nd), Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Pursuit Machina
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            if (gameObject.name.Substring(7, 9) == "_1")
            { // Tier 1: Sunset Garden
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(7, 9) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aerial Machina
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            if (gameObject.name.Substring(6, 8) == "_1")
            { // Tier 1: Twilight Town
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 8) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aquatic Machina
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            if (gameObject.name.Substring(7, 9) == "_1")
            { // Tier 1: Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(7, 9) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Turret Machina
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name.Substring(6, 8) == "_1")
            { // Tier 1: IT (2nd)
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 8) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage =;
            }
        }

        healthCurr = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurr <= 0)
        { // Set enemy inactive when hp = 0
            gameObject.SetActive(false);
        }

        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            // Insert AI here
        }
    }

    // Trigger contact effects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
            healthCurr -= GlobalControl.damage;
            Debug.Log("Hit, " + healthCurr + " hp remaining!");
        }
    }
}
