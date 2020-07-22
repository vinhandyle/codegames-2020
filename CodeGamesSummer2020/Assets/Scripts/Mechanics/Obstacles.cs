using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Non-static variables are separate of each other when applied to different objects (i.e. multiple enemies using this script)
    public int healthMax;
    public int healthCurr;
    public int damage;
    public bool hazard; // Enemy, Boss, or Hazard

    // Start is called before the first frame update
    void Start()
    {
        if ((gameObject.name == "Patrol_1_0_0" && !GlobalControl.patrol_1_0_0) ||
            (gameObject.name == "Patrol_1_0_1" && !GlobalControl.patrol_1_0_1) ||
            (gameObject.name == "Patrol_1_0_2" && !GlobalControl.patrol_1_0_2) ||
            (gameObject.name == "Patrol_1_1_0" && !GlobalControl.patrol_1_1_0) ||
            (gameObject.name == "Errat_0" && !GlobalControl.errat_0) ||
            (gameObject.name == "Errat_1" && !GlobalControl.errat_1) ||
            (gameObject.name == "Errat_2" && !GlobalControl.errat_2) ||
            (gameObject.name == "Errat_3" && !GlobalControl.errat_3) ||
            (gameObject.name == "Errat_4" && !GlobalControl.errat_4) ||
            (gameObject.name == "Errat_5" && !GlobalControl.errat_5))
        {
            gameObject.SetActive(false);
        }

        // Initialize enemy health and damage

        /*---------------Enemies----------------*/

        // Patrol Machina
        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: IT (1st), Sunset Garden
                healthMax = 4;
                damage = 1;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Twilight Town, Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_3")
            { // Tier 3: IT (2nd), Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Pursuit Machina
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            if (gameObject.name.Substring(7, 2) == "_1")
            { // Tier 1: Sunset Garden
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(7, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aerial Machina
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: Twilight Town
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aquatic Machina
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            if (gameObject.name.Substring(7, 2) == "_1")
            { // Tier 1: Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(7, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Turret Machina
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: IT (2nd)
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage =;
            }
        }

        // Errat
        else if (gameObject.name.Substring(0, 5) == "Errat")
        { // Dreg Heap
            healthMax = 1;
            damage = 0;
        }

        /*---------------Hazards----------------*/

        // Toxic Sludge
        else if (gameObject.name.Substring(0, 6) == "Sludge")
        { // Dreg Heap
            damage = 3;
        }


        /*---------------Bosses----------------*/


        if (!hazard)
        {
            healthCurr = healthMax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurr <= 0 && !hazard)
        { // Set enemy inactive when hp = 0
            gameObject.SetActive(false);

            // Add type to catalog on kill
            if (gameObject.name.Substring(0, 6) == "Patrol")
            {
                GlobalControl.downed_patrol = true;
            }
            else if (gameObject.name.Substring(0, 7) == "Pursuit")
            {
                GlobalControl.downed_pursuit = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Aerial")
            {
                GlobalControl.downed_aerial = true;
            }
            else if (gameObject.name.Substring(0, 7) == "Aquatic")
            {
                GlobalControl.downed_aquatic = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Turret")
            {
                GlobalControl.downed_turret = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Errat")
            {
                GlobalControl.found_errat = true;
            }

            // Prevent respawn on scene switch

            // Testing Area
            if (gameObject.name == "Patrol_1_0_0")
            {
                GlobalControl.patrol_1_0_0 = false;
            }
            else if (gameObject.name == "Patrol_1_0_1")
            {
                GlobalControl.patrol_1_0_1 = false;
            }
            else if (gameObject.name == "Patrol_1_0_2")
            {
                GlobalControl.patrol_1_0_2 = false;
            }
            else if (gameObject.name == "Patrol_1_1_1")
            {
                GlobalControl.patrol_1_1_0 = false;
            }

            // Dreg Heap
            else if (gameObject.name.Substring(0, 5) == "Errat")
            {
                if (gameObject.name == "Errat_0")
                {
                    GlobalControl.errat_0 = false;
                }
                else if (gameObject.name == "Errat_1")
                {
                    GlobalControl.errat_1 = false;
                }
                else if (gameObject.name == "Errat_2")
                {
                    GlobalControl.errat_2 = false;
                }
                else if (gameObject.name == "Errat_3")
                {
                    GlobalControl.errat_3 = false;
                }
                else if (gameObject.name == "Errat_4")
                {
                    GlobalControl.errat_4 = false;
                }
                else if (gameObject.name == "Errat_5")
                {
                    GlobalControl.errat_5 = false;
                }
                GlobalControl.humansLeft--;
            }
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
            if (gameObject.CompareTag("Enemy"))
            {
                healthCurr -= GlobalControl.damage;
                Debug.Log(healthCurr + " HP remaining!");
            }
            else if (gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial")
            {
                healthCurr = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat") && other.gameObject.CompareTag("Player") && !GlobalControl.immune)
        {
            if (GlobalControl.reactor == "unstable")
            {
                GlobalControl.healthCurr = 0;
            }
            else
            {
                GlobalControl.healthCurr -= damage;
                StartCoroutine(IFrame());
            }
        }
    }

    IEnumerator IFrame()
    {
        GlobalControl.immune = true;

        yield return new WaitForSeconds(1f);

        GlobalControl.immune = false;
    }
}
