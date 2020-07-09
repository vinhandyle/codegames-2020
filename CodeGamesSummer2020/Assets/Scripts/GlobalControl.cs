using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour 
{
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
    public static bool batteryUnlocked = true;    // Battery
    public static bool solarUnlocked = true;       // Solar Panel
    public static bool geoUnlocked = true;         // Geothermal Extractor

    public static bool gunUnlocked = true;         // Energy Cannon
    public static bool mapUnlocked = true;         // Navigational Module
    public static bool heartlessUnlocked = true;   // Heartless Generator

    public static bool dashUnlocked = true;        // Booster Rocket
    public static bool clingUnlocked = true;       // Climbing Claws
    public static bool doubleUnlocked = true;      // Booster Rocket MK2

    public static bool basicUnlocked = true;       // Basic Reactor
    public static bool imperialUnlocked = true;    // Strange Reactor
    public static bool familiarUnlocked = true;    // Lost Reactor
    public static bool unstableUnlocked = true;    // Unstable Reactor

    public static bool scrapFound = true;          // Hyper Scrap
    public static bool extraFound = true;         // Extra Battery
    public static bool plateFound = true;          // Special Plating

    // Numbers
    public static int plateNum = 0;                // Number of special plating obtained
    public static int extraNum = 0;                // Number of extra batteries obtained
    public static int scrapNum = 0;                // Number of hyper scraps obtained

    // Reactor
    public static string reactor = "basic";        // Name of equipped reactor

    // World
    public static int bossDowned = 0;              // How many bosses have been defeated
    public static bool switched = false;           // Used to set position on scene switch

    public static string area = "";                // Area name for scene change purposes
    public static string prevArea = "";            // Name of previous area

    public static string checkpoint = "";          // Area name of last repair station used
    public static string nextDoor = "";            // The door on the other side, from which the player will exit from

    // Enemy State
    // type_tier_area_num
    public static bool patrol_1_0_0 = true;        // Testing Area
    public static bool patrol_1_0_1 = true;
    public static bool patrol_1_0_2 = true;

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
                healthCurr = healthMax;
                energyCurr = energyMax;
            }

            // Respawn all enemies
            respawnAll();
        }
    }

    public static void respawnAll()
    {
        patrol_1_0_0 = true;
        patrol_1_0_1 = true;
        patrol_1_0_2 = true;
    }

    IEnumerator SceneSwitch(string load, string checkpoint)
    {
        nextDoor = checkpoint;
        area = load;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        switched = true;
    }
}
