using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Non-static => keep the separate from toher platforms
    public BoxCollider2D box;
    public float width;
    public float height;
    public string side;
    public bool inverted;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        width = box.size.x;
        height = box.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Pass through bottom, land on top
        if (gameObject.CompareTag("Floor") || gameObject.CompareTag("Wall"))
        {

            if (Player.rb2D.position.y - Player.rb2D.GetComponent<CircleCollider2D>().radius > transform.position.y + height / 2)
            {
                box.enabled = true;
            }
            else
            {
                box.enabled = false;
            }
        }
        // Pass through top, block from bottom
        else if (gameObject.CompareTag("Ceiling"))
        {
            if (Player.rb2D.position.y + Player.rb2D.GetComponent<CircleCollider2D>().radius < transform.position.y - height / 2)
            {
                box.enabled = true;
            }
            else
            {
                box.enabled = false;
            }
        }
    }
}
