using System.Collections;
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
    public float deg;

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
    public float time_f;
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

    // Boss Health Indicator
    public static int hp_Max;
    public static int hp_Curr; 

    // States -> refState,type,variant(if applicable)_num
    // type: Obstacles to Scope (1), Scope to Obstacles (2), Obstacles to Obstacles (3)
    // variant: multiple of same type in same num, use a->z
    // num: Pursuit (1), Aerial (2), Aquatic (3), Turret (4), Overseer (5), Containment (6), Subnautical (7), Emperor (8)
    // Add a "H" before num for hazard AI: Electrical Line (1),
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

    /* Turret */
    public static bool refState2_4;             // Is player in sight?
    public static float refState2a_4;           // Self-to-player angle
    public static bool refState2b_4;           // Preset?
    public float refState3a_4;                  // Center angle
    public float refState3b_4;                  // Pivot x
    public float refState3c_4;                  // Pivot y

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
    public bool refState3f_6;                   // Blinked?
    public bool refState3g_6;                   // Already used explosion during berserk crash
    public bool refState3h_6;                   // Prevent multiple timer_1 incs

    /* Subnautical */
    public static bool refState_7;              // Torpedo active?
    public static float refState1a_7 = 0.004f;   // Torpedo speed
    public static string refState1b_7;          // Downpouring?
    public string refState3_7 = "under";        // Above or below water?
    public bool refState3a_7;                   // Phase 2?
    public string refState3b_7;                 // Leap State
    public string refState3c_7;                 // Crystal Barrage State

    /* Emperor */
    public static int refState_8 = 0;           // Number of shields
    public static int refState1a_8;             // Which pool to use for bullet
    public static string refState1b_8 = "";     // Gale stage
    public static string refState1c_8 = "";     // Judgement stage
    public static int refState1d_8;             // Summon stage

    /* Electrical Line */
    public float refState3_1H;                  // Zap timer
    public float refState3a_1H;                 // Zap timer delay
    public float refState3b_1H;                 // Zap timer accel

    /* Gust */
    public bool refState3_2H;                   // Gust spin direction

    /* Sentry */
    public static bool refState_3H;             // Sentry is at min range?
    public static bool refState1a_3H;           // Summon threshhold met?
    public static int refState1b_3H;            // Sentries active
    public static bool refState1c_3H;           // Sentry 1 active?
    public static bool refState1d_3H;           // Sentry 2 active?
    public bool refState3_3H;                   // Sentry was called in?
    public string refState3a_3H;                // Rotation direction

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
            (gameObject.name == "Patrol_2_4_1" && !GlobalControl.patrol_2_4_1) ||
            (gameObject.name == "Patrol_2_4_2" && !GlobalControl.patrol_2_4_2) ||
            (gameObject.name == "Patrol_2_4_3" && !GlobalControl.patrol_2_4_3) ||
            (gameObject.name == "Patrol_2_4_4" && !GlobalControl.patrol_2_4_4) ||
            (gameObject.name == "Patrol_2_4_5" && !GlobalControl.patrol_2_4_5) ||
            (gameObject.name == "Patrol_2_4_6" && !GlobalControl.patrol_2_4_6) ||
            (gameObject.name == "Patrol_2_4_7" && !GlobalControl.patrol_2_4_7) ||
            (gameObject.name == "Patrol_2_4_8" && !GlobalControl.patrol_2_4_8) ||
            (gameObject.name == "Patrol_2_4_9" && !GlobalControl.patrol_2_4_9) ||
            (gameObject.name == "Patrol_2_4_10" && !GlobalControl.patrol_2_4_10) ||
            (gameObject.name == "Patrol_2_4_11" && !GlobalControl.patrol_2_4_11) ||
            (gameObject.name == "Patrol_2_4_12" && !GlobalControl.patrol_2_4_12) ||
            (gameObject.name == "Patrol_2_4_13" && !GlobalControl.patrol_2_4_13) ||
            (gameObject.name == "Patrol_3_1_0" && !GlobalControl.patrol_3_1_0) ||
            (gameObject.name == "Patrol_3_1_1" && !GlobalControl.patrol_3_1_1) ||
            (gameObject.name == "Patrol_3_1_2" && !GlobalControl.patrol_3_1_2) ||
            (gameObject.name == "Patrol_3_5_0" && !GlobalControl.patrol_3_5_0) ||
            (gameObject.name == "Patrol_3_5_1" && !GlobalControl.patrol_3_5_1) ||
            (gameObject.name == "Patrol_3_5_2" && !GlobalControl.patrol_3_5_2) ||
            (gameObject.name == "Pursuit_1_2_0" && !GlobalControl.pursuit_1_2_0) ||
            (gameObject.name == "Pursuit_1_2_1" && !GlobalControl.pursuit_1_2_1) ||
            (gameObject.name == "Pursuit_1_2_2" && !GlobalControl.pursuit_1_2_2) ||
            (gameObject.name == "Pursuit_1_2_3" && !GlobalControl.pursuit_1_2_3) ||
            (gameObject.name == "Pursuit_1_2_4" && !GlobalControl.pursuit_1_2_4) ||
            (gameObject.name == "Pursuit_1_2_5" && !GlobalControl.pursuit_1_2_5) ||
            (gameObject.name == "Pursuit_1_2_6" && !GlobalControl.pursuit_1_2_6) ||
            (gameObject.name == "Pursuit_1_2_7" && !GlobalControl.pursuit_1_2_7) ||
            (gameObject.name == "Pursuit_1_2_8" && !GlobalControl.pursuit_1_2_8) ||
            (gameObject.name == "Pursuit_2_5_0" && !GlobalControl.pursuit_2_5_0) ||
            (gameObject.name == "Aerial_1_3_0" && !GlobalControl.aerial_1_3_0) ||
            (gameObject.name == "Aerial_1_3_1" && !GlobalControl.aerial_1_3_1) ||
            (gameObject.name == "Aerial_1_3_2" && !GlobalControl.aerial_1_3_2) ||
            (gameObject.name == "Aerial_1_3_3" && !GlobalControl.aerial_1_3_3) ||
            (gameObject.name == "Aerial_1_3_4" && !GlobalControl.aerial_1_3_4) ||
            (gameObject.name == "Aerial_1_3_5" && !GlobalControl.aerial_1_3_5) ||
            (gameObject.name == "Aerial_1_3_6" && !GlobalControl.aerial_1_3_6) ||
            (gameObject.name == "Aerial_1_3_7" && !GlobalControl.aerial_1_3_7) ||
            (gameObject.name == "Aerial_1_3_8" && !GlobalControl.aerial_1_3_8) ||
            (gameObject.name == "Aerial_1_3_9" && !GlobalControl.aerial_1_3_9) ||
            (gameObject.name == "Aerial_2_5_0" && !GlobalControl.aerial_2_5_0) ||
            (gameObject.name == "Aerial_2_5_1" && !GlobalControl.aerial_2_5_1) ||
            (gameObject.name == "Aerial_2_5_2" && !GlobalControl.aerial_2_5_2) ||
            (gameObject.name == "Aerial_1_3_10" && !GlobalControl.aerial_1_3_10) ||
            (gameObject.name == "Aquatic_1_4_0" && !GlobalControl.aquatic_1_4_0) ||
            (gameObject.name == "Aquatic_1_4_1" && !GlobalControl.aquatic_1_4_1) ||
            (gameObject.name == "Aquatic_1_4_2" && !GlobalControl.aquatic_1_4_2) ||
            (gameObject.name == "Aquatic_1_4_3" && !GlobalControl.aquatic_1_4_3) ||
            (gameObject.name == "Aquatic_1_4_4" && !GlobalControl.aquatic_1_4_4) ||
            (gameObject.name == "Aquatic_1_4_5" && !GlobalControl.aquatic_1_4_5) ||
            (gameObject.name == "Aquatic_1_4_6" && !GlobalControl.aquatic_1_4_6) ||
            (gameObject.name == "Aquatic_2_5_0" && !GlobalControl.aquatic_2_5_0) ||
            (gameObject.name == "Aquatic_2_5_1" && !GlobalControl.aquatic_2_5_1) ||
            (gameObject.name == "Turret_1_1_0" && !GlobalControl.turret_1_1_0) ||
            (gameObject.name == "Turret_1_1_1" && !GlobalControl.turret_1_1_1) ||
            (gameObject.name == "Turret_1_1_2" && !GlobalControl.turret_1_1_2) ||
            (gameObject.name == "Turret_1_1_3" && !GlobalControl.turret_1_1_3) ||
            (gameObject.name == "Turret_1_1_4" && !GlobalControl.turret_1_1_4) ||
            (gameObject.name == "Turret_1_1_5" && !GlobalControl.turret_1_1_5) ||
            (gameObject.name == "Turret_1_1_6" && !GlobalControl.turret_1_1_6) ||
            (gameObject.name == "Turret_2_5_0" && !GlobalControl.turret_2_5_0) ||
            (gameObject.name == "Turret_2_5_1" && !GlobalControl.turret_2_5_1) ||
            (gameObject.name == "Errat_0_" && !GlobalControl.errat_0) ||
            (gameObject.name == "Errat_1_" && !GlobalControl.errat_1) ||
            (gameObject.name == "Errat_2_" && !GlobalControl.errat_2) ||
            (gameObject.name == "Errat_3_" && !GlobalControl.errat_3) ||
            (gameObject.name == "Errat_4_" && !GlobalControl.errat_4) ||
            (gameObject.name == "Errat_5_" && !GlobalControl.errat_5) ||
            (gameObject.name == "Overseer" && GlobalControl.downed_boss_1) ||
            (gameObject.name == "Containment" && GlobalControl.downed_boss_2) ||
            (gameObject.name == "Subnautical" && GlobalControl.downed_boss_3) ||
            (gameObject.name == "Emperor_" && GlobalControl.downed_boss_4))
        {
            gameObject.SetActive(false);
        }

        // Set starting position
        x = transform.position.x;
        y = transform.position.y;

        // Set rotation pivot
        deg = transform.rotation.z * Mathf.Rad2Deg;

        // Set center angle
        if (gameObject.name.Substring(0, 6) == "Turret")
        {
            deg = refState3a_4;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, deg);
        }

        if (gameObject.name.Substring(0, 6) != "Cutter" && gameObject.name.Substring(0, 4) != "Gust" && gameObject.name.Substring(0, 7) != "Droplet" && gameObject.name != "Emperor_")
        {
            refState3b_4 = x - GetComponent<BoxCollider2D>().size.x * Mathf.Cos(deg * Mathf.Deg2Rad) / 2;
            refState3c_4 = y - GetComponent<BoxCollider2D>().size.y * Mathf.Sin(deg * Mathf.Deg2Rad);
        }

        // If asymmetrical range is not set, set symmetrical range
        if (range_1 + range_2 == 0)
        {
            range_1 = range;
            range_2 = range;
        }

        // Load gun
        currBullet = maxBullet;
        useTime = baseUseTime;

        // Delay or accel zap timer
        if (gameObject.name.Substring(0, 8) == "Electric")
        {
            if (aiState == "active")
                time_f = 120;
            else
            {
                if (refState3a_1H > 0)
                    time_f = refState3_1H + refState3a_1H;
                else if (refState3b_1H > 0)
                    time_f = refState3_1H - refState3b_1H;
                else
                    time_f = refState3_1H;

            }
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
                healthMax = 8;
                damage = 2;
            }
            else if (gameObject.name.Substring(6, 2) == "_3")
            { // Tier 3: IT (2nd), Grey Palace
                healthMax = 16;
                damage = 3;
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
                healthMax = 12;
                damage = 5;
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
                healthMax = 12;
                damage = 3;
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
                healthMax = 20;
                damage = 4;
            }
        }

        // Turret Machina
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: IT (2nd)
                healthMax = 20;
                damage = 3;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Grey Palace
                healthMax = 30;
                damage = 5;
            }
        }

        // Sentry
        else if (gameObject.name.Substring(0, 6) == "Sentry")
        {
            healthMax = 20;
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
        {
            damage = 2;
        }
        // Super-heated Area
        else if (gameObject.name.Substring(0, 6) == "Molten")
        {
            damage = 5;
        }
        // Cutter
        else if (gameObject.name.Substring(0, 6) == "Cutter")
        {
            damage = 10;
        }
        // Charge Beam
        else if (gameObject.name.Substring(0, 7) == "OM_Beam")
        {
            damage = 8;
        }
        // Downpour
        else if (gameObject.name.Substring(0, 4) == "Drop")
        {
            damage = 4;
        }
        // Overheat
        else if (gameObject.name.Substring(0, 5) == "Blaze")
        {
            damage = 10;
        }
        // Gale
        else if (gameObject.name.Substring(0, 4) == "Gust")
        {
            damage = 8;
        }

        /*---------------Bosses----------------*/
        else if (gameObject.name == "Overseer")
        {
            healthMax = 50;
            damage = 2;
            hp_Max = healthMax;
        }
        else if (gameObject.name == "Containment")
        {
            healthMax = 180;
            damage = 5;
            hp_Max = healthMax;
        }
        else if (gameObject.name == "Subnautical")
        {
            healthMax = 200;
            damage = 4;
            hp_Max = healthMax;
        }
        else if (gameObject.name == "Emperor_")
        {
            healthMax = 500;
            damage = 10;
            hp_Max = healthMax;
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
            if(gameObject.name.Substring(0, 6) != "Sentry")
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
            else if (gameObject.name.Substring(0, 6) == "Sentry")
            {
                aiState = "inactive";
                refState1b_3H--;
                healthCurr = healthMax;
                refState3_3H = false;
                if (gameObject.name == "Sentry__")
                    refState1c_3H = false;
                else if (gameObject.name == "Sentry__ (1)")
                    refState1d_3H = false;
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
            else if (gameObject.name == "Emperor_")
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

            // Institute of Technology
            else if (gameObject.name == "Patrol_1_1_0")
            {
                GlobalControl.patrol_1_1_0 = false;
            }
            else if (gameObject.name == "Patrol_3_1_0")
            {
                GlobalControl.patrol_3_1_0 = false;
            }
            else if (gameObject.name == "Patrol_3_1_1")
            {
                GlobalControl.patrol_3_1_0 = false;
            }
            else if (gameObject.name == "Patrol_3_1_2")
            {
                GlobalControl.patrol_3_1_0 = false;
            }
            else if (gameObject.name == "Turret_1_1_0")
            {
                GlobalControl.turret_1_1_0 = false;
            }
            else if (gameObject.name == "Turret_1_1_1")
            {
                GlobalControl.turret_1_1_1 = false;
            }
            else if (gameObject.name == "Turret_1_1_2")
            {
                GlobalControl.turret_1_1_2 = false;
            }
            else if (gameObject.name == "Turret_1_1_3")
            {
                GlobalControl.turret_1_1_3 = false;
            }
            else if (gameObject.name == "Turret_1_1_4")
            {
                GlobalControl.turret_1_1_4 = false;
            }
            else if (gameObject.name == "Turret_1_1_5")
            {
                GlobalControl.turret_1_1_5 = false;
            }
            else if (gameObject.name == "Turret_1_1_6")
            {
                GlobalControl.turret_1_1_6 = false;
            }

            // Dreg Heap
            else if (gameObject.name.Substring(0, 5) == "Errat")
            {
                if (gameObject.name == "Errat_0_")
                {
                    GlobalControl.errat_0 = false;
                }
                else if (gameObject.name == "Errat_1_")
                {
                    GlobalControl.errat_1 = false;
                }
                else if (gameObject.name == "Errat_2_")
                {
                    GlobalControl.errat_2 = false;
                }
                else if (gameObject.name == "Errat_3_")
                {
                    GlobalControl.errat_3 = false;
                }
                else if (gameObject.name == "Errat_4_")
                {
                    GlobalControl.errat_4 = false;
                }
                else if (gameObject.name == "Errat_5_")
                {
                    GlobalControl.errat_5 = false;
                }
                GlobalControl.humansLeft--;
                GlobalControl.found_errat = true;
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
            else if (gameObject.name == "Pursuit_1_2_8")
            {
                GlobalControl.pursuit_1_2_8 = false;
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
            else if (gameObject.name == "Aerial_1_3_9")
            {
                GlobalControl.aerial_1_3_9 = false;
            }
            else if (gameObject.name == "Aerial_1_3_10")
            {
                GlobalControl.aerial_1_3_10 = false;
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
            else if (gameObject.name == "Patrol_2_4_1")
            {
                GlobalControl.patrol_2_4_1 = false;
            }
            else if (gameObject.name == "Patrol_2_4_2")
            {
                GlobalControl.patrol_2_4_2 = false;
            }
            else if (gameObject.name == "Patrol_2_4_3")
            {
                GlobalControl.patrol_2_4_3 = false;
            }
            else if (gameObject.name == "Patrol_2_4_4")
            {
                GlobalControl.patrol_2_4_4 = false;
            }
            else if (gameObject.name == "Patrol_2_4_5")
            {
                GlobalControl.patrol_2_4_5 = false;
            }
            else if (gameObject.name == "Patrol_2_4_6")
            {
                GlobalControl.patrol_2_4_6 = false;
            }
            else if (gameObject.name == "Patrol_2_4_7")
            {
                GlobalControl.patrol_2_4_7 = false;
            }
            else if (gameObject.name == "Patrol_2_4_8")
            {
                GlobalControl.patrol_2_4_8 = false;
            }
            else if (gameObject.name == "Patrol_2_4_9")
            {
                GlobalControl.patrol_2_4_9 = false;
            }
            else if (gameObject.name == "Patrol_2_4_10")
            {
                GlobalControl.patrol_2_4_10 = false;
            }
            else if (gameObject.name == "Patrol_2_4_11")
            {
                GlobalControl.patrol_2_4_11 = false;
            }
            else if (gameObject.name == "Patrol_2_4_12")
            {
                GlobalControl.patrol_2_4_12 = false;
            }
            else if (gameObject.name == "Patrol_2_4_13")
            {
                GlobalControl.patrol_2_4_13 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_0")
            {
                GlobalControl.aquatic_1_4_0 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_1")
            {
                GlobalControl.aquatic_1_4_1 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_2")
            {
                GlobalControl.aquatic_1_4_2 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_3")
            {
                GlobalControl.aquatic_1_4_3 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_4")
            {
                GlobalControl.aquatic_1_4_4 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_5")
            {
                GlobalControl.aquatic_1_4_5 = false;
            }
            else if (gameObject.name == "Aquatic_1_4_6")
            {
                GlobalControl.aquatic_1_4_6 = false;
            }
            else if (gameObject.name == "Subnautical")
            {
                GlobalControl.downed_boss_3 = true;
            }

            // Grey Palace
            else if (gameObject.name == "Patrol_3_5_0")
            {
                GlobalControl.patrol_3_5_0 = false;
            }
            else if (gameObject.name == "Patrol_3_5_1")
            {
                GlobalControl.patrol_3_5_1 = false;
            }
            else if (gameObject.name == "Patrol_3_5_2")
            {
                GlobalControl.patrol_3_5_2 = false;
            }
            else if (gameObject.name == "Pursuit_2_5_0")
            {
                GlobalControl.pursuit_2_5_0 = false;
            }
            else if (gameObject.name == "Aerial_2_5_0")
            {
                GlobalControl.aerial_2_5_0 = false;
            }
            else if (gameObject.name == "Aerial_2_5_1")
            {
                GlobalControl.aerial_2_5_1 = false;
            }
            else if (gameObject.name == "Aerial_2_5_2")
            {
                GlobalControl.aerial_2_5_2 = false;
            }
            else if (gameObject.name == "Aquatic_2_5_0")
            {
                GlobalControl.aquatic_2_5_0 = false;
            }
            else if (gameObject.name == "Aquatic_2_5_1")
            {
                GlobalControl.aquatic_2_5_1 = false;
            }
            else if (gameObject.name == "Turret_2_5_0")
            {
                GlobalControl.turret_2_5_0 = false;
            }
            else if (gameObject.name == "Turret_2_5_1")
            {
                GlobalControl.turret_2_5_1 = false;
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
                }
            }

            if (aiState == "" || aiState == null)
                defense = 100;

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
                            else if (direction.x > direction.y)
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
            Vector3 difference;
            float rotationZ;

            // Fixed shot
            if (aiState == "preset")
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                refState2_4 = true;
                refState2b_4 = true;

                if (canShoot)
                {
                    rotationZ = deg;

                    fireBullet(new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad)), rotationZ, bulletSpeed[0]);
                    StartCoroutine(cooldown());
                }
            }
            // Target shot
            else if (refState2_4 && Scope.inRange == gameObject.name)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];

                difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (aiState != "preset")
                {
                    // Calculate speed
                    float rSpeed = refState2a_4 / 3 * speed;
                    if (refState2a_4 > 180)
                    {
                        rSpeed = (360 - refState2a_4) / 3 * speed;
                    }

                    // Forward
                    if (refState2a_4 < 180)
                        transform.RotateAround(new Vector3(refState3b_4, refState3c_4), new Vector3(0, 0, 1), rSpeed);
                    // Backward
                    else if (refState2a_4 > 180)
                        transform.RotateAround(new Vector3(refState3b_4, refState3c_4), new Vector3(0, 0, 1), -rSpeed);
                }

                if (canShoot)
                {
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    fireBullet(direction, rotationZ, bulletSpeed[0]);
                    StartCoroutine(cooldown());
                }
            }
            // Passive
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                float localCenter = deg;

                if (localCenter <= 0)
                    localCenter = 360 + deg;

                if (pathState == "forth")
                {
                    if (localCenter + range_1 > 360)
                        localCenter -= 360;

                    transform.RotateAround(new Vector3(refState3b_4, refState3c_4), new Vector3(0, 0, 1), speed);
                    if (transform.localEulerAngles.z > localCenter + range_1 && (transform.localEulerAngles.z < 180 && localCenter + range_1 < 180 || transform.localEulerAngles.z > 180 && localCenter + range_1 > 180))
                        pathState = "back";
                }
                else if (pathState == "back")
                {
                    transform.RotateAround(new Vector3(refState3b_4, refState3c_4), new Vector3(0, 0, 1), -speed);
                    if (transform.localEulerAngles.z < localCenter - range_2 && (transform.localEulerAngles.z < 180 && localCenter - range_1 < 180 || transform.localEulerAngles.z > 180 && localCenter - range_1 > 180))
                        pathState = "forth";
                }
            }
        }

        /*-----Sentry-----*/
        else if (gameObject.name.Substring(0, 6) == "Sentry")
        {
            if (aiState == "inactive")
            {
                transform.position = new Vector3(0, 20, transform.position.z);

                // Summon conditions
                if (refState1a_3H)
                {
                    if (refState1b_3H == 0)
                    {
                        if (gameObject.name == "Sentry__")
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                    }
                    else if (refState1b_3H == 1)
                    {
                        if (gameObject.name == "Sentry__")
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                        else if (gameObject.name == "Sentry__ (1)" && refState1c_3H)
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                    }
                    else
                    {
                        if (gameObject.name == "Sentry__")
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                        else if (gameObject.name == "Sentry__ (1)" && refState1c_3H)
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                        else if (gameObject.name == "Sentry__ (2)" && refState1c_3H && refState1d_3H)
                        {
                            aiState = "active";
                            refState1b_3H++;
                            refState1a_3H = false;
                        }
                    }
                }
            }
            else if (aiState == "active")
            {
                if (!refState3_3H)
                {
                    refState3_3H = true;
                    transform.position = new Vector3(0f, 6f, transform.position.z);
                    if (gameObject.name == "Sentry__")
                        refState1c_3H = true;
                    else if (gameObject.name == "Sentry__ (1)")
                        refState1d_3H = true;
                }

                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();

                // Rotate around Player
                if (refState_3H && ((gameObject.name == "Sentry__" && Scope.in_1) || (gameObject.name == "Sentry__ (1)" && Scope.in_2) || (gameObject.name == "Sentry__ (2)" && Scope.in_3)))
                {
                    if(randNum > 0)
                        transform.RotateAround(Player.rb2D.position, new Vector3(0, 0, 1), speed_1);
                    else
                        transform.RotateAround(Player.rb2D.position, new Vector3(0, 0, 1), -speed_1);
                }
                // Follow Player
                else
                {
                    randNum = Random.Range(0, 2);
                    transform.position += new Vector3(direction.x * speed, direction.y * speed);
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
                }

                // Shoot x bullets at y rate then recharge for z seconds
                if (canShoot && currBullet > 0)
                {
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

        /*-----Electrical Line-----*/
        else if (gameObject.name.Substring(0, 8) == "Electric")
        {
            // Active
            if (aiState == "active")
            {
                damage = 5;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Animator>().enabled = true;

                if (time_f <= 0)
                {
                    time_f = refState3_1H;
                    aiState = "inactive";
                }
                else
                    time_f--;
            }
            // Inactive
            else if (aiState == "inactive")
            {
                damage = 0;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;

                if (time_f <= 0)
                {
                    time_f = 120;
                    aiState = "active";
                    GetComponent<Animator>().Play("Electrical", -1, 0.0f);
                }
                else
                    time_f--;
            }
        }

        /*-----Cutter-----*/
        else if (gameObject.name.Substring(0, 6) == "Cutter")
        {
            transform.Rotate(0, 0, 2f);

            // Determine pathing
            if (path != "")
            {
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
        }

        /*-----Gust-----*/
        else if (gameObject.name.Substring(0, 4) == "Gust")
        {
            if (refState3_2H)
                transform.Rotate(0, 0, 1.5f);
            else
                transform.Rotate(0, 0, -1.5f);
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
                    useTime *= 0.9f;
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
            if (transform.position.x < -3.95f)
            {
                transform.position = new Vector3(-3.95f, transform.position.y);
            }
            else if (transform.position.x > 3.96f)
            {
                transform.position = new Vector3(3.96f, transform.position.y);
            }

            if (transform.position.y < -2.75f)
            {
                transform.position = new Vector3(transform.position.x, -2.75f);
            }
            else if (transform.position.y > 4.1f)
            {
                transform.position = new Vector3(transform.position.x, 4.1f);
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
                time = 0;
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
                            StartCoroutine(delayRand(0f, 1, 4));
                        }
                        else if (refState3a_6 == "berserk")
                        {
                            StartCoroutine(delayRand(3f, 1, 4));
                        }
                        else
                        {
                            StartCoroutine(delayRand(1f, 1, 4));
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
                                if (!refState3g_6)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        fireBullet(new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                                    }
                                    refState3g_6 = true;
                                }                                

                                if (time_1 > 0 && refState3g_6)
                                {
                                    time_1 = 0;
                                    refState3c_6++;
                                    refState3d_6 = "stage 1";
                                    refState3g_6 = false;
                                    refState3h_6 = false;
                                }
                                else if(!refState3h_6)
                                {
                                    refState3h_6 = true;
                                    StartCoroutine(addSecond_1());
                                }
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
                else if (!hold_time)
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
                else if (!hold_time)
                {
                    StartCoroutine(addSecond());
                }
            }

            // Downpour Timer
            if (healthCurr < 100 && !refState3a_7)
            {
                refState3a_7 = true;
                if (aiState != "dive" || aiState != "surface" || aiState != "leap")
                    aiState = "downpour";
            }
            else if (refState3a_7)
            {
                if (time_1 >= 20)
                {
                    time_1 = 0;

                    float rand = Random.Range(0, 3);

                    if (rand > 0 && (aiState != "dive" || aiState != "surface" || aiState != "leap"))
                        aiState = "downpour";
                }
                else if (!hold_time_1)
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
                refState_7 = false;
                refState1b_7 = "";

                if (!hold_rand)
                {
                    StartCoroutine(delayRand(4f, 1, 2));
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
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2, transform.position.z);
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (canShoot && currBullet > -1)
                {
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();

                    int bullet;
                    if (refState3a_7)
                    {
                        bullet = 5;
                    }
                    else
                    {
                        bullet = 3;
                    }

                    for (int i = 0; i < bullet; i++)
                    {
                        if (refState3a_7)
                        {
                            if (direction.x * direction.y >= 0)
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 2))), rotationZ + 20 * (i - 2), bulletSpeed[0]);
                            else if (direction.x > direction.y)
                                fireBullet(new Vector2(Mathf.Sqrt(1 - Mathf.Pow(Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 2)), 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 9 * (i - 2))), rotationZ + 20 * (i - 2), bulletSpeed[0]);
                            else
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 2)), Mathf.Sqrt(1 - Mathf.Pow(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 9 * (i - 2)), 2))), rotationZ + 20 * (i - 2), bulletSpeed[0]);
                        }
                        else
                        {
                            if (direction.x * direction.y >= 0)
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                            else if (direction.x > direction.y)
                                fireBullet(new Vector2(Mathf.Sqrt(1 - Mathf.Pow(Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1)), 2)), Mathf.Sin(Mathf.Asin(direction.y) + Mathf.PI / 6 * (i - 1))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                            else
                                fireBullet(new Vector2(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), Mathf.Sqrt(1 - Mathf.Pow(Mathf.Cos(Mathf.Acos(direction.x) + Mathf.PI / 6 * (i - 1)), 2))), rotationZ + 30 * (i - 1), bulletSpeed[0]);
                        }
                    }
                    currBullet--;
                    StartCoroutine(cooldown());
                }
                else if (currBullet <= -1)
                {
                    aiState = "rest";
                }
            }

            // Crystal Barrage
            else if (aiState == "crystal")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2, transform.position.z);
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
                }
            }

            // Downpour
            else if (aiState == "downpour")
            {
                if (refState1b_7 == "" || refState1b_7 == null)
                {
                    if (healthCurr < 50)
                        refState1b_7 = "storming";
                    else
                        refState1b_7 = "pouring";
                }
                else if (refState1b_7 == "finish")
                    aiState = "rest";
            }
        }

        /*-----The Emperor-----*/
        else if (gameObject.name == "Emperor_")
        {
            // Health Shields
            if (healthCurr <= 100)
                refState_8 = 4;
            else if (healthCurr <= 200)
                refState_8 = 3;
            else if (healthCurr <= 300)
                refState_8 = 2;
            else if (healthCurr <= 400)
                refState_8 = 1;

            // Summon Sentry
            if ((healthCurr <= 450 && refState1d_8 == 0) || (healthCurr <= 400 && refState1d_8 == 1) || (healthCurr <= 350 && refState1d_8 == 2) || (healthCurr <= 300 && refState1d_8 == 3) || (healthCurr <= 250 && refState1d_8 == 4) || (healthCurr <= 200 && refState1d_8 == 5) || (healthCurr <= 150 && refState1d_8 == 6) || (healthCurr <= 100 && refState1d_8 == 7) || (healthCurr <= 50 && refState1d_8 == 8))
            {
                refState1d_8++;
                refState1a_3H = true;
            }

            // Rest
            if (aiState == "rest")
            {
                // Reset stuff
                refState1a_8 = 0;
                refState1b_8 = "";
                refState1c_8 = "";
                currBullet = maxBullet;
                useTime = baseUseTime;

                if (!hold_rand)
                {
                    if (refState_8 > 2)
                        StartCoroutine(delayRand(1.5f, 1, 10));
                    else if (refState_8 > 0)
                        StartCoroutine(delayRand(1.5f, 1, 4));
                    else
                        StartCoroutine(delayRand(1.5f, 1, 1));
                }

                if (refState_8 > 2)
                {
                    if (randNum < 8)
                    {
                        aiState = "cannon";
                        hold_rand = false;
                    }
                    else if (randNum < 9)
                    {
                        aiState = "gale";
                        hold_rand = false;
                    }
                    else
                    {
                        aiState = "doom";
                        hold_rand = false;
                    }
                }
                else if (refState_8 > 0)
                {
                    if (randNum < 4)
                    {
                        aiState = "cannon";
                        hold_rand = false;
                    }
                    else
                    {
                        aiState = "gale";
                        hold_rand = false;
                    }
                }
                else
                {
                    if (randNum == 1)
                    {
                        aiState = "cannon";
                        hold_rand = false;
                    }
                }

                randNum = 0;
            }

            // Cannon Fire
            else if (aiState == "cannon")
            {
                Vector3 difference = (Vector3)Player.rb2D.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (refState1a_8 == 0)
                    refState1a_8 = Random.Range(1, 4);

                // Change fire rate
                if (refState1a_8 == 2)
                    useTime = baseUseTime / 1.5f;
                else if (refState1a_8 == 3)
                    useTime = baseUseTime / 0.75f;

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
                    aiState = "rest";
                }
            }

            // Gale
            else if (aiState == "gale")
            {
                if (refState1b_8 == "")
                    refState1b_8 = "blowing";
                else if (refState1b_8 == "finish")
                    aiState = "rest";
            }

            // Judgement
            else if (aiState == "doom")
            {
                if (refState1c_8 == "" || refState1c_8 == null)
                    refState1c_8 = "warning";
                else if (refState1c_8 == "finish")
                    aiState = "rest";
            }
        }

        // Set reference state
        if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            refState_1 = aiState;
        }

        // Update hp indicator
        if (gameObject.name == "Overseer" || gameObject.name == "Containment" || gameObject.name == "Subnautical" || gameObject.name == "Emperor_")
            hp_Curr = healthCurr;
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
                else if (gameObject.name.Substring(0, 6) == "Turret")
                {
                    refState2_4 = true;
                }
            }
            else if (gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial")
            {
                healthCurr = 0;
            }
        }

        // On hit player -> damage + knockback
        else if (other.name == "Player" && !passive)
        {
            if (gameObject.name.Substring(0, 6) == "Cutter")
            {
                Player.cut = true;

                if (Player.rb2D.position.x + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.x - GetComponent<CircleCollider2D>().radius * transform.lossyScale.x * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(-2f, Player.rb2D.velocity.y);
                }
                else if (Player.rb2D.position.x - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.x + GetComponent<CircleCollider2D>().radius * transform.lossyScale.x * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(2f, Player.rb2D.velocity.y);
                }
                else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<CircleCollider2D>().radius * transform.lossyScale.y * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                }
                else if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<CircleCollider2D>().radius * transform.lossyScale.y * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                }
            }

            if (!GlobalControl.immune)
            {
                if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat" || (gameObject.name.Substring(0, 6) == "Sludge" && GlobalControl.doubleUnlocked)))
                {
                    // Damage calculation
                    if (GlobalControl.reactor == "unstable")
                    {
                        GlobalControl.healthCurr = 0;
                    }
                    else if (damage > 0)
                    {
                        GlobalControl.healthCurr -= damage;
                        GlobalControl.immune = true;

                        // Energy drain
                        if (gameObject.name.Substring(0, 8) == "Electric")
                        {
                            if (GlobalControl.energyCurr > damage)
                                GlobalControl.energyCurr -= damage;
                            else
                                GlobalControl.energyCurr = 0;
                        }
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
                    else if (gameObject.name == "Subnautical")
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
                }
            }
        }

        // On touch with outer box
        else if (other.transform.parent != null)
        {
            if (other.transform.parent.name == "Top Ceiling" || other.transform.parent.name == "Bottom Floor" || other.transform.parent.name == "Left Side" || other.transform.parent.name == "Right Side")
            {
                if (gameObject.name.Substring(0, 6) == "Sentry")
                {
                    if (randNum > 0)
                        randNum = 0;
                    else
                        randNum = 1;
                }                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // On hit player -> damage + knockback
        if (other.name == "Player" && !passive)
        {
            if (gameObject.name.Substring(0, 6) == "Cutter")
            {
                if (Player.rb2D.position.x + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.x - GetComponent<CircleCollider2D>().radius * transform.lossyScale.x * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(-2f, Player.rb2D.velocity.y);
                }
                else if (Player.rb2D.position.x - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.x + GetComponent<CircleCollider2D>().radius * transform.lossyScale.x * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(2f, Player.rb2D.velocity.y);
                }
                else if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - GetComponent<CircleCollider2D>().radius * transform.lossyScale.y * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, -2f);
                }
                else if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + GetComponent<CircleCollider2D>().radius * transform.lossyScale.y * Mathf.Sqrt(2) / 2)
                {
                    Player.rb2D.velocity = new Vector2(Player.rb2D.velocity.x, 2f);
                }
            }

            if (!GlobalControl.immune)
            {
                if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat" || (gameObject.name.Substring(0, 6) == "Sludge" && GlobalControl.doubleUnlocked)))
                {
                    // Damage calculation
                    if (GlobalControl.reactor == "unstable")
                    {
                        GlobalControl.healthCurr = 0;
                    }
                    else if (damage > 0)
                    {
                        GlobalControl.healthCurr -= damage;
                        GlobalControl.immune = true;

                        // Energy drain
                        if (gameObject.name.Substring(0, 8) == "Electric")
                        {
                            if (GlobalControl.energyCurr > damage)
                                GlobalControl.energyCurr -= damage;
                            else
                                GlobalControl.energyCurr = 0;
                        }
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
                    else if (gameObject.name == "Subnautical")
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
                }
            }
        }

        // Effects
        if (other.name == "Player" && (gameObject.name.Substring(0, 5) == "Water" || gameObject.name.Substring(0, 6) == "Sludge"))
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
        if (other.name == "Player" && !passive)
        {
            if (gameObject.name.Substring(0, 6) == "Cutter")
                Player.cut = false;
        }
    }

    void fireBullet(Vector2 direction, float rotation2, float speed)
    {
        // Fires a bullet from the pool
        GameObject bullet = null;

        // Single bullet type
        if (gameObject.name.Substring(0, 6) == "Aerial" || gameObject.name.Substring(0, 6) == "Turret" || gameObject.name == "Containment")
        {
            bullet = EnemyObjectPooler.SharedInstance.GetPooledObject();
        }
        else if (gameObject.name.Substring(0, 6) == "Sentry")
        {
            bullet = EnemyObjectPooler5.SharedInstance.GetPooledObject();
        }
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            if (GlobalControl.area == "GP_14")
                bullet = EnemyObjectPooler2.SharedInstance.GetPooledObject();
            else
                bullet = EnemyObjectPooler.SharedInstance.GetPooledObject();
        }

        // Overseer
        else if (gameObject.name == "Overseer")
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
        // Subnautical
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
        // Emperor
        else if (gameObject.name == "Emperor_")
        {
            if (refState1a_8 == 1)
                bullet = EnemyObjectPooler.SharedInstance.GetPooledObject();
            else if (refState1a_8 == 2)
                bullet = EnemyObjectPooler2.SharedInstance.GetPooledObject();
            else if (refState1a_8 == 3)
                bullet = EnemyObjectPooler3.SharedInstance.GetPooledObject();
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
            else if(gameObject.name.Substring(0, 6) == "Turret")
            {
                bullet.transform.position = new Vector3(transform.position.x + GetComponent<BoxCollider2D>().size.x * Mathf.Cos(rotation2 * Mathf.Deg2Rad) / 2, transform.position.y + GetComponent<BoxCollider2D>().size.y * Mathf.Sin(rotation2 * Mathf.Deg2Rad), transform.position.z + 1);
            }
            else if (gameObject.name == "Overseer")
            {
                bullet.transform.position = new Vector3(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, transform.position.z);
            }
            else if (gameObject.name == "Subnautical")
            {
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2, transform.position.z);
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
