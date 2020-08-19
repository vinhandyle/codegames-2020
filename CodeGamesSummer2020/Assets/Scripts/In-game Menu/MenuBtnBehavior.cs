using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtnBehavior : MonoBehaviour
{
    public static string btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Description.descOf = btn;
    }

    // Section Header Button Press
    public void ChangeMenu(string menu)
    {
        if (MenuBackground.inMenu && 
            (menu == "inventory" || menu == "help" || menu == "exit" ||
            (menu == "map" && GlobalControl.mapUnlocked) ||
            (menu == "enemies" && GlobalControl.counter_1 > 0) ||
            (menu == "reports" && GlobalControl.data > 0)))
        {
            btn = "";
            if (menu != "exit")
            {
                GlobalControl.menu = menu;
            }
            else
            {
                MenuBackground.inMenu = false;
                GlobalControl.canContinue = true;                
                StartCoroutine(SceneSwitch("Main Menu"));
            }
        }
    }

    // Button Press for Description
    public void ChangeDesc(string name)
    {
        btn = name;
    }

    IEnumerator SceneSwitch(string load)
    {
        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
