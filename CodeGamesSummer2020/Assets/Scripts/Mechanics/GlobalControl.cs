using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour 
{
    // Endings
    public static bool ending_1;                    // Save Humanity
    public static bool ending_2;                    // Return to the Past
    public static bool ending_3;                    // End the Cycle
    public static bool complete;                    // Get all endings
    public static bool triggerOnce;

    // Energy
    public static int energyMax = 10;               // max energy level
    public static int energyCurr;                   // current energy level
    public static int energyUse;                    // amount of energy used per shot

    // Health
    public static int healthMax = 10;               // max health level
    public static int healthCurr;                   // current health level

    // Other Stats
    public static int damage;                       // player bullet damage
    public static int data;                         // Percent data collected
    public static bool immune;                      // Immune to damage (after taking damage)
    public static bool immune_;                     // Immune update once per time
    private float time_i;                           // Timer to check immunity bug

    // Unlock 
    public static bool batteryUnlocked;             // Battery
    public static bool solarUnlocked;               // Solar Panel
    public static bool geoUnlocked;                 // Geothermal Extractor

    public static bool gunUnlocked;                 // Energy Cannon
    public static bool mapUnlocked;                 // Navigational Module
    public static bool heartlessUnlocked;           // Heartless Generator
    public static bool keyUnlocked;                 // Access Key

    public static bool dashUnlocked;                // Booster Rocket
    public static bool clingUnlocked;               // Climbing Claws
    public static bool doubleUnlocked;              // Booster Rocket MK2

    public static bool basicUnlocked = true;        // Basic Reactor
    public static bool imperialUnlocked;            // Strange Reactor
    public static bool familiarUnlocked;            // Lost Reactor
    public static bool unstableUnlocked;            // Unstable Reactor

    public static bool scrapFound;                  // Hyper Scrap
    public static bool extraFound;                  // Extra Battery
    public static bool plateFound;                  // Special Plating

    public static bool extra_1;                     // Extra battery pick-ups
    public static bool extra_2;
    public static bool extra_3;

    public static bool plating_1;                   // Special Plating pick-ups
    public static bool plating_2;

    public static bool scrap_1;                     // Hyper Scrap pick-ups
    public static bool scrap_2;
    public static bool scrap_3;

    // Inventory Numbers
    public static string menu = "help";             // Which menu the player was in
    public static string map = "local";             // Map mode
    public static int plateNum = 0;                 // Number of special plating obtained
    public static int extraNum = 0;                 // Number of extra batteries obtained
    public static int scrapNum = 0;                 // Number of hyper scraps in possession

    // Toggle
    public static string reactor = "basic";         // Name of equipped reactor
    public static bool h2e = true;                  // True = HP to Energy, False = Energy to HP
    public static int prog = 3;                     // Progession level for unlockAll: Start(0), Post-Start(1), Post-Dreg(2), Post-Garden(3), Post-Second(4), Post-Town(5), Post-Third(6), Post-Return(7), Post-End(8)

    // Vacuum Pod
    public static string pod_direction = "right";   // Direction of pod
    public static string pod_location = "main";     // Where is the pod

    // Lift from Hell
    public static string lift_direction = "up";     // Direction of the lift

    // Eye of the Storm
    public static bool calm;

    // World
    public static float pX;                         // Player x-coord before opening menu
    public static float pY;                         // Player y-coord before opening menu

    public static int humansLeft = 6;               // How many humans left to capture (6: May-October)
    public static int bossDowned = 0;               // How many bosses have been defeated
    public static string fate;                      // Ending 2 or 3
    public static bool canContinue;                 // Continue from Main Menu?

    public static int update = 0;                   // Update stats when power-up obtained
    public static bool switched;                    // Used to set position on scene switch

    public static string area = "";                 // Area name for scene change purposes
    public static string prevArea = "";             // Name of previous area
    public static string checkpoint = "";           // Area name of last repair station used

    public static bool sg;                          // World map update
    public static bool tt;
    public static bool mb;
    public static bool it;
    public static bool gp;

    // Dialogue
    public static int counter_1 = 0;                // Counter for First dialogue
    public static bool masterControl;               // Using Master Control

    // Enemy Catalog
    public static bool downed_patrol;
    public static bool downed_pursuit;
    public static bool downed_aerial;
    public static bool downed_aquatic;
    public static bool downed_turret;

    public static bool found_errat;

    public static bool downed_boss_1;
    public static bool downed_boss_2;
    public static bool downed_boss_3;
    public static bool downed_boss_4;

    // Reports
    public static bool report_1;
    public static bool report_2;
    public static bool report_3;
    public static bool report_4;
    public static bool report_5;
    public static bool report_6;
    public static bool report_7;
    public static bool report_8;
    public static bool report_9;
    public static bool report_10;


    /*---------------------Wall of Text Starts---------------------*/

    // Doors
    public static string nextDoor = "";             // The door on the other side, from which the player will exit from

    public static bool locked_1 = true;             // Birthplace-Start Door
    public static bool locked_2;                    // The Lift to Heaven Door
    public static bool locked_3 = true;             // DH_4_to_DH_6
    public static bool locked_4 = true;             // SG_10_to_SG_3
    public static bool locked_5 = true;             // MB_3_to_MB_12
    public static bool locked_6 = true;             // GP_0A_to_GP_10

    // Switches
    public static string state_SG_8 = "active";
    public static string state_SG_10 = "active";
    public static string state_SG_11 = "active";
    public static string state_SG_11S = "active";
    public static string state_SG_11S_ = "active";
    public static string state_TT_11 = "active";
    public static string state_MB_4 = "active";
    public static string state_MB_7 = "active";
    public static string state_MB_8 = "active";
    public static string state_MB_11 = "active";
    public static string state_IT_4 = "active";
    public static string state_IT_6 = "active";
    public static string state_IT_9 = "active";
    public static string state_GP_4 = "active";
    public static string state_GP_10 = "active";

    // Destructibles
    public static bool block_starter = true;
    public static bool secret_unstable = true;

    public static bool block_DH_4 = true;
    public static bool secret_DH_5 = true;

    public static bool block_SG_9 = true;
    public static bool block_SG_11 = true;
    public static bool block_SG_11S = true;
    public static bool block_SG_11S_ = true;
    public static bool block_SG_12 = true;
    public static bool secret_SG_9 = true;

    public static bool block_TT_2 = true;
    public static bool block_TT_6 = true;
    public static bool block_TT_9 = true;
    public static bool block_TT_11 = true;
    public static bool block_TT_12 = true;
    public static bool block_TT_14S = true;

    public static bool secret_MB_3 = true;

    public static bool block_IT_4 = true;
    public static bool block_IT_4_ = true;
    public static bool block_IT_6 = true;
    public static bool block_IT_9 = true;

    public static bool block_GP_4 = true;
    public static bool block_GP_10 = true;

    // Enemy State

    // type_tier_area_num
    public static bool patrol_1_0_0 = true;        // Testing Area
    public static bool patrol_1_0_1 = true;
    public static bool patrol_1_0_2 = true;

    public static bool patrol_1_1_0 = true;        // Institute of Technology
    public static bool patrol_3_1_0 = true;
    public static bool patrol_3_1_1 = true;
    public static bool patrol_3_1_2 = true;
    public static bool turret_1_1_0 = true;
    public static bool turret_1_1_1 = true;
    public static bool turret_1_1_2 = true;
    public static bool turret_1_1_3 = true;
    public static bool turret_1_1_4 = true;
    public static bool turret_1_1_5 = true;
    public static bool turret_1_1_6 = true;

    public static bool errat_0 = true;             // Dreg Heap
    public static bool errat_1 = true;
    public static bool errat_2 = true;
    public static bool errat_3 = true;
    public static bool errat_4 = true;
    public static bool errat_5 = true;

    public static bool patrol_1_2_0 = true;        // Sunset Garden
    public static bool patrol_1_2_1 = true;
    public static bool patrol_1_2_2 = true;
    public static bool patrol_1_2_3 = true;
    public static bool patrol_1_2_4 = true;
    public static bool patrol_1_2_5 = true;
    public static bool patrol_1_2_6 = true;
    public static bool patrol_1_2_7 = true;
    public static bool patrol_1_2_8 = true;
    public static bool patrol_1_2_9 = true;
    public static bool pursuit_1_2_0 = true;
    public static bool pursuit_1_2_1 = true;
    public static bool pursuit_1_2_2 = true;
    public static bool pursuit_1_2_3 = true;
    public static bool pursuit_1_2_4 = true;
    public static bool pursuit_1_2_5 = true;
    public static bool pursuit_1_2_6 = true;
    public static bool pursuit_1_2_7 = true;
    public static bool pursuit_1_2_8 = true;

    public static bool patrol_2_3_0 = true;        // Twilight Town
    public static bool patrol_2_3_1 = true;
    public static bool patrol_2_3_2 = true;
    public static bool patrol_2_3_3 = true;
    public static bool patrol_2_3_4 = true;
    public static bool patrol_2_3_5 = true;
    public static bool patrol_2_3_6 = true;
    public static bool patrol_2_3_7 = true;
    public static bool patrol_2_3_8 = true;
    public static bool patrol_2_3_9 = true;
    public static bool patrol_2_3_10 = true;
    public static bool aerial_1_3_0 = true;
    public static bool aerial_1_3_1 = true;
    public static bool aerial_1_3_2 = true;
    public static bool aerial_1_3_3 = true;
    public static bool aerial_1_3_4 = true;
    public static bool aerial_1_3_5 = true;
    public static bool aerial_1_3_6 = true;
    public static bool aerial_1_3_7 = true;
    public static bool aerial_1_3_8 = true;
    public static bool aerial_1_3_9 = true;
    public static bool aerial_1_3_10 = true;

    public static bool patrol_2_4_0 = true;        // Midnight Bay
    public static bool patrol_2_4_1 = true;
    public static bool patrol_2_4_2 = true;
    public static bool patrol_2_4_3 = true;
    public static bool patrol_2_4_4 = true;
    public static bool patrol_2_4_5 = true;
    public static bool patrol_2_4_6 = true;
    public static bool patrol_2_4_7 = true;
    public static bool patrol_2_4_8 = true;
    public static bool patrol_2_4_9 = true;
    public static bool patrol_2_4_10 = true;
    public static bool patrol_2_4_11 = true;
    public static bool patrol_2_4_12 = true;
    public static bool patrol_2_4_13 = true;
    public static bool aquatic_1_4_0 = true;
    public static bool aquatic_1_4_1 = true;
    public static bool aquatic_1_4_2 = true;
    public static bool aquatic_1_4_3 = true;
    public static bool aquatic_1_4_4 = true;
    public static bool aquatic_1_4_5 = true;
    public static bool aquatic_1_4_6 = true;

    public static bool patrol_3_5_0 = true;         // Grey Palace
    public static bool patrol_3_5_1 = true;
    public static bool patrol_3_5_2 = true;
    public static bool pursuit_2_5_0 = true;
    public static bool aerial_2_5_0 = true;
    public static bool aerial_2_5_1 = true;
    public static bool aerial_2_5_2 = true;
    public static bool aquatic_2_5_0 = true;
    public static bool aquatic_2_5_1 = true;
    public static bool turret_2_5_0 = true;
    public static bool turret_2_5_1 = true;

    /*---------------------Wall of Text Ends---------------------*/

    public static GlobalControl Instance;
    private float t = 0;

    private void Awake()
    {
        if (Instance == null)
        { // Make one global instance
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        { // Ensure there is only one instance
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        unlockAll();
        StartCoroutine(setAreaName());

        // Set player stats
        healthCurr = healthMax;
        energyCurr = energyMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Update stats
        if (update == 1)
        {
            update = 0;
            healthMax = 10 + plateNum * 20;
            healthCurr = healthMax;
        }
        else if (update == 2)
        {
            update = 0;
            energyMax = 10 + extraNum * 10;
            energyCurr = energyMax;
        }
        // I-Frames
        if (immune && immune_)
        {
            immune_ = false;
            StartCoroutine(notImmune(1f));
        }
        else if (!immune)
        {
            immune_ = true;
        }

        if (!immune_)
        {
            time_i++;
            if (time_i > 200)
                immune = false;
        }
        else
            time_i = 0;

        // De-aggro
        if (switched)
        {
            Obstacles.refState2_4 = false;
        }

        // Platinum Trophy
        if (ending_1 && ending_2 && ending_3)
        {
            complete = true;
        }

        // Endings
        if (humansLeft == 0)
        { // Save Humanity
            humansLeft = 6;
            StartCoroutine(SceneSwitch("Ending_1", ""));
        }
        else if (fate == "free")
        {
            fate = "";
            StartCoroutine(SceneSwitch("Ending_2", ""));
        }
        else if (fate == "kill")
        {
            fate = "";
            StartCoroutine(SceneSwitch("Ending_3", ""));
        }

        // Return to Main Menu
        if (area.Substring(0, 3) == "End" && !triggerOnce)
        {
            triggerOnce = true;
            canContinue = false;
            if (area == "Ending_1")
            {
                ending_1 = true;
                StartCoroutine(delayedSwitch(4.5f, SceneSwitch("Main Menu", "")));
            }
            else if (area == "Ending_2")
            {
                ending_2 = true;
                StartCoroutine(delayedSwitch(4f, SceneSwitch("Main Menu", "")));
            }
            else if (area == "Ending_3")
            {
                ending_3 = true;
                StartCoroutine(delayedSwitch(2.6f, SceneSwitch("Main Menu", "")));
            }
        }

        if (area == "Main Menu")
        {
            triggerOnce = false;
        }

        // Reactors
        if (reactor == "basic")
        {
            energyUse = 1;
            damage = 1 + bossDowned;
        }
        else if (reactor == "imperial")
        {
            energyUse = 1;
            damage = 0;
        }
        else if (reactor == "familiar")
        {
            energyUse = 2;
            damage = 0 + data / 10;
        }
        else if (reactor == "unstable")
        {
            energyUse = 1;
            damage = 10;
        }

        // Respawning
        if (healthCurr <= 0)
        {
            immune = false;
            Player.canDash = true;

            // Testing Area 2
            if (checkpoint == "Rest_Test")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", checkpoint));
            }

            // IT - Homecoming
            else if (checkpoint == "Checkpoint_1")
            {
                StartCoroutine(SceneSwitch("IT_1", checkpoint));
            }

            // DH - Death Basin
            else if (checkpoint == "Checkpoint_2")
            {
                StartCoroutine(SceneSwitch("DH_2", checkpoint));
            }

            // SG - Back End
            else if (checkpoint == "Checkpoint_3")
            {
                StartCoroutine(SceneSwitch("SG_10", checkpoint));
            }

            // TT - Central Plaza
            else if (checkpoint == "Checkpoint_4")
            {
                StartCoroutine(SceneSwitch("TT_2", checkpoint));
            }

            // MB - Hull
            else if (checkpoint == "Checkpoint_5")
            {
                StartCoroutine(SceneSwitch("MB_3", checkpoint));
            }

            // GP - Final Rest
            else if (checkpoint == "Checkpoint_6")
            {
                StartCoroutine(SceneSwitch("GP_10", checkpoint));
            }

            // No checkpoints used
            else
            {
                if (counter_1 > 0)
                {
                    StartCoroutine(SceneSwitch("DH_2", "Checkpoint_2"));
                }
                else
                {
                    StartCoroutine(SceneSwitch("Start_", ""));
                }
            }

            // Full restore
            healthCurr = healthMax;
            energyCurr = energyMax;

            // Respawn all enemies
            respawnAll();
        }
    }

    public static void resetPlayer()
    {
        // Ending Flag
        triggerOnce = false;

        // Player Stats
        healthMax = 10;
        energyMax = 10;
        healthCurr = healthMax;
        energyCurr = energyMax;
        data = 0;

        // Inventory
        batteryUnlocked = false;
        solarUnlocked = false;
        geoUnlocked = false;
        heartlessUnlocked = false;
        mapUnlocked = false;
        dashUnlocked = false;
        clingUnlocked = false;
        doubleUnlocked = false;
        keyUnlocked = false;
        gunUnlocked = false;
        imperialUnlocked = false;
        familiarUnlocked = false;
        unstableUnlocked = false;

        plateFound = false;
        plating_1 = false;
        plating_2 = false;
        plateNum = 0;

        extraFound = false;
        extra_1 = false;
        extra_2 = false;
        extra_3 = false;
        extraNum = 0;

        scrapFound = false;
        scrap_1 = false;
        scrap_2 = false;
        scrap_3 = false;
        scrapNum = 0;

        // Map
        sg = false;
        tt = false;
        mb = false;
        it = false;
        gp = false;

        // Enemy Logs
        downed_patrol = false;
        downed_pursuit = false;
        downed_aerial = false;
        downed_aquatic = false;
        downed_boss_1 = false;
        downed_boss_2 = false;
        downed_boss_3 = false;
        downed_boss_4 = false;
        found_errat = false;
       
        // Ego Reports
        report_1 = false;
        report_2 = false;
        report_3 = false;
        report_4 = false;
        report_5 = false;
        report_6 = false;
        report_7 = false;
        report_8 = false;
        report_9 = false;
        report_10 = false;

        // Reset non-respawning enemies
        errat_0 = true;
        errat_1 = true;
        errat_2 = true;
        errat_3 = true;
        errat_4 = true;
        errat_5 = true;

        bossDowned = 0;

        // Default Settings
        h2e = true;
        menu = "inventory";
        map = "local";
        reactor = "basic";
        prevArea = "";
        checkpoint = "";
        counter_1 = 0;
    }

    public static void resetObjects()
    {
        // Doors
        locked_1 = true;
        locked_2 = false;
        locked_3 = true;
        locked_4 = true;
        locked_5 = true;
        locked_6 = true;

        // Destructibles
        block_starter = true;
        secret_unstable = true;        

        block_DH_4 = true;
        secret_DH_5 = true;

        block_SG_9 = true;
        block_SG_11 = true;
        block_SG_11S = true;
        block_SG_11S_ = true;
        block_SG_12 = true;
        secret_SG_9 = true;

        block_TT_2 = true;
        block_TT_6 = true;
        block_TT_9 = true;
        block_TT_11 = true;
        block_TT_12 = true;
        block_TT_14S = true;

        secret_MB_3 = true;

        block_IT_4 = true;
        block_IT_4_ = true;
        block_IT_6 = true;
        block_IT_9 = true;

        block_GP_4 = true;
        block_GP_10 = true;

        // Switches
        state_SG_8 = "active";
        state_SG_10 = "active";
        state_SG_11 = "active";
        state_SG_11S = "active";
        state_SG_11S_ = "active";
        state_TT_11 = "active";
        state_MB_4 = "active";
        state_MB_7 = "active";
        state_MB_8 = "active";
        state_MB_11 = "active";
        state_IT_4 = "active";
        state_IT_6 = "active";
        state_IT_9 = "active";
        state_GP_4 = "active";
        state_GP_10 = "active";

        // Vac Pod
        pod_location = "main";
        pod_direction = "right";

        // Lift
        lift_direction = "up";
    }

    public static void respawnAll()
    {
        patrol_1_0_0 = true;
        patrol_1_0_1 = true;
        patrol_1_0_2 = true;

        patrol_1_1_0 = true;
        patrol_3_1_0 = true;
        patrol_3_1_1 = true;
        patrol_3_1_2 = true;
        turret_1_1_0 = true;
        turret_1_1_1 = true;
        turret_1_1_2 = true;
        turret_1_1_3 = true;
        turret_1_1_4 = true;
        turret_1_1_5 = true;
        turret_1_1_6 = true;

        patrol_1_2_0 = true;
        patrol_1_2_1 = true;
        patrol_1_2_2 = true;
        patrol_1_2_3 = true;
        patrol_1_2_4 = true;
        patrol_1_2_5 = true;
        patrol_1_2_6 = true;
        patrol_1_2_7 = true;
        patrol_1_2_8 = true;
        patrol_1_2_9 = true;
        pursuit_1_2_0 = true;
        pursuit_1_2_1 = true;
        pursuit_1_2_2 = true;
        pursuit_1_2_3 = true;
        pursuit_1_2_4 = true;
        pursuit_1_2_5 = true;
        pursuit_1_2_6 = true;
        pursuit_1_2_7 = true;
        pursuit_1_2_8 = true;

        patrol_2_3_0 = true;
        patrol_2_3_1 = true;
        patrol_2_3_2 = true;
        patrol_2_3_3 = true;
        patrol_2_3_4 = true;
        patrol_2_3_5 = true;
        patrol_2_3_6 = true;
        patrol_2_3_7 = true;
        patrol_2_3_8 = true;
        patrol_2_3_9 = true;
        patrol_2_3_10 = true;
        aerial_1_3_0 = true;
        aerial_1_3_1 = true;
        aerial_1_3_2 = true;
        aerial_1_3_3 = true;
        aerial_1_3_4 = true;
        aerial_1_3_5 = true;
        aerial_1_3_6 = true;
        aerial_1_3_7 = true;
        aerial_1_3_8 = true;
        aerial_1_3_9 = true;
        aerial_1_3_10 = true;

        patrol_2_4_0 = true;
        patrol_2_4_1 = true;
        patrol_2_4_2 = true;
        patrol_2_4_3 = true;
        patrol_2_4_4 = true;
        patrol_2_4_5 = true;
        patrol_2_4_6 = true;
        patrol_2_4_7 = true;
        patrol_2_4_8 = true;
        patrol_2_4_9 = true;
        patrol_2_4_10 = true;
        patrol_2_4_11 = true;
        patrol_2_4_12 = true;
        patrol_2_4_13 = true;
        aquatic_1_4_0 = true;
        aquatic_1_4_1 = true;
        aquatic_1_4_2 = true;
        aquatic_1_4_3 = true;
        aquatic_1_4_4 = true;
        aquatic_1_4_5 = true;
        aquatic_1_4_6 = true;

        patrol_3_5_0 = true;
        patrol_3_5_1 = true;
        patrol_3_5_2 = true;
        pursuit_2_5_0 = true;
        aerial_2_5_0 = true;
        aerial_2_5_1 = true;
        aerial_2_5_2 = true;
        aquatic_2_5_0 = true;
        aquatic_2_5_1 = true;
        turret_2_5_0 = true;
        turret_2_5_1 = true;
    }

    public static void unlockAll()
    {       
        if (prog > 0)
        { // Post-Start
            batteryUnlocked = true;
            solarUnlocked = true;
            gunUnlocked = true;
            unstableUnlocked = true;

            downed_patrol = true;

            if (prog > 1)
            { // Post-Dreg
                heartlessUnlocked = true;
                mapUnlocked = true;
                familiarUnlocked = true;

                counter_1 = 1;

                report_1 = true;
                data = 10;

                if (prog > 2)
                { // Post-Garden
                    dashUnlocked = true;
                    geoUnlocked = true;
                    sg = true;

                    downed_boss_1 = true;
                    scrapFound = true;
                    scrap_1 = true;
                    bossDowned = 1;

                    downed_pursuit = true;
                    counter_1 = 6;

                    plateFound = true;
                    extraFound = true;
                    plating_1 = true;
                    extra_1 = true;
                    plateNum = 1;
                    extraNum = 1;
                    healthMax = 30;
                    energyMax = 20;

                    if (prog > 3)
                    { // Post-Second
                        clingUnlocked = true;

                        downed_boss_2 = true;
                        downed_boss_3 = true;
                        scrap_2 = true;
                        bossDowned = 2;

                        extra_2 = true;
                        extraNum = 2;
                        energyMax = 30;

                        report_4 = true;
                        report_7 = true;
                        data = 30;

                        if (prog > 4)
                        { // Post-Town
                            keyUnlocked = true;
                            tt = true;

                            downed_aerial = true;

                            report_5 = true;
                            data = 40;

                            if (prog > 5)
                            { // Post-Third
                                doubleUnlocked = true;
                                mb = true;

                                scrap_3 = true;
                                bossDowned = 3;

                                downed_aquatic = true;

                                extra_3 = true;
                                extraNum = 3;
                                energyMax = 40;

                                report_2 = true;
                                report_3 = true;
                                report_6 = true;
                                data = 70;

                                if (prog > 6)
                                { // Post-Return
                                    it = true;

                                    downed_turret = true;

                                    plating_2 = true;
                                    plateNum = 2;
                                    healthMax = 50;

                                    report_8 = true;
                                    data = 80;

                                    if (prog > 7)
                                    { // Post-End
                                        gp = true;

                                        downed_boss_4 = true;

                                        report_9 = true;
                                        report_10 = true;
                                        data = 100;

                                        // Alternate Timeline
                                        found_errat = true;

                                        // Endings
                                        ending_1 = true;
                                        ending_2 = true;
                                        ending_3 = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator SceneSwitch(string load, string checkpoint)
    {
        string prev = area;
        nextDoor = checkpoint;
        area = load;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        if (prev.Substring(0, 3) == "End")
        {
            MainMenuBtn.confirmNew = false;
            Cursor.visible = true;
        }
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    IEnumerator delayedSwitch(float time, IEnumerator action)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(action);
    }

    IEnumerator notImmune(float time)
    {
        yield return new WaitForSeconds(time);
        immune = false;
    }

    IEnumerator check_fps()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(t);
    }

    IEnumerator setAreaName()
    {
        area = SceneManager.GetActiveScene().name;
        yield return null;
    }
}
