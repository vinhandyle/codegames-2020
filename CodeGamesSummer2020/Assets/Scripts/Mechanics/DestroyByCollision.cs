using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the bullet hits something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Ceiling") || other.gameObject.CompareTag("Enemy") || (other.gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial"))
        {
            // Destroy bullet
            if (gameObject.CompareTag("Player Bullet"))
            {
                gameObject.SetActive(false);
            }

            // Destroy fragile blocks
            if (other.gameObject.name == "Secret_Unstable")
            {
                GlobalControl.secret_unstable = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_DH_1")
            {
                GlobalControl.block_DH_1 = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_DH_4")
            {
                GlobalControl.block_DH_4 = false;
                other.gameObject.SetActive(false);
            }
        }
    }
}
