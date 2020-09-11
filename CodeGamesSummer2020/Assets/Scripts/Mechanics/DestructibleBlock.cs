﻿using System.Collections;
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
            (gameObject.name == "Block_DH_5S" && !GlobalControl.secret_DH_5) ||
            (gameObject.name == "Secret_DH_4" && !GlobalControl.secret_DH_5) ||
            (gameObject.name == "Block_SG_9" && !GlobalControl.block_SG_9) ||
            (gameObject.name == "Block_SG_11" && !GlobalControl.block_SG_11) ||
            (gameObject.name == "Block_SG_11S" && !GlobalControl.block_SG_11S) ||
            (gameObject.name == "Block_SG_11S (1)" && !GlobalControl.block_SG_11S_) ||
            (gameObject.name == "Block_SG_12" && !GlobalControl.block_SG_12) ||
            (gameObject.name == "Secret_SG_9" && !GlobalControl.secret_SG_9) ||
            (gameObject.name == "Block_TT_2" && !GlobalControl.block_TT_2) ||
            (gameObject.name == "Block_TT_6" && !GlobalControl.block_TT_6) ||
            (gameObject.name == "Block_TT_9" && !GlobalControl.block_TT_9) ||
            (gameObject.name == "Block_TT_11" && !GlobalControl.block_TT_11) ||
            (gameObject.name == "Block_TT_12" && !GlobalControl.block_TT_12) ||
            (gameObject.name == "Block_TT_14S" && !GlobalControl.block_TT_14S) ||
            (gameObject.name == "Secret_MB_3" && !GlobalControl.secret_MB_3) ||
            (gameObject.name == "Block_IT_4" && !GlobalControl.block_IT_4) ||
            (gameObject.name == "Block_IT_4 (1)" && !GlobalControl.block_IT_4_) ||
            (gameObject.name == "Block_IT_6" && !GlobalControl.block_IT_6) ||
            (gameObject.name == "Block_IT_9" && !GlobalControl.block_IT_9) ||
            (gameObject.name == "Block_GP_4" && !GlobalControl.block_GP_4) ||
            (gameObject.name == "Block_GP_10" && !GlobalControl.block_GP_10))
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
        else if (gameObject.name == "Block_SG_11" && !GlobalControl.block_SG_11)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_SG_11S" && !GlobalControl.block_SG_11S)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_SG_11S (1)" && !GlobalControl.block_SG_11S_)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_SG_12" && GlobalControl.downed_boss_1)
        {
            gameObject.SetActive(false);
            GlobalControl.block_SG_12 = false;
        }
        else if (gameObject.name == "Block_TT_11" && !GlobalControl.block_TT_11)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_TT_12" && GlobalControl.downed_boss_2)
        {
            gameObject.SetActive(false);
            GlobalControl.block_TT_12 = false;
        }
        else if (gameObject.name == "Block_IT_4 (1)" && !GlobalControl.block_IT_4_)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_IT_6" && !GlobalControl.block_IT_6)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_IT_9" && !GlobalControl.block_IT_9)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_GP_4" && !GlobalControl.block_GP_4)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "Block_GP_10" && !GlobalControl.block_GP_10)
        {
            gameObject.SetActive(false);
        }
    }
}
