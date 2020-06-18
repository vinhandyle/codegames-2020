using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    // Note that player must be set to Never Sleep in order for energy to increment while staionary

    public float regenTime = 1.0f; // Time between energy increments
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
        if (other.gameObject.CompareTag("Player") && Player.energyCurr < Player.energyMax && canRegen)
        {
            StartCoroutine(regen());
        }
    }

    // Increments current energy every 0.5 seconds
    IEnumerator regen()
    {
        if (gameObject.CompareTag("Solar") && Player.solarUnlocked)
        {
            Player.energyCurr++;

        }
        else if (gameObject.CompareTag("Geothermal") && Player.geoUnlocked)
        {
            Player.energyCurr += 5;
        }
        canRegen = false;

        yield return new WaitForSeconds(regenTime);

        canRegen = true;
    }
}
