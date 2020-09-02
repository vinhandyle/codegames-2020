using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.lift_direction == "up")
        {
            transform.position += new Vector3(0, -0.1f);
            if (transform.position.y <= -5f)
                transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        }
        else
        {
            transform.position += new Vector3(0, 0.1f);
            if (transform.position.y >= 5f)
                transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
        }
    }
}
