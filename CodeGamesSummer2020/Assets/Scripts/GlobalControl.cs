using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour // overhaul
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

    // Unlock 
    public static bool batteryUnlocked = false;     // Battery
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
    public static bool extraFound = false;         // Extra Battery
    public static bool plateFound = true;          // Special Plating

    // Menu
    //public static bool infoPage = true;

    // World
    public static int bossDowned = 0;              // How many bosses have been defeated
    public static string area = "";                // Area name for scene change purposes
    public static string prevArea = "";            // Name of previous area
    public static string checkpoint = "";          // Area name of last repair station used
    public static string nextDoor = "";            // The door on the other side, from which the player will exit from

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
        
    }
}
