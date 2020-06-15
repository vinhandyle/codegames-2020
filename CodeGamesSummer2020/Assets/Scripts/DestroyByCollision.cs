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
        Debug.Log("Enter");
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Ceiling") || other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enter True");
            if (gameObject.CompareTag("Player Bullet"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
