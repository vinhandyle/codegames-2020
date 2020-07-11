using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public bool fragile;
    public static bool isFragile = false;

    // Start is called before the first frame update
    void Start()
    {
        // Able to be destroyed by player bullet
        if (fragile)
        {
            isFragile = fragile;
        }

        // Retain state on scene load
        if ((gameObject.name == "Block_Starter" && !GlobalControl.block_starter) ||
            (gameObject.name == "Secret_Unstable" && !GlobalControl.secret_unstable))
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
    }
}
