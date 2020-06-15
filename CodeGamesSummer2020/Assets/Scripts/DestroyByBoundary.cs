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

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        if (other.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Exited Bound");
            if (gameObject.CompareTag("Player Bullet"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
