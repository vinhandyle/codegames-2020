using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool midair = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        // Press "a" to move left
        if (Input.GetKey("a"))
        {
            transform.Translate(Time.deltaTime * -5, 0, 0);
        }

        // Press "d" to move right
        if (Input.GetKey("d"))
        {
            transform.Translate(Time.deltaTime * 5, 0, 0);
        }

        // Press "space" to jump
        if (Input.GetKey("space") && !midair)
        {
            transform.Translate(0, Time.deltaTime * 9, 0);
        }

        if (Input.GetKeyUp("space"))
        {
            midair = true;
        }

        if(Collider2D.IsTouching("Floor"))
    }
}

