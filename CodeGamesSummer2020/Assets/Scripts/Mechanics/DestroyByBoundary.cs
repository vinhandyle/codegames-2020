using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When bullet exits the screen bounds
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            if (gameObject.CompareTag("Player Bullet") || gameObject.CompareTag("Enemy Bullet"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
