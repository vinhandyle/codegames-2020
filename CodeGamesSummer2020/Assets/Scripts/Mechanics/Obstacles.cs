using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Non-static variables are separate of each other when applied to different objects (i.e. multiple enemies using this script)
    public int healthMax;
    public int healthCurr;
    public int damage;
    public bool hazard;
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
    public float speed;
    public float speed_1;

    // Time
    public int time;    // AI time
    public int deAggroTime;
    public bool hold_time = false; // hold time

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
    public static string refState_1;    // Current aiState
    public static string refState2_1;   // Should it be passive?
    public static string refState2a_1;  // Should it stop?
    public string refState3_1;          // Where was it facing before it stopped?

    /* Aerial */
    public static string refState2_2;   // Is player in range?

    /* Overseer */
    public static string refState_5;    // Scorched Earth attack phase
    public static string refState1a_5;  // Charge Beam attack phase
    public static string refState1b_5;  // Center segment position
    public static string refState2_5;   // Is Scorched Earth Active?
    public static string refState2a_5;  // Is Charge Beam active?
    public string refState3_5;          // Is Gear Shift active?
    public string refState3a_5;         // Current boss phase
    public string refState3b_5;         // Which ObjectPooler?

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
            (gameObject.name == "Errat_0" && !GlobalControl.errat_0) ||
            (gameObject.name == "Errat_1" && !GlobalControl.errat_1) ||
            (gameObject.name == "Errat_2" && !GlobalControl.errat_2) ||
            (gameObject.name == "Errat_3" && !GlobalControl.errat_3) ||
            (gameObject.name == "Errat_4" && !GlobalControl.errat_4) ||
            (gameObject.name == "Errat_5" && !GlobalControl.errat_5) ||
            (gameObject.name == "Overseer" && GlobalControl.downed_boss_1))
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
                healthMax = 5;
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
                GlobalControl.patrol_2_3_2 = false;
            }
            else if (gameObject.name == "Patrol_2_3_5")
            {
                GlobalControl.patrol_2_3_3 = false;
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
                if(!hold_time)
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
            if (refState2_2 == "in")
            {
                aiState = "shoot";
            }
            else
            {
                aiState = "";
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
            {
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
            // Insert AI here
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
            if (time > 0 && refState3_5 != "shifted" && ((time % 10 == 0 && refState3a_5 != "phase 2") || (time % 5 == 0 && refState3a_5 == "phase 2")) && (refState2a_5 == "" || refState2a_5 == null))
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
                }
                else if (randNum == 2)
                {
                    aiState = "exploding";
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
                }
                else if (randNum > 3)
                {
                    aiState = "beam";
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
                // Take damage
                healthCurr -= GlobalControl.damage;
                Debug.Log(healthCurr + " HP remaining!");

                // Damage indication
                StartCoroutine(dmgFlash(0.05f));

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
        else if (other.name == "Player" && !GlobalControl.immune)
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

                }
                else if (gameObject.name.Substring(0, 7) == "Aquatic")
                {

                }
                else if (gameObject.name == "Overseer")
                {
                    Player.rb2D.velocity = new Vector2(-5f, 0f);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // On hit player -> damage + knockback
        if (other.name == "Player" && !GlobalControl.immune)
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

                }
                else if (gameObject.name.Substring(0, 7) == "Aquatic")
                {

                }
                else if (gameObject.name == "Overseer")
                {
                    Player.rb2D.velocity = new Vector2(-5f, 0f);
                }
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

        if (gameObject.name.Substring(0, 6) == "Aerial")
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

        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
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

    IEnumerator delayRand(float time, int min, int max)
    {
        hold_rand = true;
        yield return new WaitForSeconds(time);
        randNum = Random.Range(min, max + 1);
        hold_rand = false;
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
