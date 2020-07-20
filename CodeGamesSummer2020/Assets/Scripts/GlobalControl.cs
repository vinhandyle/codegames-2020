using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour 
{
    // Endings
    public static bool ending_1 = true;           // Save Humanity
    public static bool ending_2 = true;           // Return to the Past
    public static bool ending_3 = true;           // End the Cycle
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

    // Unlock 
    public static bool batteryUnlocked = false;    // Battery
    public static bool solarUnlocked = false;       // Solar Panel
    public static bool geoUnlocked = true;         // Geothermal Extractor

    public static bool gunUnlocked = true;         // Energy Cannon
    public static bool mapUnlocked = true;         // Navigational Module
    public static bool heartlessUnlocked = false;   // Heartless Generator

    public static bool dashUnlocked = true;        // Booster Rocket
    public static bool clingUnlocked = false;       // Climbing Claws
    public static bool doubleUnlocked = true;      // Booster Rocket MK2

    public static bool basicUnlocked = true;       // Basic Reactor
    public static bool imperialUnlocked = false;    // Strange Reactor
    public static bool familiarUnlocked = false;    // Lost Reactor
    public static bool unstableUnlocked = false;    // Unstable Reactor

    public static bool scrapFound = true;          // Hyper Scrap
    public static bool extraFound = true;         // Extra Battery
    public static bool plateFound = true;          // Special Plating

    public static bool extra_1 = false;            // Extra battery pick-ups
    public static bool extra_2 = false;
    public static bool extra_3 = false;

    public static bool plating_1 = false;          // Special Plating pick-ups
    public static bool plating_2 = false;

    // Inventory Numbers
    public static int plateNum = 0;                // Number of special plating obtained
    public static int extraNum = 0;                // Number of extra batteries obtained
    public static int scrapNum = 0;                // Number of hyper scraps in possession

    // Toggle
    public static string reactor = "imperial";        // Name of equipped reactor
    public static bool h2e = true;                 // True = HP to Energy, False = Energy to HP

    // World
    public static int humansLeft = 6;              // How many humans left to capture (6: May-October)
    public static int bossDowned = 0;              // How many bosses have been defeated
    public static bool switched = false;           // Used to set position on scene switch

    public static string area = "Ending_1";                // Area name for scene change purposes
    public static string prevArea = "";            // Name of previous area
    public static string checkpoint = "";          // Area name of last repair station used

    // Dialogue
    public static int counter_1 = 0;               // Counter for First dialogue

    // Doors
    public static string nextDoor = "";            // The door on the other side, from which the player will exit from

    public static bool locked_1 = true;            // Birthplace-Start Door
    public static bool locked_2 = false;           // The Lift to Heaven Door
    public static bool locked_3 = true;            // DH_4_to_DH_6

    // Destructibles
    public static bool block_starter = true;
    public static bool secret_unstable = true;

    public static bool block_DH_1 = true;
    public static bool block_DH_4 = true;
    public static bool secret_DH_5 = true;

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

    }

    // Update is called once per frame
    void Update()
    {
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

            // No checkpoints used
            else
            {
                if (counter_1 > 5)
                {
                    StartCoroutine(SceneSwitch("DH_2", ""));
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

        // Destructibles
        block_starter = true;
        secret_unstable = true;
        block_GP_1 = true;
    }


    public static void respawnAll()
    {
        patrol_1_0_0 = true;
        patrol_1_0_1 = true;
        patrol_1_0_2 = true;

        patrol_1_1_0 = true;
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
}
