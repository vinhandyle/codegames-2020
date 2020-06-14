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
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Floor" || other.gameObject.tag == "Ceiling" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hey");
            if (gameObject.tag == "Player Bullet")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
