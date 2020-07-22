using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBtnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Only works if EventSystem is at top and start in this scene
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReactor(string name)
    {
        if (ReactorManage.open &&
            ((name == "basic" && GlobalControl.basicUnlocked) ||
            (name == "unstable" && GlobalControl.unstableUnlocked) ||
            (name == "familiar" && GlobalControl.familiarUnlocked)))
        {
            GlobalControl.reactor = name;
            if (name == "basic" && GlobalControl.imperialUnlocked)
            {
                GlobalControl.reactor = "imperial";
            }
        }
        Debug.Log(GlobalControl.reactor);
    }
}
