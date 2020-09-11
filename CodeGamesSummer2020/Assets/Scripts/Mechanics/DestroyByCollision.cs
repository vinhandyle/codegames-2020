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
        if (other.CompareTag("Wall") || other.CompareTag("Floor") || other.CompareTag("Ceiling") || (other.CompareTag("Enemy") && !other.GetComponent<Obstacles>().hazard) || (other.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial"))
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
            else if (other.gameObject.name == "Block_DH_4")
            {
                GlobalControl.block_DH_4 = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_DH_5S")
            {
                GlobalControl.secret_DH_5 = false;
                other.gameObject.SetActive(false);
            }

            else if (other.gameObject.name == "Block_TT_6")
            {
                GlobalControl.block_TT_6 = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_TT_9")
            {
                GlobalControl.block_TT_9 = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_TT_14S")
            {
                GlobalControl.block_TT_14S = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Secret_MB_3")
            {
                GlobalControl.secret_MB_3 = false;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.name == "Block_IT_4")
            {
                GlobalControl.block_IT_4 = false;
                other.gameObject.SetActive(false);
            }
        }
    }
}
