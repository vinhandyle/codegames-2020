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
            if (MainMenuBtn.confirmNew)
            {
                // Reset all global values and switch to first scene
                GlobalControl.canContinue = true;
                GlobalControl.resetPlayer();
                GlobalControl.resetObjects();
                GlobalControl.respawnAll();

                StartCoroutine(SceneSwitch("Start_"));
            }
            else
            {
                MainMenuBtn.confirmNew = true;
            }
        }
        else if (type == "continue")
        {
            if (GlobalControl.canContinue)
            {
                StartCoroutine(SceneSwitch2(GlobalControl.prevArea));
            }
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
            MainMenuBtn.confirmNew = false;
            MainMenuBtn.inControl = false;
            MainMenuBtn.inTrophy = false;
        }
    }

    IEnumerator SceneSwitch(string load)
    {
        if (!(load == null || load == ""))
            GlobalControl.area = load;
        else
            load = GlobalControl.area;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    IEnumerator SceneSwitch2(string load)
    {
        GlobalControl.area = load;
        GlobalControl.nextDoor = "";

        SceneManager.LoadScene(load, LoadSceneMode.Single);    
        
        Player.x = GlobalControl.pX;
        Player.y = GlobalControl.pY;
        GlobalControl.switched = true;        

        yield return null;

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);        
    }
}
