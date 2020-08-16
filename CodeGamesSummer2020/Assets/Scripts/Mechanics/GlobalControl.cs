using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour 
{
    // Endings
    public static bool ending_1 = false;           // Save Humanity
    public static bool ending_2 = false;           // Return to the Past
    public static bool ending_3 = false;           // End the Cycle
    public static bool complete = false;           // Get all endings
    public static bool triggerOnce = false;

    // Energy
    public static int energyMax = 10;              // max energy level
    public static int energyCurr = energyMax;      // current energy level
    public static int energyUse;                   // amount of energy used per shot

    // Health
    public static int healthMax = 10;              // max health level
    public static int healthCurr = healthMax;      // current health level

    // Other Stats
    public static int damage;                      // player bullet damage
    public static int data;                        // Percent data collected
    public static bool immune;                     // Immune to damage (after taking damage)
    public static bool immune_ = false;                    // Immune update once per time

    // Unlock 
    public static bool batteryUnlocked = false;    // Battery
    public static bool solarUnlocked = false;      // Solar Panel
    public static bool geoUnlocked = false;        // Geothermal Extractor

    public static bool gunUnlocked = false;        // Energy Cannon
    public static bool mapUnlocked = false;        // Navigational Module
    public static bool heartlessUnlocked = false;  // Heartless Generator
    public static bool keyUnlocked = false;        // Access Key

    public static bool dashUnlocked = false;       // Booster Rocket
    public static bool clingUnlocked = false;      // Climbing Claws
    public static bool doubleUnlocked = false;     // Booster Rocket MK2

    public static bool basicUnlocked = true;       // Basic Reactor
    public static bool imperialUnlocked = false;   // Strange Reactor
    public static bool familiarUnlocked = false;   // Lost Reactor
    public static bool unstableUnlocked = false;   // Unstable Reactor

    public static bool scrapFound = false;         // Hyper Scrap
    public static bool extraFound = false;         // Extra Battery
    public static bool plateFound = false;         // Special Plating

    public static bool extra_1 = false;            // Extra battery pick-ups
    public static bool extra_2 = false;
    public static bool extra_3 = false;

    public static bool plating_1 = false;          // Special Plating pick-ups
    public static bool plating_2 = false;

    public static bool scrap_1 = false;            // Hyper Scrap pick-ups
    public static bool scrap_2 = false;
    public static bool scrap_3 = false;

    // Inventory Numbers
    public static string menu = "help";            // Which menu the player was in
    public static int plateNum = 0;                // Number of special plating obtained
    public static int extraNum = 0;                // Number of extra batteries obtained
    public static int scrapNum = 0;                // Number of hyper scraps in possession

    // Toggle
    public static string reactor = "basic";        // Name of equipped reactor
    public static bool h2e = true;                 // True = HP to Energy, False = Energy to HP
    public static int prog = 3;                    // Progession level for unlockAll: Start(0), Post-Start(1), Post-Dreg(2), Post-Garden(3), Post-Second(4), Post-Town(5), Post-Third(6), Post-Return(7), Post-End(8)

    // Vacuum Pod
    public static string pod_direction = "right";  // Direction of pod
    public static string pod_location = "main";    // Where is the pod

    // World
    public static int humansLeft = 6;              // How many humans left to capture (6: May-October)
    public static int bossDowned = 0;              // How many bosses have been defeated
    public static int update = 0;                  // Update stats when power-up obtained
    public static bool switched = false;           // Used to set position on scene switch

    public static string area = "";                // Area name for scene change purposes
    public static string prevArea = "";            // Name of previous area
    public static string checkpoint = "";          // Area name of last repair station used

    // Dialogue
    public static int counter_1 = 0;               // Counter for First dialogue

    // Enemy Catalog
    public static bool downed_patrol = false;
    public static bool downed_pursuit = false;
    public static bool downed_aerial = false;
    public static bool downed_aquatic = false;
    public static bool downed_turret = false;

    public static bool found_errat = false;

    public static bool downed_boss_1 = false;
    public static bool downed_boss_2 = false;
    public static bool downed_boss_3 = false;
    public static bool downed_boss_4 = false;

    // Reports
    public static bool report_1 = false;
    public static bool report_2 = false;
    public static bool report_3 = false;
    public static bool report_4 = false;
    public static bool report_5 = false;
    public static bool report_6 = false;
    public static bool report_7 = false;
    public static bool report_8 = false;
    public static bool report_9 = false;
    public static bool report_10 = false;


    /*---------------------Wall of Text Starts---------------------*/

    // Doors
    public static string nextDoor = "";            // The door on the other side, from which the player will exit from

    public static bool locked_1 = true;            // Birthplace-Start Door
    public static bool locked_2 = false;           // The Lift to Heaven Door
    public static bool locked_3 = true;            // DH_4_to_DH_6
    public static bool locked_4 = true;            // SG_10_to_SG_3

    // Switches
    public static string state_SG_8 = "active";
    public static string state_SG_10 = "active";
    public static string state_SG_11 = "active";
    public static string state_TT_11 = "active";

    // Destructibles
    public static bool block_starter = true;
    public static bool secret_unstable = true;

    public static bool block_DH_4 = true;
    public static bool secret_DH_5 = true;

    public static bool block_SG_9 = true;
    public static bool block_SG_11 = true;
    public static bool block_SG_12 = true;
    public static bool secret_SG_9 = true;

    public static bool block_TT_2 = true;
    public static bool block_TT_6 = true;
    public static bool block_TT_9 = true;
    public static bool block_TT_11 = true;
    public static bool block_TT_12 = true;
    public static bool secret_TT_6 = true;

    public static bool block_GP_1 = true;

    // Enemy State

    // type_tier_area_num
    public static bool patrol_1_0_0 = true;        // Testing Area
    public static bool patrol_1_0_1 = true;
    public static bool patrol_1_0_2 = true;

    public static bool patrol_1_1_0 = true;        // Institute of Technology

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
    public static bool aerial_1_3_0 = true;
    public static bool aerial_1_3_1 = true;
    public static bool aerial_1_3_2 = true;
    public static bool aerial_1_3_3 = true;
    public static bool aerial_1_3_4 = true;
    public static bool aerial_1_3_5 = true;
    public static bool aerial_1_3_6 = true;
    public static bool aerial_1_3_7 = true;
    public static bool aerial_1_3_8 = true;


    /*---------------------Wall of Text Ends---------------------*/

    public static GlobalControl Instance;

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

        // Platinum Trophy
        if (ending_1 && ending_2 && ending_3)
        {
            complete = true;
        }

        // Endings
        if (humansLeft == 0)
        { // Save Humanity
            humansLeft--;
            StartCoroutine(SceneSwitch("Ending_1", ""));
        }

        // Return to Main Menu
        if (area == "Ending_1" && !triggerOnce)
        {
            triggerOnce = true;
            StartCoroutine(delayedSwitch(8.5f, SceneSwitch("Main Menu", "")));
            ending_1 = true;
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

    public static void resetObjects()
    {
        // Doors
        locked_1 = true;
        locked_2 = false;
        locked_3 = true;

        // Destructibles
        block_starter = true;
        secret_unstable = true;

        block_DH_4 = true;
        secret_DH_5 = true;

        block_SG_9 = true;
        block_SG_11 = true;
        block_SG_12 = true;
        secret_SG_9 = true;

        block_TT_2 = true;
        block_TT_6 = true;
        block_TT_9 = true;
        block_TT_11 = true;
        block_TT_12 = true;
        secret_TT_6 = true;

        block_GP_1 = true;

        // Switches
        state_SG_8 = "active";
        state_SG_10 = "active";
        state_SG_11 = "active";

        // Vac Pod
        pod_location = "main";
        pod_direction = "right";
    }


    public static void respawnAll()
    {
        patrol_1_0_0 = true;
        patrol_1_0_1 = true;
        patrol_1_0_2 = true;

        patrol_1_1_0 = true;

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
        aerial_1_3_0 = true;
        aerial_1_3_1 = true;
        aerial_1_3_2 = true;
        aerial_1_3_3 = true;
        aerial_1_3_4 = true;
        aerial_1_3_5 = true;
        aerial_1_3_6 = true;
        aerial_1_3_7 = true;
        aerial_1_3_8 = true;
    }

    public static void unlockAll()
    {
        // Endings
        ending_1 = true;
        ending_2 = true;
        ending_3 = true;

        if (prog > 0)
        { // Post-Start
            batteryUnlocked = true;
            solarUnlocked = true;
            gunUnlocked = true;
            unstableUnlocked = true;

            if (prog > 1)
            { // Post-Dreg
                heartlessUnlocked = true;
                mapUnlocked = true;
                familiarUnlocked = true;

                if (prog > 2)
                { // Post-Garden
                    dashUnlocked = true;
                    geoUnlocked = true;
                    extraNum = 1;
                    plateNum = 1;

                    if (prog > 3)
                    { // Post-Second
                        clingUnlocked = true;
                        extraNum = 2;

                        if (prog > 4)
                        { // Post-Town
                            keyUnlocked = true;

                            if (prog > 5)
                            { // Post-Third
                                doubleUnlocked = true;
                                extraNum = 3;

                                if (prog > 6)
                                { // Post-Return
                                    plateNum = 2;
                                }
                            }
                        }
                    }
                }
            }
        }
        // Inventory
        plateFound = true;
        extraFound = true;         
        scrapFound = true;
        plating_1 = true;
        plating_2 = true;
        extra_1 = true;
        extra_2 = true;
        extra_3 = true;
        scrap_1 = true;
        scrap_2 = true;
        scrap_3 = true;

        // Enemy Catalog
        counter_1 = 1;
        downed_patrol = true;
        downed_pursuit = true;
        downed_aerial = true;
        downed_aquatic = true;
        downed_turret = true;
        found_errat = true;
        //downed_boss_1 = true;
        downed_boss_2 = true;
        downed_boss_3 = true;
        downed_boss_4 = true;

        // Reports
        data = 100;
        report_1 = true;
        report_2 = true;
        report_3 = true;
        report_4 = true;
        report_5 = true;
        report_6 = true;
        report_7 = true;
        report_8 = true;
        report_9 = true;
        report_10 = true;
    }

    IEnumerator SceneSwitch(string load, string checkpoint)
    {
        nextDoor = checkpoint;
        area = load;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
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
}
