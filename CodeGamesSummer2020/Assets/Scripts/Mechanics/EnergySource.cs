using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    // Note that player must be set to Never Sleep in order for energy to increment while staionary

    public static float regenTime = 1.5f; // Time between energy increments
    private bool canRegen = true; // Whether current energy can be incremented on a given frame

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Triggers while player is in sunlight
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && GlobalControl.energyCurr < GlobalControl.energyMax && canRegen)
        {
            StartCoroutine(regen());
        }
    }

    // Increments current energy every 0.5 seconds
    IEnumerator regen()
    {
        if (gameObject.CompareTag("Solar") && GlobalControl.solarUnlocked)
        {
            GlobalControl.energyCurr++;

        }
        else if (gameObject.CompareTag("Geothermal") && GlobalControl.geoUnlocked)
        {
            GlobalControl.energyCurr += 5;
        }
        canRegen = false;

        yield return new WaitForSeconds(regenTime);

        canRegen = true;
    }
}
