﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Non-static variables are separate of each other when applied to different objects (i.e. multiple enemies using this script)
    public int healthMax;
    public int healthCurr;
    public int defense;
    public int damage;
    public bool hazard;
    public bool passive;
    public bool animated;
    public string attachedTo;
    public List<Sprite> sprites;

    /*-----Variables used in AI-----*/
    // Starting Position
    public float x;
    public float y;

    // Movement
    public float range;     // Range centered, equal sides
    public float range_1;   // Different range lengths
    public float range_2;
    public float range_3;
    public float speed;
    public float speed_1;

    public Vector3 refDifference;
    public float refDistance;
    public Vector2 refDirection = new Vector2();

    // Time
    public int time;    // AI time
    public int time_1;
    public int deAggroTime;
    public bool hold_time = false; // hold time
    public bool hold_time_1 = false;

    // Projectile
    public List<float> bulletSpeed;
    public float baseUseTime;
    public float useTime;
    public float rechargeTime;
    public int maxBullet;
    public int currBullet;
    public bool canShoot = true;

    // Random
    public float randNum;
    public bool hold_rand = false;

    // States -> refState,type,variant(if applicable)_num
    // type: Obstacles to Scope (1), Scope to Obstacles (2), Obstacles to Obstacles (3)
    // variant: multiple of same type in same num, use a->z
    // num: Pursuit (1), Aerial (2), Aquatic (3), Turret (4), Overseer (5), Containment (6), Subnautical (7), Emperor (8)
    // Add a "H" before num for hazard AI: Crusher (1),
    // e.g. refState2b_5
    public string aiState;
    public string path;
    public string pathState;

    /* Pursuit */
    public static string refState_1;            // Current aiState
    public static string refState2_1;           // Should it be passive?
    public static string refState2a_1;          // Should it stop?
    public string refState3_1;                  // Where was it facing before it stopped?

    /* Aerial */
    public static string refState2_2;           // Is player in range?

    /* Aquatic */
    public static string refState2_3;           // Is player in range?
    public bool refState3_3 = false;            // Risen above water?

    /* Overseer */
    public static string refState_5;            // Scorched Earth attack phase
    public static string refState1a_5;          // Charge Beam attack phase
    public static string refState1b_5;          // Center segment position
    public static string refState2_5;           // Is Scorched Earth Active?
    public static string refState2a_5;          // Is Charge Beam active?
    public string refState3_5;                  // Is Gear Shift active?
    public string refState3a_5;                 // Current boss phase
    public string refState3b_5;                 // Which ObjectPooler?

    /* Containment */
    public static string refState_6;            // Warning for Crash
    public static string refState2_6 = "far";   // Is the player too far?
    public static string refState2a_6;          // Touching outer box?
    public string refState3_6;                  // Current boss phase
    public string refState3a_6;                 // Previous aiState
    public string refState3b_6;                 // Crashing?
    public int refState3c_6 = 0;                // Berserk Crash #
    public string refState3d_6;                 // Berserk Stage
    public bool refState3e_6 = true;            // Berserk Indicator
    public bool refState3f_6 = false;           // Blinked?

    /* Subnautical */
    public static bool refState_7;              // Torpedo active?
    public static float refState1a_7 = 0.01f;   // Torpedo speed
    public static string refState1b_7;          // Downpouring?
    public string refState3_7 = "under";        // Above or below water?
    public bool refState3a_7;                   // Phase 2?
    public string refState3b_7;                 // Leap State
    public string refState3c_7;                 // Crystal Barrage State

    // Start is called before the first frame update
    void Start()
    {
        // Retain alive.dead status when reloading scene
        if ((gameObject.name == "Patrol_1_0_0" && !GlobalControl.patrol_1_0_0) ||
            (gameObject.name == "Patrol_1_0_1" && !GlobalControl.patrol_1_0_1) ||
            (gameObject.name == "Patrol_1_0_2" && !GlobalControl.patrol_1_0_2) ||
            (gameObject.name == "Patrol_1_1_0" && !GlobalControl.patrol_1_1_0) ||
            (gameObject.name == "Patrol_1_2_0" && !GlobalControl.patrol_1_2_0) ||
            (gameObject.name == "Patrol_1_2_1" && !GlobalControl.patrol_1_2_1) ||
            (gameObject.name == "Patrol_1_2_2" && !GlobalControl.patrol_1_2_2) ||
            (gameObject.name == "Patrol_1_2_3" && !GlobalControl.patrol_1_2_3) ||
            (gameObject.name == "Patrol_1_2_4" && !GlobalControl.patrol_1_2_4) ||
            (gameObject.name == "Patrol_1_2_5" && !GlobalControl.patrol_1_2_5) ||
            (gameObject.name == "Patrol_1_2_6" && !GlobalControl.patrol_1_2_6) ||
            (gameObject.name == "Patrol_1_2_7" && !GlobalControl.patrol_1_2_7) ||
            (gameObject.name == "Patrol_1_2_8" && !GlobalControl.patrol_1_2_8) ||
            (gameObject.name == "Patrol_1_2_9" && !GlobalControl.patrol_1_2_9) ||
            (gameObject.name == "Patrol_2_3_0" && !GlobalControl.patrol_2_3_0) ||
            (gameObject.name == "Patrol_2_3_1" && !GlobalControl.patrol_2_3_1) ||
            (gameObject.name == "Patrol_2_3_2" && !GlobalControl.patrol_2_3_2) ||
            (gameObject.name == "Patrol_2_3_3" && !GlobalControl.patrol_2_3_3) ||
            (gameObject.name == "Patrol_2_3_4" && !GlobalControl.patrol_2_3_4) ||
            (gameObject.name == "Patrol_2_3_5" && !GlobalControl.patrol_2_3_5) ||
            (gameObject.name == "Patrol_2_3_6" && !GlobalControl.patrol_2_3_6) ||
            (gameObject.name == "Patrol_2_3_7" && !GlobalControl.patrol_2_3_7) ||
            (gameObject.name == "Patrol_2_3_8" && !GlobalControl.patrol_2_3_8) ||
            (gameObject.name == "Patrol_2_3_9" && !GlobalControl.patrol_2_3_9) ||
            (gameObject.name == "Patrol_2_3_10" && !GlobalControl.patrol_2_3_10) ||
            (gameObject.name == "Patrol_2_4_0" && !GlobalControl.patrol_2_4_0) ||
            (gameObject.name == "Pursuit_1_2_0" && !GlobalControl.pursuit_1_2_0) ||
            (gameObject.name == "Pursuit_1_2_1" && !GlobalControl.pursuit_1_2_1) ||
            (gameObject.name == "Pursuit_1_2_2" && !GlobalControl.pursuit_1_2_2) ||
            (gameObject.name == "Pursuit_1_2_3" && !GlobalControl.pursuit_1_2_3) ||
            (gameObject.name == "Pursuit_1_2_4" && !GlobalControl.pursuit_1_2_4) ||
            (gameObject.name == "Pursuit_1_2_5" && !GlobalControl.pursuit_1_2_5) ||
            (gameObject.name == "Pursuit_1_2_6" && !GlobalControl.pursuit_1_2_6) ||
            (gameObject.name == "Pursuit_1_2_7" && !GlobalControl.pursuit_1_2_7) ||
            (gameObject.name == "Aerial_1_3_0" && !GlobalControl.aerial_1_3_0) ||
            (gameObject.name == "Aerial_1_3_1" && !GlobalControl.aerial_1_3_1) ||
            (gameObject.name == "Aerial_1_3_2" && !GlobalControl.aerial_1_3_2) ||
            (gameObject.name == "Aerial_1_3_3" && !GlobalControl.aerial_1_3_3) ||
            (gameObject.name == "Aerial_1_3_4" && !GlobalControl.aerial_1_3_4) ||
            (gameObject.name == "Aerial_1_3_5" && !GlobalControl.aerial_1_3_5) ||
            (gameObject.name == "Aerial_1_3_6" && !GlobalControl.aerial_1_3_6) ||
            (gameObject.name == "Aerial_1_3_7" && !GlobalControl.aerial_1_3_7) ||
            (gameObject.name == "Aerial_1_3_8" && !GlobalControl.aerial_1_3_8) ||
            (gameObject.name == "Aquatic_1_4_0" && !GlobalControl.aquatic_1_4_0) ||
            (gameObject.name == "Errat_0" && !GlobalControl.errat_0) ||
            (gameObject.name == "Errat_1" && !GlobalControl.errat_1) ||
            (gameObject.name == "Errat_2" && !GlobalControl.errat_2) ||
            (gameObject.name == "Errat_3" && !GlobalControl.errat_3) ||
            (gameObject.name == "Errat_4" && !GlobalControl.errat_4) ||
            (gameObject.name == "Errat_5" && !GlobalControl.errat_5) ||
            (gameObject.name == "Overseer" && GlobalControl.downed_boss_1) ||
            (gameObject.name == "Containment" && GlobalControl.downed_boss_2))
        {
            gameObject.SetActive(false);
        }

        // Set starting position
        x = transform.position.x;
        y = transform.position.y;

        // If asymmetrical range is not set, set symmetrical range
        if (range_1 + range_2 == 0)
        {
            range_1 = range;
            range_2 = range;
        }

        // Load gun
        currBullet = maxBullet;
        useTime = baseUseTime;

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
                healthMax = 8;
                damage = 2;
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
                healthMax = 3;
                damage = 2;
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
                healthMax = 6;
                damage = 2;
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
                healthMax = 10;
                damage = 2;
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
            damage = 2;
        }
        else if (gameObject.name.Substring(0, 6) == "Molten")
        {
            damage = 5;
        }
        else if (gameObject.name.Substring(0, 7) == "OM_Beam")
        {
            damage = 8;
        }


        /*---------------Bosses----------------*/
        else if (gameObject.name == "Overseer")
        {
            healthMax = 50;
            damage = 2;
        }
        else if (gameObject.name == "Containment")
        {
            healthMax = 180;
            damage = 5;
        }
        else if (gameObject.name == "Subnautical")
        {
            healthMax = 200;
            damage = 4;
        }

        // Hazards immune to damage
        if (!hazard)
        {
            healthCurr = healthMax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // On kill
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
            else if (gameObject.name == "Overseer")
            {
                GlobalControl.downed_boss_1 = true;
            }
            else if (gameObject.name == "Containment")
            {
                GlobalControl.downed_boss_2 = true;
            }
            else if (gameObject.name == "Subnautical")
            {
                GlobalControl.downed_boss_3 = true;
            }
            else if (gameObject.name == "Emperor")
            {
                GlobalControl.downed_boss_4 = true;
            }

            /*-----Prevent respawn on scene switch-----*/

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

            // Sunset Garden
            else if (gameObject.name == "Patrol_1_2_0")
            {
                GlobalControl.patrol_1_2_0 = false;
            }
            else if (gameObject.name == "Patrol_1_2_1")
            {
                GlobalControl.patrol_1_2_1 = false;
            }
            else if (gameObject.name == "Patrol_1_2_2")
            {
                GlobalControl.patrol_1_2_2 = false;
            }
            else if (gameObject.name == "Patrol_1_2_3")
            {
                GlobalControl.patrol_1_2_3 = false;
            }
            else if (gameObject.name == "Patrol_1_2_4")
            {
                GlobalControl.patrol_1_2_4 = false;
            }
            else if (gameObject.name == "Patrol_1_2_5")
            {
                GlobalControl.patrol_1_2_5 = false;
            }
            else if (gameObject.name == "Patrol_1_2_6")
            {
                GlobalControl.patrol_1_2_6 = false;
            }
            else if (gameObject.name == "Patrol_1_2_7")
            {
                GlobalControl.patrol_1_2_7 = false;
            }
            else if (gameObject.name == "Patrol_1_2_8")
            {
                GlobalControl.patrol_1_2_8 = false;
            }
            else if (gameObject.name == "Patrol_1_2_9")
            {
                GlobalControl.patrol_1_2_9 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_0")
            {
                GlobalControl.pursuit_1_2_0 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_1")
            {
                GlobalControl.pursuit_1_2_1 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_2")
            {
                GlobalControl.pursuit_1_2_2 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_3")
            {
                GlobalControl.pursuit_1_2_3 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_4")
            {
                GlobalControl.pursuit_1_2_4 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_5")
            {
                GlobalControl.pursuit_1_2_5 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_6")
            {
                GlobalControl.pursuit_1_2_6 = false;
            }
            else if (gameObject.name == "Pursuit_1_2_7")
            {
                GlobalControl.pursuit_1_2_7 = false;
            }
            else if (gameObject.name == "Overseer")
            {
                GlobalControl.downed_boss_1 = true;
            }

            // Twilight Town
            else if (gameObject.name == "Patrol_2_3_0")
            {
                GlobalControl.patrol_2_3_0 = false;
            }
            else if (gameObject.name == "Patrol_2_3_1")
            {
                GlobalControl.patrol_2_3_1 = false;
            }
            else if (gameObject.name == "Patrol_2_3_2")
            {
                GlobalControl.patrol_2_3_2 = false;
            }
            else if (gameObject.name == "Patrol_2_3_3")
            {
                GlobalControl.patrol_2_3_3 = false;
            }
            else if (gameObject.name == "Patrol_2_3_4")
            {
                GlobalControl.patrol_2_3_4 = false;
            }
            else if (gameObject.name == "Patrol_2_3_5")
            {
                GlobalControl.patrol_2_3_5 = false;
            }
            else if (gameObject.name == "Patrol_2_3_6")
            {
                GlobalControl.patrol_2_3_6 = false;
            }
            else if (gameObject.name == "Patrol_2_3_7")
            {
                GlobalControl.patrol_2_3_7 = false;
            }
            else if (gameObject.name == "Patrol_2_3_8")
            {
                GlobalControl.patrol_2_3_8 = false;
            }
            else if (gameObject.name == "Patrol_2_3_9")
            {
                GlobalControl.patrol_2_3_9 = false;
            }
            else if (gameObject.name == "Patrol_2_3_10")
            {
                GlobalControl.patrol_2_3_10 = false;
            }
            else if (gameObject.name == "Aerial_1_3_0")
            {
                GlobalControl.aerial_1_3_0 = false;
            }
            else if (gameObject.name == "Aerial_1_3_1")
            {
                GlobalControl.aerial_1_3_1 = false;
            }
            else if (gameObject.name == "Aerial_1_3_2")
            {
                GlobalControl.aerial_1_3_2 = false;
            }
            else if (gameObject.name == "Aerial_1_3_3")
            {
                GlobalControl.aerial_1_3_3 = false;
            }
            else if (gameObject.name == "Aerial_1_3_4")
            {
                GlobalControl.aerial_1_3_4 = false;
            }
            else if (gameObject.name == "Aerial_1_3_5")
            {
                GlobalControl.aerial_1_3_5 = false;
            }
            else if (gameObject.name == "Aerial_1_3_6")
            {
                GlobalControl.aerial_1_3_6 = false;
            }
            else if (gameObject.name == "Aerial_1_3_7")
            {
                GlobalControl.aerial_1_3_7 = false;
            }
            else if (gameObject.name == "Aerial_1_3_8")
            {
                GlobalControl.aerial_1_3_8 = false;
            }
            else if (gameObject.name == "Containment")
            {
                GlobalControl.downed_boss_2 = true;
            }

            // Midnight Bay
            else if (gameObject.name == "Patrol_2_4_0")
            {
                GlobalControl.patrol_2_4_0 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_0")
            {
                GlobalControl.aquatic_1_4_0 = false;
            }
        }

        /*----------Enemy AI----------*/

        /*-----Patrol Machina-----*/
        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            // Moving left
            if (aiState == "moveLeft")
            {
                if (transform.position.x > x - range_1)
                {
                    transform.position += new Vector3(-speed, 0, 0);
                }
                else
                {
                    aiState = "moveRight";
                }
            }
            // Moving right
            else if (aiState == "moveRight")
            {
                if (transform.position.x < x + range_2)
                {
                    transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    aiState = "moveLeft";
                }
            }
        }

        /*-----Pursuit Machina-----*/
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            /*---Scout Mode---*/
            if (aiState == "passive_right")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];

                // Detect player when facing towards and at the same level
                if (Player.rb2D.position.y - Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius > transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.x > transform.position.x && (refState2_1 != "passive" && refState2_1 != null))
                {
                    aiState = "hostile_right";
                }

                // Move Right
                if (transform.position.x < x + range_1)
                {
                    transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    aiState = "passive_left";
                }
            }
            else if (aiState == "passive_left")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];

                // Detect player when facing towards and at the same level
                if (Player.rb2D.position.y - Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius > transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.x < transform.position.x && (refState2_1 != "passive" && refState2_1 != null))
                {
                    aiState = "hostile_left";
                }

                // Move Left
                if (transform.position.x > x - range_2)
                {
                    transform.position += new Vector3(-speed, 0, 0);
                }
                else
                {
                    aiState = "passive_right";
                }
            }

            /*---Chase Mode---*/
            else if (aiState == "hostile_right")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];

                if (Player.rb2D.position.x + Player.rb2D.GetComponent<CircleCollider2D>().radius * 2 < transform.position.x - GetComponent<BoxCollider2D>().size.x / 2)
                {
                    aiState = "hostile_left";
                }
                else if (refState2a_1 == "stop")
                {
                    refState3_1 = aiState;
                    aiState = "stop";
                }
                else
                {
                    transform.position += new Vector3(2 * speed, 0, 0);
                }
            }
            else if (aiState == "hostile_left")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];

                if (Player.rb2D.position.x - Player.rb2D.GetComponent<CircleCollider2D>().radius * 2 > transform.position.x + GetComponent<BoxCollider2D>().size.x / 2)
                {
                    aiState = "hostile_right";
                }
                else if (refState2a_1 == "stop")
                {
                    refState3_1 = aiState;
                    aiState = "stop";
                }
                else
                {
                    transform.position += new Vector3(-2 * speed, 0, 0);
                }
            }

            /*-----Stop and Deaggro-----*/
            else if (aiState == "stop")
            {
                if (!hold_time)
                    StartCoroutine(addSecond());

                if (time > deAggroTime)
                {
                    if (refState3_1 == "hostile_left")
                    {
                        aiState = "passive_right";
                    }
                    else if (refState3_1 == "hostile_right")
                    {
                        aiState = "passive_left";
                    }
                    time = 0;
                    refState2a_1 = "";
                }
                else if (Player.rb2D.position.x > transform.position.x && refState3_1 == "hostile_left")
                {
                    aiState = "hostile_right";
                    time = 0;
                    refState2a_1 = "";
                }
                else if (Player.rb2D.position.x < transform.position.x && refState3_1 == "hostile_right")
                {
                    aiState = "hostile_left";
                    time = 0;
                    refState2a_1 = "";
                }
            }

            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2 && aiState.Substring(0, 7) == "hostile")
            {
                if (!hold_time)
                    StartCoroutine(addSecond());
            }
            else
            {
                time = 0;
            }

            if (time / 2 >= deAggroTime)
            {
                if (aiState == "hostile_right")
                {
                    aiState = "passive_right";
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                else if (aiState == "hostile_left")
                {
                    aiState = "passive_left";
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }

                time = 0;
            }
        }

        /*-----Aerial Machina-----*/
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            // Change AI if player is in or out of range
            if (gameObject.name == Scope.signal)
            {
                if (refState2_2 == "in")
                {
                    aiState = "shoot";
                }
                else
                {
                    aiState = "";
                }
            }

            if (aiState == "shoot")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                // Shoot x bullets at y rate then recharge for z seconds
                if (canShoot && currBullet > 0)
                {
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    fireBullet(direction, rotationZ, bulletSpeed[0]);
                    currBullet--;
                    StartCoroutine(cooldown());
                }
                else if (currBullet == 0 && !hold_time)
                {
                    StartCoroutine(addSecond());
                    if (time >= rechargeTime)
                    {
                        currBullet = maxBullet;
                        time = 0;
                    }
                }
            }

            // Determine pathing
            if (path.Substring(0, 4) == "line")
            {
                if (path == "line_h")
                {
                    if (pathState == "left")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0);
                        }
                        else
                        {
                            pathState = "right";
                        }
                    }
                    else if (pathState == "right")
                    {
                        if (transform.position.x < x + range_2)
                        {
                            transform.position += new Vector3(speed, 0);
                        }
                        else
                        {
                            pathState = "left";
                        }
                    }
                }
                else if (path == "line_v")
                {
                    if (pathState == "down")
                    {
                        if (transform.position.y > y - range_1)
                        {
                            transform.position += new Vector3(0, -speed);
                        }
                        else
                        {
                            pathState = "up";
                        }
                    }
                    else if (pathState == "up")
                    {
                        if (transform.position.y < y + range_2)
                        {
                            transform.position += new Vector3(0, speed);
                        }
                        else
                        {
                            pathState = "down";
                        }
                    }
                }
                else if (path == "line_d")
                {
                    if (pathState == "left-down")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, -speed_1);
                        }
                        else
                        {
                            pathState = "right-up";
                        }
                    }
                    else if (pathState == "right-up")
                    {
                        if (transform.position.x < x + range_2)
                        {
                            transform.position += new Vector3(speed, speed_1);
                        }
                        else
                        {
                            pathState = "left-down";
                        }
                    }
                    else if (pathState == "left-up")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, speed_1);
                        }
                        else
                        {
                            pathState = "right-down";
                        }
                    }
                    else if (pathState == "right-down")
                    {
                        if (transform.position.x < x + range_2)
                        {
                            transform.position += new Vector3(speed, -speed_1);
                        }
                        else
                        {
                            pathState = "left-up";
                        }
                    }
                }
            }
            else if (path.Substring(0, 3) == "box")
            { // Start from box center and spiral into intended path
                if (path == "box-clock")
                {
                    if (pathState == "left")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0);
                        }
                        else
                        {
                            pathState = "up";
                        }
                    }
                    else if (pathState == "right")
                    {
                        if (transform.position.x < x + range_1)
                        {
                            transform.position += new Vector3(speed, 0);
                        }
                        else
                        {
                            pathState = "down";
                        }
                    }
                    else if (pathState == "down")
                    {
                        if (transform.position.y > y - range_2)
                        {
                            transform.position += new Vector3(0, -speed);
                        }
                        else
                        {
                            pathState = "left";
                        }
                    }
                    else if (pathState == "up")
                    {
                        if (transform.position.y < y + range_2)
                        {
                            transform.position += new Vector3(0, speed);
                        }
                        else
                        {
                            pathState = "right";
                        }
                    }
                }
                else if (path == "box-counter")
                {
                    if (pathState == "left")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0);
                        }
                        else
                        {
                            pathState = "down";
                        }
                    }
                    else if (pathState == "right")
                    {
                        if (transform.position.x < x + range_1)
                        {
                            transform.position += new Vector3(speed, 0);
                        }
                        else
                        {
                            pathState = "up";
                        }
                    }
                    else if (pathState == "down")
                    {
                        if (transform.position.y > y - range_2)
                        {
                            transform.position += new Vector3(0, -speed);
                        }
                        else
                        {
                            pathState = "right";
                        }
                    }
                    else if (pathState == "up")
                    {
                        if (transform.position.y < y + range_2)
                        {
                            transform.position += new Vector3(0, speed);
                        }
                        else
                        {
                            pathState = "left";
                        }
                    }
                }
            }
        }

        /*-----Aquatic Machina-----*/
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            // Switch between wander and attack
            if (Scope.signal == gameObject.name)
            {
                if (refState2_3 == "in" && (Player.rb2D.position.x + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.x - GetComponent<BoxCollider2D>().size.x * 2 || Player.rb2D.position.x - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.x + GetComponent<BoxCollider2D>().size.x * 2))
                {
                    aiState = "attack";
                    defense = 0;
                }
                else
                {
                    aiState = "";
                    defense = 100;
                }
            }

            // Wander
            if (aiState != "attack")
            {
                refState3_3 = false;
                GetComponent<SpriteRenderer>().sprite = sprites[0];

                if (transform.position.y > y)
                {
                    transform.position += new Vector3(0, -speed_1);
                }
                else if (transform.position.y < y)
                {
                    transform.position += new Vector3(0, speed_1);
                }
                else
                {
                    if (pathState == "left")
                    {
                        if (transform.position.x > x - range_1)
                        {
                            transform.position += new Vector3(-speed, 0);
                        }
                        else
                        {
                            pathState = "right";
                            GetComponent<SpriteRenderer>().flipX = true;
                        }
                    }
                    else if (pathState == "right")
                    {
                        if (transform.position.x < x + range_2)
                        {
                            transform.position += new Vector3(speed, 0);
                        }
                        else
                        {
                            pathState = "left";
                            GetComponent<SpriteRenderer>().flipX = false;
                        }
                    }
                }
            }

            // Attack
            else if (aiState == "attack")
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];

                // Repositioning
                if (Player.rb2D.position.x < transform.position.x)
                {
                    pathState = "left";
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    pathState = "right";
                    GetComponent<SpriteRenderer>().flipX = true;
                }

                // Rise above surface
                if (!refState3_3)
                {
                    if (Player.rb2D.position.y > transform.position.y)
                    {
                        if (transform.position.y < y + range_3)
                        {
                            transform.position += new Vector3(0, speed_1);
                        }
                        else
                        {
                            refState3_3 = true;
                        }
                    }
                    else
                    {
                        if (transform.position.y > y - range_3)
                        {
                            transform.position += new Vector3(0, -speed_1);
                        }
                        else
                        {
                            refState3_3 = true;
                        }
                    }
                }

                // Triple Shot
                else if (refState3_3)
                {
                    if (canShoot && (Player.rb2D.position.x + Player.rb2D.GetComponent<CircleCollider2D>().radius <= transform.position.x - GetComponent<BoxCollider2D>().size.x * 2 || Player.rb2D.position.x - Player.rb2D.GetComponent<CircleCollider2D>().radius >= transform.position.x + GetComponent<BoxCollider2D>().size.x * 2))
                    {
                        Vector3 difference = new Vector3();
                        if (pathState == "left")
                        {
                            difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x * 2, transform.position.y, transform.position.z);
                        }
                        else if (pathState == "right")
                        {
                            difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x + GetComponent<BoxCollider2D>().size.x * 2, transform.position.y, transform.position.z);
                        }

                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                        float distance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();

                        for (int i = 0; i < 3; i++)
                        {
                            if (direction.x * direction.y >= 0)                            
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);                            
                            else if(direction.x > direction.y)                           
                                fireBullet(new Vector2(Mathf.Sqrt(1 - Mathf.Pow(Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1)), 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                            else
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sqrt(1 - Mathf.Pow(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), 2))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                        }
                        StartCoroutine(cooldown());
                    }
                }
            }
        }

        /*-----Turret Machina-----*/
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            // Insert AI here
        }

        /*----------Hazard AI----------*/

        /*-----Crusher-----*/
        else if (gameObject.name.Substring(0, 7) == "Crusher")
        {
            if (aiState == "moveUp")
            {
                if (transform.position.y + GetComponent<BoxCollider2D>().size.y / 2 < y + range_1)
                {
                    transform.position += new Vector3(0, speed, 0);
                }
                else
                {
                    aiState = "moveDown";
                }
            }
            else if (aiState == "moveDown")
            {
                if (transform.position.y - GetComponent<BoxCollider2D>().size.y / 2 > y - range_2)
                {
                    transform.position += new Vector3(0, -speed, 0);
                }
                else
                {
                    aiState = "moveUp";
                }
            }
        }

        /*----------Boss AI----------*/

        /*-----Overseer Machina-----*/
        else if (gameObject.name == "Overseer")
        {
            // Timer for AI
            if (aiState != "top" && aiState != "bottom" && !hold_time)
            {
                StartCoroutine(addSecond());
            }

            // Gear Shift
            if (time > 0 && refState3_5 != "shifted" && ((time % 10 == 0 && refState3a_5 != "phase 2") || (time % 5 == 0 && refState3a_5 == "phase 2")) && (refState2a_5 == "" || refState2a_5 == null) && aiState != "ramping")
            {
                if (transform.position.y <= -1.94)
                {
                    aiState = "top";
                }
                else if (transform.position.y >= 2.06)
                {
                    aiState = "bottom";
                }
                refState3_5 = "shifted";
            }

            if (aiState == "top")
            {
                if (transform.position.y < 2.06)
                {
                    transform.position += new Vector3(0, speed, 0);
                }
                else
                {
                    aiState = "rest";
                    refState3_5 = "";
                }
            }
            else if (aiState == "bottom")
            {
                if (transform.position.y > -1.94)
                {
                    transform.position += new Vector3(0, -speed, 0);
                }
                else
                {
                    aiState = "rest";
                    refState3_5 = "";
                }
            }

            // Rest
            else if (aiState == "rest")
            {
                refState3b_5 = "";
                currBullet = maxBullet;
                useTime = baseUseTime;

                // Randomize next attack
                if (!hold_rand && (refState1a_5 == "" || refState1a_5 == null))
                {
                    // Opening, wait 1.5 seconds for player to adjust
                    if (time < 2)
                    {
                        StartCoroutine(delayRand(1.5f, 1, 2));
                    }

                    // Phase 2, 2 seconds between attacks
                    else if (refState3a_5 == "phase 2")
                    {
                        StartCoroutine(delayRand(2f, 1, 5));
                    }

                    // Phase 1, 3 seconds between attacks
                    else
                    {
                        StartCoroutine(delayRand(3f, 1, 3));
                    }
                }

                if (randNum == 1)
                {
                    aiState = "ramping";
                    hold_rand = false;
                }
                else if (randNum == 2)
                {
                    aiState = "exploding";
                    hold_rand = false;
                }
                else if (randNum == 3)
                {
                    if (refState2_5 != "scorching")
                    {
                        aiState = "scorched";
                    }
                    else
                    {
                        StartCoroutine(delayRand(0.5f, 1, 2));
                    }
                    hold_rand = false;
                }
                else if (randNum > 3)
                {
                    aiState = "beam";
                    hold_rand = false;
                }
                randNum = 0;
            }

            // Ramping Fire
            else if (aiState == "ramping" && (refState2a_5 == "" || refState2a_5 == null))
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                refState3b_5 = "pool_1";

                if (canShoot && currBullet > 0)
                {
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    fireBullet(direction, rotationZ, bulletSpeed[0]);
                    currBullet--;
                    StartCoroutine(cooldown());
                    useTime *= 0.85f;
                }
                else if (currBullet <= 0)
                {
                    aiState = "rest";
                }
            }

            // Exploding Shot
            else if (aiState == "exploding" && (refState2a_5 == "" || refState2a_5 == null))
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                refState3b_5 = "pool_2";

                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                fireBullet(direction, rotationZ, bulletSpeed[1]);
                aiState = "rest";
            }

            // Scorched Earth
            else if (aiState == "scorched")
            {
                refState_5 = "warning";
                aiState = "rest";
            }

            // Charge Beam
            if (aiState != "top" && aiState != "bottom" && refState3a_5 != "phase 2" && healthCurr < 25)
            {
                refState3a_5 = "phase 2";
                aiState = "beam";
            }

            if (aiState == "beam" && refState2a_5 != "beaming")
            {
                refState1a_5 = "warning";
                refState2a_5 = "beaming";
                if (transform.position.y >= 2.06)
                {
                    refState1b_5 = "top";
                }
                else if (transform.position.y <= -1.94)
                {
                    refState1b_5 = "bottom";
                }
                aiState = "rest";
            }
        }

        /*-----Containment Machina-----*/
        else if (gameObject.name == "Containment")
        {
            // Keep from straying outside outer box
            if (transform.position.x < -3.87f)
            {
                transform.position = new Vector3(-3.87f, transform.position.y);
            }
            else if (transform.position.x > 3.96f)
            {
                transform.position = new Vector3(3.96f, transform.position.y);
            }

            if (transform.position.y < -2.73f)
            {
                transform.position = new Vector3(transform.position.x, -2.73f);
            }
            else if (transform.position.y > 3.9f)
            {
                transform.position = new Vector3(transform.position.x, 3.9f);
            }

            // "Collision" with outer box
            if (refState2a_6 == "stop")
            {
                if (aiState == "crash" && refState3b_6 == "crashing")
                {
                    aiState = "rest";
                    refState3b_6 = "";
                }
                else if (aiState == "berserk" && refState3b_6 == "crashing")
                {
                    refState3b_6 = "";
                    refState3d_6 = "stage 2";
                }

                if (transform.position.x <= -3.87f)
                {
                    transform.position += new Vector3(0.1f, 0);
                }
                else if (transform.position.x >= 3.96f)
                {
                    transform.position += new Vector3(-0.1f, 0);
                }

                if (transform.position.y <= -2.73f)
                {
                    transform.position += new Vector3(0, 0.1f);
                }
                else if (transform.position.y >= 3.9f)
                {
                    transform.position += new Vector3(0, -0.1f);
                }
            }

            // Activate phase 2
            if (healthCurr <= 60 && refState3_6 != "phase 2")
            {
                refState3_6 = "phase 2";
                refState3d_6 = "stage 1";
                aiState = "berserk";
            }

            // Follow Player
            if (aiState == "follow")
            {
                refState3a_6 = aiState;

                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();

                // Faster Further, Slower Closer
                if (refState2_6 == "far")
                {
                    if (refState3_6 == "phase 2")
                    {
                        transform.position += new Vector3(direction.x * speed * 3, direction.y * speed * 3);
                    }
                    else
                    {
                        transform.position += new Vector3(direction.x * speed * 2, direction.y * speed * 2);
                    }
                }
                else
                {
                    if (refState3_6 == "phase 2")
                    {
                        transform.position += new Vector3(direction.x * speed * 1.5f, direction.y * speed * 1.5f);
                    }
                    else
                    {
                        transform.position += new Vector3(direction.x * speed, direction.y * speed);
                    }
                }

                if (!hold_time)
                {
                    StartCoroutine(addSecond());
                }

                if (time >= 6 || (time >= 3 && refState3_6 == "phase 2"))
                {
                    randNum = Random.Range(1, 4);
                    if (randNum > 1)
                    {
                        aiState = "rest";
                    }
                    else
                    {
                        aiState = "blink";
                    }
                    time = 0;
                    randNum = 0;
                }
            }

            // Rest
            else if (aiState == "rest")
            {
                time = 0;
                damage = 5;
                currBullet = maxBullet;

                // Wait then randomize attack based on previous aiState and current phase
                if (!hold_rand)
                {
                    if (refState3_6 == "phase 2")
                    {
                        if (refState3a_6 == "follow")
                        {
                            StartCoroutine(delayRand(0f, 1, 3));
                        }
                        else if (refState3a_6 == "berserk")
                        {
                            StartCoroutine(delayRand(3f, 1, 4));
                        }
                        else
                        {
                            StartCoroutine(delayRand(1f, 1, 3));
                        }
                    }
                    else
                    {
                        if (refState3a_6 == "follow")
                        {
                            StartCoroutine(delayRand(0f, 1, 3));
                        }
                        else
                        {
                            StartCoroutine(delayRand(2f, 1, 3));
                        }
                    }
                }

                if (randNum == 1)
                {
                    aiState = "follow";
                    hold_rand = false;
                }
                if (randNum == 2)
                {
                    aiState = "crash";
                    hold_rand = false;
                }
                else if (randNum == 3)
                {
                    aiState = "explosion";
                    hold_rand = false;
                }
                else if (randNum >= 4)
                {
                    aiState = "berserk";
                    hold_rand = false;
                }
                randNum = 0;
            }

            // Blink
            else if (aiState == "blink")
            {
                refState3a_6 = aiState;

                if (!refState3f_6)
                {
                    refState3f_6 = true;
                    // If player isn't moving
                    if (Player.rb2D.velocity.x == 0 && Player.rb2D.velocity.y == 0)
                    {
                        transform.position = new Vector3(Player.rb2D.position.x, Player.rb2D.position.y + 2f);
                    }
                    // Normal case
                    else if (!Player.dashing)
                    {
                        transform.position = new Vector3(Player.rb2D.position.x + Player.rb2D.velocity.x, Player.rb2D.position.y + Player.rb2D.velocity.y);
                    }
                }

                if (!hold_time)
                {
                    StartCoroutine(addSecond());
                }

                if (time >= 1)
                {
                    randNum = Random.Range(1, 4);
                    if (randNum > 1)
                    {
                        aiState = "follow";
                    }
                    else
                    {
                        aiState = "crash";
                    }
                    time = 0;
                    randNum = 0;
                    refState3f_6 = false;
                }
            }

            // Crash
            else if (aiState == "crash")
            {
                refState3a_6 = aiState;
                damage = 10;

                if (refState3b_6 != "crashing" && time < 1)
                {
                    refState_6 = "warning";
                    StartCoroutine(addSecond());
                }
                else if (time >= 1)
                {
                    refState_6 = "";
                    time = 0;
                }

                if (refState_6 != "warning" && refState_6 != null)
                {
                    if (refState3b_6 != "crashing")
                    {
                        refDifference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                        refDistance = refDifference.magnitude;
                        refDirection = refDifference / refDistance;
                        refDirection.Normalize();

                        if (refState2a_6 == "stop")
                        {
                            transform.position += new Vector3(refDirection.x * speed, refDirection.y * speed);
                        }
                    }

                    refState3b_6 = "crashing";
                    transform.position += new Vector3(refDirection.x * speed * 10, refDirection.y * speed * 10);
                }
            }

            // Explosion
            else if (aiState == "explosion")
            {
                refState3a_6 = aiState;

                if (canShoot && currBullet > 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        // Alternateing spread
                        if (currBullet % 2 == 0)
                        {
                            fireBullet(new Vector2(Mathf.Cos((2 * i + 1) * Mathf.PI / 8), Mathf.Sin((2 * i + 1) * Mathf.PI / 8)), 45 * i, 5f);
                        }
                        else
                        {
                            fireBullet(new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                        }
                    }
                    currBullet--;
                    StartCoroutine(cooldown());
                }
                else if (currBullet == 0)
                {
                    aiState = "rest";
                }
            }

            // Berserk
            else if (aiState == "berserk")
            {
                refState3a_6 = aiState;
                damage = 10;

                if (refState3e_6)
                {
                    refState_6 = "warning_1";
                    refState3e_6 = false;
                    StartCoroutine(addSecond());
                }
                else if (time >= 1)
                {
                    refState_6 = "";
                    time = 0;
                }

                if (refState_6 != "warning_1" && refState_6 != null)
                {
                    // Crash
                    if (refState3d_6 == "stage 1" && refState3c_6 < 3)
                    {
                        if (refState3b_6 != "crashing")
                        {
                            // "Collision" with outer box
                            if (refState2a_6 == "stop")
                            {
                                if (aiState == "crash" && refState3b_6 == "crashing")
                                {
                                    aiState = "rest";
                                    refState3b_6 = "";
                                }
                                else if (aiState == "berserk" && refState3b_6 == "crashing")
                                {
                                    refState3b_6 = "";
                                    refState3d_6 = "stage 2";
                                }
                            }

                            if (transform.position.y < -2.73f)
                            {
                                transform.position = new Vector3(transform.position.x, -2.73f);
                            }
                            else if (transform.position.y > 3.9f)
                            {
                                transform.position = new Vector3(transform.position.x, 3.9f);
                            }

                            refDifference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                            refDistance = refDifference.magnitude;
                            refDirection = refDifference / refDistance;
                            refDirection.Normalize();
                        }

                        refState3b_6 = "crashing";

                        transform.position += new Vector3(refDirection.x * speed * 10, refDirection.y * speed * 10);
                    }
                    // Explosion
                    else if (refState3d_6 == "stage 2")
                    {
                        if (refState2a_6 == "stop")
                        {
                            refDifference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                            refDistance = refDifference.magnitude;
                            refDirection = refDifference / refDistance;
                            refDirection.Normalize();

                            transform.position += new Vector3(refDirection.x * speed, refDirection.y * speed);
                        }
                        else
                        {
                            if (refState3c_6 < 3)
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    fireBullet(new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                                }
                                refState3c_6++;
                                refState3d_6 = "stage 1";
                            }
                        }
                    }
                }

                // Finish
                if (refState3c_6 == 3)
                {
                    refState3c_6 = 0;
                    refState3e_6 = true;
                    aiState = "rest";
                }
            }
        }

        /*-----Subnautical Machina-----*/
        else if (gameObject.name == "Subnautical")
        {
            // Underwater passive effects
            if (refState3_7 == "under")
            {
                defense = GlobalControl.damage / 2;

                // Movement
                if (pathState == "left")
                {
                    if (transform.position.x > x - range_2)
                    {
                        transform.position += new Vector3(-speed_1, 0);
                    }
                    else
                    {
                        pathState = "right";
                    }
                }
                else if (pathState == "right")
                {
                    if (transform.position.x < x + range_2)
                    {
                        transform.position += new Vector3(speed_1, 0);
                    }
                    else
                    {
                        pathState = "left";
                    }
                }

                // Timer
                if (time >= 12 && aiState != "downpour")
                {
                    aiState = "surface";
                }
                else if(!hold_time)
                {
                    StartCoroutine(addSecond());
                }
            }
            // Surface passive effects
            else if (refState3_7 == "above")
            {
                defense = 0;

                // Timer
                if (time >= 8 && aiState != "downpour")
                {
                    aiState = "dive";
                }
                else if(!hold_time)
                {
                    StartCoroutine(addSecond());
                }
            }

            // Downpour Timer
            if (healthCurr < 100 && !refState3a_7)
            {
                refState3a_7 = true;
                aiState = "downpour";
            }
            else if (refState3a_7)
            {
                if (time_1 >= 20)
                {
                    time_1 = 0;

                    float rand = Random.Range(0, 3);

                    if(rand > 0)
                        aiState = "downpour";
                }
                else if(!hold_time_1)
                {
                    StartCoroutine(addSecond_1());
                }
            }

            // Surface
            if (aiState == "surface")
            {
                time = 0;
                refState3_7 = "";

                // Chance to leap
                if (refState3a_7)
                {
                    float r = Random.Range(0, 2);
                    if (r > 0)
                    {
                        aiState = "leap";
                    }
                }

                if (transform.position.y < y + range_1)
                {
                    transform.position += new Vector3(0, speed);
                }
                else
                {
                    refState3_7 = "above";
                    aiState = "rest";
                }
            }

            // Dive
            else if (aiState == "dive")
            {
                time = 0;
                refState3_7 = "";

                if (transform.position.y > y)
                {
                    transform.position += new Vector3(0, -speed);
                }
                else
                {
                    refState3_7 = "under";
                    aiState = "rest";
                }
            }

            // Leap
            else if (aiState == "leap")
            {
                if (transform.position.y >= y + range_1)
                    refState3_7 = "above";

                if (refState3b_7 == "")
                {
                    refState3b_7 = "up";
                }
                else if (refState3b_7 == "up")
                {
                    if (transform.position.y < y + range_3)
                    {
                        transform.position += new Vector3(0, speed * 5);
                    }
                    else
                    {
                        refState3b_7 = "down";
                    }
                }
                else if (refState3b_7 == "down")
                {
                    if (transform.position.y > y + range_1)
                    {
                        transform.position += new Vector3(0, -speed * 2);
                    }
                    else
                    {
                        refState3b_7 = "";
                        aiState = "rest";
                    }
                }
            }

            // Rest
            else if (aiState == "rest")
            {
                useTime = baseUseTime;
                currBullet = maxBullet;
                
                if (refState3_7 == "under")
                {
                    if (!hold_rand)
                    {
                        StartCoroutine(delayRand(2f, 1, 2));
                    }
                }
                else if (refState3_7 == "above")
                {
                    if (!hold_rand)
                    {
                        StartCoroutine(delayRand(2f, 1, 2));
                    }
                }

                if (randNum == 1)
                {
                    if (refState3_7 == "above")
                        aiState = "scatter";
                    else
                        aiState = "crystal";
                    hold_rand = false;
                }
                else if (randNum == 2)
                {
                    if (refState3_7 == "above")
                        aiState = "scatter";
                    else
                        aiState = "torpedo";
                    hold_rand = false;
                }
                randNum = 0;
            }

            // Scatter Shot
            else if (aiState == "scatter")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (canShoot && ((currBullet > -1 && !refState3a_7) || (currBullet > -2 && refState3a_7)))
                {
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    for (int i = 0; i < 3; i++)
                    {
                        if (refState3a_7)
                        {
                            if(direction.x * direction.y >= 0)
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 1)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 1))), rotationZ + 20 * (i - 1), bulletSpeed[0]);
                            else if(direction.x < direction.y)
                                fireBullet(new Vector2(Mathf.Sqrt(1 - Mathf.Pow(Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 1)), 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 1))), rotationZ + 20 * (i - 1), bulletSpeed[0]);
                            else
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 1)), Mathf.Sqrt(1 - Mathf.Pow(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 1)), 2))), rotationZ + 20 * (i - 1), bulletSpeed[0]);
                        }
                        else
                        {
                            if(direction.x * direction.y >= 0)
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                            else if(direction.x > direction.y)
                                fireBullet(new Vector2(Mathf.Sqrt(1 - Mathf.Pow(Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1)), 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                            else
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sqrt(1 - Mathf.Pow(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), 2))), rotationZ + 30 * (i - 1), bulletSpeed[0]);

                        }
                    }
                    currBullet--;
                    StartCoroutine(cooldown());
                }
                else if ((currBullet <= -1 && !refState3a_7) || (currBullet <= -2 && refState3a_7))
                {
                    aiState = "rest";
                }
            }

            // Crystal Barrage
            else if (aiState == "crystal")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                // 3 bullet
                if (refState3c_7 == "")
                {
                    if (canShoot && currBullet > 0)
                    {
                        float distance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();
                        fireBullet(direction, rotationZ, bulletSpeed[0]);
                        currBullet--;
                        StartCoroutine(cooldown());
                    }
                    else if (currBullet <= 0)
                    {
                        refState3c_7 = "end";
                    }
                }
                // Bouncing bullet
                else if (refState3c_7 == "end")
                {
                    useTime = 1.5f;

                    if (canShoot)
                    {
                        float distance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();
                        fireBullet(direction, rotationZ, bulletSpeed[0]);
                        StartCoroutine(cooldown());

                        if (refState3a_7)
                        {
                            refState3c_7 = "second";
                            currBullet = maxBullet - 1;
                        }
                        else
                        {
                            refState3c_7 = "";
                            aiState = "rest";
                        }
                    }                   
                }

                // Phase 2 extended
                if (refState3a_7)
                {
                    // 2 bullet
                    if (refState3c_7 == "second")
                    {
                        useTime = 0.25f;

                        if (canShoot && currBullet > 0)
                        {
                            float distance = difference.magnitude;
                            Vector2 direction = difference / distance;
                            direction.Normalize();
                            fireBullet(direction, rotationZ, bulletSpeed[0]);
                            currBullet--;
                            StartCoroutine(cooldown());
                        }
                        else if (currBullet <= 0)
                        {
                            refState3c_7 = "end 2";
                        }
                    }
                    // Bouncing bullet
                    else if (refState3c_7 == "end 2")
                    {
                        if (canShoot)
                        {
                            float distance = difference.magnitude;
                            Vector2 direction = difference / distance;
                            direction.Normalize();
                            fireBullet(direction, rotationZ, bulletSpeed[0]);
                            refState3c_7 = "";
                            aiState = "rest";
                        }                        
                    }
                }
            }

            // Torpedo
            else if (aiState == "torpedo")
            {
                refState_7 = true;

                if (canShoot && ((currBullet > 0 && !refState3a_7) || (currBullet > -2 && refState3a_7)))
                {
                    fireBullet(new Vector2(0, 1), 0, bulletSpeed[1]);
                    currBullet--;
                    StartCoroutine(cooldown());
                }
                else if ((currBullet <= 0 && !refState3a_7) || (currBullet <= -2 && refState3a_7))
                {
                    aiState = "rest";
                    refState_7 = false;
                }
            }

            // Downpour
            else if (aiState == "downpour")
            {
                if (refState1b_7 == "")
                {
                    if(healthCurr < 50)
                        refState1b_7 = "storming";
                    else
                        refState1b_7 = "pouring";
                }
                else if (refState1b_7 == "finish")               
                    aiState = "rest";                
            }
        }

        // Set reference state
        if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            refState_1 = aiState;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            /*-----Conveyor Belt AI-----*/
            if (gameObject.name.Substring(0, 8) == "Conveyor")
            {
                if (aiState == "left" && Player.rb2D.velocity.x > -1.2 * Player.moveBy)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x - Player.moveBy / 4, Player.rb2D.velocity.y);
                }
                else if (aiState == "right" && Player.rb2D.velocity.x < 1.2 * Player.moveBy)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x + Player.moveBy / 4, Player.rb2D.velocity.y);
                }
                else if (aiState == "up" && Player.rb2D.velocity.y < 1.2 * Player.moveBy)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, Player.rb2D.velocity.y + Player.moveBy / 4);
                }
                else if (aiState == "down" && Player.rb2D.velocity.y > -1.2 * Player.moveBy)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, Player.rb2D.velocity.y - Player.moveBy / 4);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // On bullet hit -> take damage + other
        if (other.gameObject.tag == "Player Bullet")
        {
            if (gameObject.CompareTag("Enemy") && !hazard)
            {
                // Unstable bypasses defense
                if (GlobalControl.reactor == "unstable")
                {
                    healthCurr -= GlobalControl.damage;

                    // Damage indication
                    StartCoroutine(dmgFlash(0.05f));
                }
                // Other reactor dmg calculation
                else
                {
                    if (GlobalControl.damage - defense > 0)
                    {
                        healthCurr -= (GlobalControl.damage - defense);

                        // Damage indication
                        StartCoroutine(dmgFlash(0.05f));
                    }
                }                
                Debug.Log(healthCurr + " HP remaining!");                

                // Other effects
                if (gameObject.name.Substring(0, 7) == "Pursuit")
                {
                    if (Player.rb2D.position.x < transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                        aiState = "hostile_left";
                    }
                    else if (Player.rb2D.position.x > transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                        aiState = "hostile_right";
                    }
                    time = 0;
                }
            }
            else if (gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial")
            {
                healthCurr = 0;
            }
        }

        // On hit player -> damage + knockback
        else if (other.name == "Player" && !GlobalControl.immune && !passive)
        {
            if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat"))
            {
                // Damage calculation
                if (GlobalControl.reactor == "unstable")
                {
                    GlobalControl.healthCurr = 0;
                }
                else
                {
                    GlobalControl.healthCurr -= damage;
                    GlobalControl.immune = true;
                }

                // On hit effects
                if (gameObject.name.Substring(0, 6) == "Patrol")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(-3f, 2f);
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(3f, 2f);
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 7) == "Pursuit")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 3f);
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(-2f, 3f);
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(2f, 3f);
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 6) == "Aerial")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                        }
                        else
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -1f);
                        }
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                            {
                                Player.rb2D.velocity = new Vector2(-3f, 2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(-3f, -1f);
                            }
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                            {
                                Player.rb2D.velocity = new Vector2(3f, 2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(3f, -1f);
                            }
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 7) == "Aquatic")
                {

                }
                else if (gameObject.name == "Overseer")
                {
                    Player.rb2D.velocity = new Vector2(-5f, 0f);
                }
                else if (gameObject.name == "Containment")
                {
                    if (Player.rb2D.position.x == transform.position.x)
                    {
                        if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                        }
                        else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                        }
                        else
                        {
                            float rand = Random.Range(0, 2);
                            if (rand > 0)
                            {
                                Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                            }
                        }
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(-5f, 2f);
                            }
                            else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(-5f, -2f);
                            }
                            else
                            {
                                float rand = Random.Range(0, 2);
                                if (rand > 0)
                                {
                                    Player.rb2D.velocity = new Vector2(-5f, -2f);
                                }
                                else
                                {
                                    Player.rb2D.velocity = new Vector2(-5f, 2f);
                                }
                            }
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(5f, 2f);
                            }
                            else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(5f, -2f);
                            }
                            else
                            {
                                float rand = Random.Range(0, 2);
                                if (rand > 0)
                                {
                                    Player.rb2D.velocity = new Vector2(5f, -2f);
                                }
                                else
                                {
                                    Player.rb2D.velocity = new Vector2(5f, 2f);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // On hit player -> damage + knockback
        if (other.name == "Player" && !GlobalControl.immune && !passive)
        {
            if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat"))
            {
                // Damage calculation
                if (GlobalControl.reactor == "unstable")
                {
                    GlobalControl.healthCurr = 0;
                }
                else
                {
                    GlobalControl.healthCurr -= damage;
                    GlobalControl.immune = true;
                }

                // On hit effects
                if (gameObject.name.Substring(0, 6) == "Patrol")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(-3f, 2f);
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(3f, 2f);
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 7) == "Pursuit")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 3f);
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(-2f, 3f);
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            Player.rb2D.velocity += new Vector2(2f, 3f);
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 6) == "Aerial")
                {
                    if (Player.rb2D.position.x > transform.position.x - GetComponent<BoxCollider2D>().size.x / 3 && Player.rb2D.position.x < transform.position.x + GetComponent<BoxCollider2D>().size.x / 3)
                    {
                        if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                        }
                        else
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -1f);
                        }
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                            {
                                Player.rb2D.velocity = new Vector2(-3f, 2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(-3f, -1f);
                            }
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y)
                            {
                                Player.rb2D.velocity = new Vector2(3f, 2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(3f, -1f);
                            }
                        }
                    }
                }
                else if (gameObject.name.Substring(0, 7) == "Aquatic")
                {

                }
                else if (gameObject.name == "Overseer")
                {
                    Player.rb2D.velocity = new Vector2(-5f, 0f);
                }
                else if (gameObject.name == "Containment")
                {
                    if (Player.rb2D.position.x == transform.position.x)
                    {
                        if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                        }
                        else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                        {
                            Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                        }
                        else
                        {
                            float rand = Random.Range(0, 2);
                            if (rand > 0)
                            {
                                Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                            }
                            else
                            {
                                Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                            }
                        }
                    }
                    else
                    {
                        if (transform.position.x > Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(-5f, 2f);
                            }
                            else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(-5f, -2f);
                            }
                            else
                            {
                                float rand = Random.Range(0, 2);
                                if (rand > 0)
                                {
                                    Player.rb2D.velocity = new Vector2(-5f, -2f);
                                }
                                else
                                {
                                    Player.rb2D.velocity = new Vector2(-5f, 2f);
                                }
                            }
                        }
                        else if (transform.position.x < Player.rb2D.position.x)
                        {
                            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(5f, 2f);
                            }
                            else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<BoxCollider2D>().size.y / 2)
                            {
                                Player.rb2D.velocity = new Vector2(5f, -2f);
                            }
                            else
                            {
                                float rand = Random.Range(0, 2);
                                if (rand > 0)
                                {
                                    Player.rb2D.velocity = new Vector2(5f, -2f);
                                }
                                else
                                {
                                    Player.rb2D.velocity = new Vector2(5f, 2f);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Effects
        if (other.name == "Player" && gameObject.name.Substring(0, 5) == "Water")
        {
            if (Mathf.Abs(Player.rb2D.velocity.x) > 0.5f * Player.moveBy)
            {
                Player.rb2D.velocity /= new Vector2(1.05f, 1f);
            }

            if (Mathf.Abs(Player.rb2D.velocity.y) > Player.moveBy)
            {
                Player.rb2D.velocity /= new Vector2(1f, 1.05f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    void fireBullet(Vector2 direction, float rotation2, float speed)
    {
        // Fires a bullet from the pool
        GameObject bullet = null;

        // Single bullet type
        if (gameObject.name.Substring(0, 6) == "Aerial" || gameObject.name.Substring(0, 7) == "Aquatic" || gameObject.name == "Containment")
        {
            bullet = EnemyObjectPooler.SharedInstance.GetPooledObject();
        }

        // Overseer
        if (gameObject.name == "Overseer")
        {
            if (refState3b_5 == "pool_1")
            {
                bullet = EnemyObjectPooler.SharedInstance.GetPooledObject();
            }
            else if (refState3b_5 == "pool_2")
            {
                bullet = EnemyObjectPooler2.SharedInstance.GetPooledObject();
            }
        }
        else if (gameObject.name == "Subnautical")
        {
            if (aiState == "scatter" || aiState == "torpedo")
            {
                bullet = EnemyObjectPooler2.SharedInstance.GetPooledObject();
            }
            else if (refState3c_7 == "" || refState3c_7 == "second")
            {
                bullet = EnemyObjectPooler3.SharedInstance.GetPooledObject();
            }
            else if (refState3c_7 == "end" || refState3c_7 == "end 2")
            {
                bullet = EnemyObjectPooler4.SharedInstance.GetPooledObject();
            }
        }

        if (bullet != null)
        {
            bullet.SetActive(true);

            // Custom bullet spawn position - Note that size does not take into account scale
            if (gameObject.name.Substring(0, 7) == "Aquatic")
            {
                if (pathState == "left")
                {
                    bullet.transform.position = new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x * 2, transform.position.y, transform.position.z);
                }
                else if (pathState == "right")
                {
                    bullet.transform.position = new Vector3(transform.position.x + GetComponent<BoxCollider2D>().size.x * 2, transform.position.y, transform.position.z);
                }
            }
            else if (gameObject.name == "Overseer")
            {
                bullet.transform.position = new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
            }           
            else
            {
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation2);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    IEnumerator addSecond()
    {
        hold_time = true;
        yield return new WaitForSeconds(1f);
        time++;
        hold_time = false;
    }

    IEnumerator addSecond_1()
    {
        hold_time_1 = true;
        yield return new WaitForSeconds(1f);
        time_1++;
        hold_time_1 = false;
    }

    IEnumerator delayRand(float time, int min, int max)
    {
        hold_rand = true;
        yield return new WaitForSeconds(time);
        randNum = Random.Range(min, max + 1);
    }

    IEnumerator cooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(useTime);

        canShoot = true;
    }

    IEnumerator dmgFlash(float time)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if(animated)
        GetComponent<Animator>().enabled = false;

        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().enabled = true;
        if(animated)
        GetComponent<Animator>().enabled = true;
    }
}
