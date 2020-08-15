using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress(string type)
    {
        if (type == "start")
        {
            // Reset all global values and switch to first scene
            GlobalControl.energyMax = 10;
            GlobalControl.energyCurr = GlobalControl.energyMax;
            GlobalControl.healthMax = 10;
            GlobalControl.healthCurr = GlobalControl.healthMax;
            GlobalControl.data = 0;
            GlobalControl.batteryUnlocked = false;
            GlobalControl.solarUnlocked = false;
            GlobalControl.geoUnlocked = false;
            GlobalControl.gunUnlocked = false;
            GlobalControl.mapUnlocked = false;
            GlobalControl.heartlessUnlocked = false;
            GlobalControl.keyUnlocked = false;
            GlobalControl.dashUnlocked = false;
            GlobalControl.clingUnlocked = false;
            GlobalControl.doubleUnlocked = false;
            GlobalControl.imperialUnlocked = false;
            GlobalControl.familiarUnlocked = false;
            GlobalControl.unstableUnlocked = false;
            GlobalControl.scrapFound = false;
            GlobalControl.extraFound = false;
            GlobalControl.plateFound = false;
            GlobalControl.extra_1 = false;
            GlobalControl.extra_2 = false;
            GlobalControl.extra_3 = false;
            GlobalControl.plating_1 = false;
            GlobalControl.plating_2 = false;
            GlobalControl.scrapNum = 0;
            GlobalControl.extraNum = 0;
            GlobalControl.plateNum = 0;
            GlobalControl.reactor = "basic";
            GlobalControl.h2e = true;
            GlobalControl.bossDowned = 0;
            GlobalControl.prevArea = "";
            GlobalControl.checkpoint = "";
            GlobalControl.errat_0 = true;
            GlobalControl.errat_1 = true;
            GlobalControl.errat_2 = true;
            GlobalControl.errat_3 = true;
            GlobalControl.errat_4 = true;
            GlobalControl.errat_5 = true;
            GlobalControl.resetObjects();
            GlobalControl.respawnAll();

            StartCoroutine(SceneSwitch("Start_"));
        }
        else if (type == "controls")
        {
            MainMenuBtn.inControl = true;
        }
        else if (type == "trophy")
        {
            MainMenuBtn.inTrophy = true;
        }
        else if (type == "exit")
        {
            MainMenuBtn.inControl = false;
            MainMenuBtn.inTrophy = false;
        }
    }

    IEnumerator SceneSwitch(string load)
    {
        GlobalControl.area = load;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
