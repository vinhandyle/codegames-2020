using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite regular;
    public Sprite unstable;
    public Sprite imperial;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalControl.reactor == "imperial")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = imperial;
        }
        else if (GlobalControl.reactor == "unstable")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unstable;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = regular;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
