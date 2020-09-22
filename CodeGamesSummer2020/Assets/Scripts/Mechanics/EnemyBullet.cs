using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public int poolNum;
    public int bounce;
    public bool homing;
    public Vector3 position;
    public List<GameObject> frag;
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Called when object is set active
    private void OnEnable()
    {
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
            }
        }
        else if (GlobalControl.area == "TT_12")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 2;
            }
        }
        else if (GlobalControl.area == "GP_0B")
        {
            if (gameObject.name.Substring(0, 18) == "Small E_Bullet (1)")
            {
                damage = 1;
            }
            else if (gameObject.name.Substring(0, 18) == "Small E_Bullet (2)")
            {
                damage = 2;
                poolNum = 3;
            }
            else if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 6;
            }
            else if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
            {
                damage = 3;
            }
        }

        else if (GlobalControl.area.Substring(0, 2) == "TT")
        {
            if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
            {
                damage = 1;
            }
        }
        else if (GlobalControl.area.Substring(0, 2) == "MB")
        {
            if (GlobalControl.area == "MB_12")
            {
                if (gameObject.name.Substring(0, 13) == "Crystal Frag ")
                {
                    damage = 1;
                }
                else if (gameObject.name.Substring(0, 14) == "Crystal Frag_1")
                {
                    damage = 2;
                    if (Obstacles.refState_7)
                    {
                        homing = true;
                    }
                    else
                    {
                        homing = false;
                    }
                }
                else if (gameObject.name.Substring(0, 14) == "Crystal Bullet")
                {
                    damage = 3;
                }
                else if (gameObject.name.Substring(0, 8) == "Snowball")
                {
                    damage = 5;
                    poolNum = 1;
                }
            }
            else if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 2;
            }
        }
        else if (GlobalControl.area.Substring(0, 2) == "IT")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 3;
            }
        }
        else if (GlobalControl.area.Substring(0, 2) == "GP")
        {
            if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
            {
                damage = 2;
            }
            else if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                if (GlobalControl.area == "GP_14")
                    damage = 4;
                else
                    damage = 5;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Homing effect
        if (homing)
        {
            Vector3 difference = (Vector3)Player.rb2D.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            transform.position += new Vector3(direction.x * Obstacles.refState1a_7 * 2.5f, direction.y * Obstacles.refState1a_7 * 2.5f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
        }
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
            else if (gameObject.name.Substring(0, 14) == "Crystal Bullet" && GlobalControl.area == "MB_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos((2 * i + 1) * Mathf.PI / 4), Mathf.Sin((2 * i + 1) * Mathf.PI / 4)), 45 * (2 * i + 1), 5f);
                }
            }
            // Crystal Barrage (second and fourth)	
            else if (gameObject.name.Substring(0, 8) == "Snowball" && GlobalControl.area == "MB_12") { /* Nothing here */ }

            // Draining Shot
            else if (gameObject.name.Substring(0, 18) == "Small E_Bullet (1)")
            {
                if (GlobalControl.energyCurr - 5 < 0)
                    GlobalControl.energyCurr = 0;
                else
                    GlobalControl.energyCurr -= 5;
            }
            else if (gameObject.name.Substring(0, 18) == "Small E_Bullet (2)")
            {
                position = transform.position;
                fireBullet(frag[0], new Vector2(0, 0), 0, 0);
            }

            // Normal bullets
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (other.name == "Player" && GlobalControl.immune)
        {
            // Destroy homing bullets
            if (homing)
            {
                gameObject.SetActive(false);
            }
        }

        // On hit player bullet
        else if (other.CompareTag("Player Bullet"))
        {
            if (gameObject.name.Substring(0, 14) == "Crystal Frag_1" && GlobalControl.area == "MB_12" && homing)
            {
                gameObject.SetActive(false);
                other.gameObject.SetActive(false);
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
            else if (gameObject.name.Substring(0, 14) == "Crystal Bullet" && GlobalControl.area == "MB_12")
            {
                position = transform.position;
                gameObject.SetActive(false);

                float rand = Random.Range(0, 2);

                for (int i = 0; i < 4; i++)
                {
                    fireBullet(frag[0], new Vector2(Mathf.Cos((2 * i + rand) * Mathf.PI / 4), Mathf.Sin((2 * i + rand) * Mathf.PI / 4)), 45 * (2 * i + rand), 5f);
                }
            }
            // Crystal Barrage (second and fourth)
            else if (gameObject.name.Substring(0, 8) == "Snowball" && GlobalControl.area == "MB_12")
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
                    bounce = 0;

                    for (int i = 0; i < 8; i++)
                    {
                        fireBullet(frag[0], new Vector2(Mathf.Cos(i * Mathf.PI / 4), Mathf.Sin(i * Mathf.PI / 4)), 45 * i, 5f);
                    }
                }
            }
            // Torpedo or Scatter
            else if (gameObject.name.Substring(0, 14) == "Small E_Bullet" && GlobalControl.area == "MB_12")
            {
                gameObject.SetActive(false);
            }


            if (GlobalControl.area.Substring(0, 2) == "TT" || GlobalControl.area.Substring(0, 2) == "GP")
            {
                if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
                {
                    gameObject.SetActive(false);
                }
                else if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    position = transform.position;
                    gameObject.SetActive(false);

                    if (gameObject.name.Substring(0, 18) == "Small E_Bullet (2)")
                        fireBullet(frag[0], new Vector2(0, 0), 0, 0);
                }
            }
            else if (GlobalControl.area.Substring(0, 2) == "MB" && GlobalControl.area != "MB_12")
            {
                if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    gameObject.SetActive(false);
                }
            }
            else if (GlobalControl.area.Substring(0, 2) == "IT")
            {
                if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    gameObject.SetActive(false);                   
                }
            }
        }

        // On hit any terrain
        else if (other.CompareTag("Floor") || other.CompareTag("Ceiling") || other.CompareTag("Wall"))
        {
            if ((GlobalControl.area.Substring(0, 2) == "TT" && GlobalControl.area != "TT_12") || GlobalControl.area.Substring(0, 2) == "GP")
            {
                if (gameObject.name.Substring(0, 13) == "Tiny E_Bullet")
                {
                    gameObject.SetActive(false);
                }
                else if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    position = transform.position;
                    gameObject.SetActive(false);

                    if (gameObject.name.Substring(0, 18) == "Small E_Bullet (2)")
                        fireBullet(frag[0], new Vector2(0, 0), 0, 0);
                }
            }
            else if (GlobalControl.area.Substring(0, 2) == "MB" && GlobalControl.area != "MB_12")
            {
                if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    gameObject.SetActive(false);
                }
            }
            else if (GlobalControl.area.Substring(0, 2) == "IT" || GlobalControl.area.Substring(0, 2) == "GP")
            {
                if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
                {
                    gameObject.SetActive(false);
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

            // Custom effects
            if (b.name.Substring(0, 14) == "Crystal Frag_1")
            {
                if (GlobalControl.area == "MB_12")
                {
                    b.GetComponent<EnemyBullet>().homing = false;
                }
            }
        }
    }
}