using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : MonoBehaviour
{
    public static int healthMax;
    public static int healthCurr;
    public static int damage;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize enemy health and damage
        if (gameObject.name.Substring(0, 12) == "Empire Grunt")
        {
            healthMax = 10;
            damage = 1;
        }
        healthCurr = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurr <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Trigger contact effects
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Player Bullet")
        {
            healthCurr -= GlobalControl.damage;
            Debug.Log(healthCurr);
        }
    }
}
