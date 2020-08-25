using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public int poolNum;
    public int bounce;
    public Vector3 position;
    public List<GameObject> frag;
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Set projectile effects
        if (GlobalControl.area == "SG_12")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 1;
            }
            else if (gameObject.name.Substring(0, 14) == "Large E_Bullet")
            {
                damage = 4;
                poolNum = 0;
            }
        }
        else if (GlobalControl.area == "TT_12")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 2;
            }
        }

        if (GlobalControl.area.Substring(0, 2) == "TT")
        {
            if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
            {
                damage = 1;
            }
        }
        else if (GlobalControl.area.Substring(0, 2) == "MB")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 2;
            }
        }
        else if (GlobalControl.area == "MB_12")
        {
            if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
            {
                damage = 1;
            }
            else if (gameObject.name.Substring(0, 12) == "Med E_Bullet")
            {
                damage = 3;
                poolNum = 0;
            }
            else if (gameObject.name.Substring(0, 14) == "Large E_Bullet")
            {
                damage = 4;
                poolNum = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // On hit player
        if (other.name == "Player" && !GlobalControl.immune)
        {
            // Deal damage
            GlobalControl.healthCurr -= damage;
            GlobalControl.immune = true;

            // Exploding Shot
            if (gameObject.name.Substring(0, 14) == "Large E_Bullet" && GlobalControl.area == "SG_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                for (int i = 0; i < 8; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                }
            }

            // Crystal Barrage (first and third)
            else if (gameObject.name.Substring(0, 12) == "Med E_Bullet" && GlobalControl.area == "MB_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos((2 * i + 1) * Mathf.PI / 4), Mathf.Sin((2 * i + 1) * Mathf.PI / 4)), 45 * (2 * i + 1), 5f);
                }
            }
            // Crystal Barrage (second and fourth)
            if (gameObject.name.Substring(0, 14) == "Large E_Bullet" && GlobalControl.area == "MB_12") { /* Nothing here */ }

            // Normal bullets
            else
            {
                gameObject.SetActive(false);
            }
        }

        // On hit outer box
        else if (other.transform.parent.name == "Bottom Floor" || other.transform.parent.name == "Top Ceiling" || other.transform.parent.name == "Left Side" || other.transform.parent.name == "Right Side" || other.transform.parent.name == "Destructibles")
        {
            // Exploding Shot
            if (gameObject.name.Substring(0, 14) == "Large E_Bullet" && GlobalControl.area == "SG_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                for (int i = 0; i < 8; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                }
            }
            // Crystal Barrage (first and third)
            else if (gameObject.name.Substring(0, 12) == "Med E_Bullet" && GlobalControl.area == "MB_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos((2 * i + 1) * Mathf.PI / 4), Mathf.Sin((2 * i + 1) * Mathf.PI / 4)), 45 * (2 * i + 1), 5f);
                }
            }
            // Crystal Barrage (second and fourth)
            if (gameObject.name.Substring(0, 14) == "Large E_Bullet" && GlobalControl.area == "MB_12")
            {
                if (bounce < 5)
                {
                    if (other.transform.parent.name == "Top Ceiling" || other.transform.parent.name == "Bottom Floor") 
                    {
                        rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * -1);
                    }
                    else if (other.transform.parent.name == "Left Side" || other.transform.parent.name == "Right Side")
                    {
                        rb2D.velocity = new Vector2(rb2D.velocity.x * -1, rb2D.velocity.y);
                    }
                    bounce++;
                }
                else
                {
                    position = transform.position;
                    gameObject.SetActive(false);

                    for (int i = 0; i < 8; i++)
                    {
                        fireBullet(frag[0], new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                    }
                }                
            }


            if (GlobalControl.area.Substring(0, 2) == "TT")
            {
                if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
                {
                    gameObject.SetActive(false);
                }
            }

        }


        // On hit any terrain
        else if (other.CompareTag("Floor") || other.CompareTag("Ceiling") || other.CompareTag("Wall"))
        {
            if (GlobalControl.area.Substring(0, 2) == "TT")
            {
                if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
                {
                    gameObject.SetActive(false);
                }

                if (GlobalControl.area.Substring(0, 2) == "MB" && GlobalControl.area != "MB_12")
                {
                    if (gameObject.name.Substring(0, 13) == "Small E_Bullet")
                    {
                        gameObject.SetActive(false);
                    }
                }

            }
        }         
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    // Parameters: object fired, trajectory, object/sprite rotation, object velocity
    void fireBullet(GameObject bullet, Vector2 direction, float rotation2, float speed)
    {
        // Fires a bullet from the pool
        GameObject b = null;

        if (poolNum == 0)
        {
            b = EnemyObjectPooler.SharedInstance.GetPooledObject();
        }
        else if (poolNum == 1)
        {
            b = EnemyObjectPooler2.SharedInstance.GetPooledObject();
            Debug.Log(0);
        }
        else if (poolNum == 2)
        {
            b = EnemyObjectPooler3.SharedInstance.GetPooledObject();
        }
        else if (poolNum == 3)
        {
            b = EnemyObjectPooler4.SharedInstance.GetPooledObject();
        }

        if (bullet != null)
        {
            b.SetActive(true);
            b.transform.position = position;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation2);
            b.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}