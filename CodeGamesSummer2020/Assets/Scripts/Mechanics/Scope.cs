using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public string attachedTo;
    public static string sticky;

    public static bool canSee = true;
    public static bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        sticky = attachedTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (sticky.Substring(0, 7) == "Pursuit")
            {
                canSee = false;
                canJump = true;
            }
        }
    }
}
