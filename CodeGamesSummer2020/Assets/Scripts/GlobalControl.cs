using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // Energy
    public int energyCurr;
    public int energyMax;
    public int energyGain;

    // Combat 
    public int health;
    public int damage;
    public int energyUse;

    // Unlock
    public bool batteryUnlocked;
    public bool solarUnlocked;
    public bool geoUnlocked;

    public bool gunUnlocked;
    public bool mapUnlocked;
    public bool heartlessUnlocked;

    public bool dashUnlocked;
    public bool clingUnlocked;
    public bool doubleUnlocked;

    public bool basicUnlocked;
    public bool imperialUnlocked;
    public bool familiarUnlocked;
    public bool unstableUnlocked;

    public bool scrapFound;

    // World
    public int bossDowned;


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
