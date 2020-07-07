using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBtnBehavior : MonoBehaviour
{
    public string attachedTo;

    // Start is called before the first frame update
    void Start()
    {
        
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
            if (ReactorBtn.pType == "imperial" && name == "basic")
            {
                GlobalControl.reactor = ReactorBtn.pType;
            }
        }
        Debug.Log(GlobalControl.reactor);
    }
}
