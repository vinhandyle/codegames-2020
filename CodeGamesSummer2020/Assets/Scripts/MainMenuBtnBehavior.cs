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
