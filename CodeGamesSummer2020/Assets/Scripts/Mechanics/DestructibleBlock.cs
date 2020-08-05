using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public static bool isFragile = false;

    // Start is called before the first frame update
    void Start()
    {
        // Retain state on scene load
        if ((gameObject.name == "Block_Starter" && !GlobalControl.block_starter) ||
            (gameObject.name == "Secret_Unstable" && !GlobalControl.secret_unstable) ||
            (gameObject.name == "Block_DH_4" && !GlobalControl.block_DH_4) ||
            (gameObject.name == "Block_DH_5" && GlobalControl.counter_1 < 6) ||
            (gameObject.name == "Secret_DH_4" && !GlobalControl.secret_DH_5) ||
            (gameObject.name == "Block_GP_1" && !GlobalControl.block_GP_1) ||
            (gameObject.name == "Block_SG_9" && !GlobalControl.block_SG_9) ||
            (gameObject.name == "Block_SG_10" && !GlobalControl.block_SG_10) ||
            (gameObject.name == "Block_SG_11" && !GlobalControl.block_SG_11) ||
            (gameObject.name == "Secret_SG_9" && !GlobalControl.secret_SG_9))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Block_Starter" && GlobalControl.gunUnlocked)
        {
            gameObject.SetActive(false);
            GlobalControl.block_starter = false;
        }
        else if (gameObject.name == "Block_SG_10" && !GlobalControl.block_SG_10)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_SG_11" && !GlobalControl.block_SG_11)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_GP_1" && GlobalControl.doubleUnlocked)
        {
            gameObject.SetActive(false);
            GlobalControl.block_GP_1 = false;
        }
    }
}
