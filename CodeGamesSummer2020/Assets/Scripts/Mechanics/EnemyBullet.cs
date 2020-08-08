using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public bool pooling = false;
    public Vector3 position;
    public List<GameObject> frag;

    // Start is called before the first frame update
    void Start()
    {
        // Boss Projectiles
        if (GlobalControl.area == "SG_12")
        {
            if (gameObject.name.Substring(0, 14) == "Small E_Bullet")
            {
                damage = 1;
            }
            else if (gameObject.name.Substring(0, 14) == "Large E_Bullet")
            {
                damage = 4;
                pooling = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameObject.SetActive(false);
            GlobalControl.healthCurr -= damage;
        }

        if (gameObject.name.Substring(0, 14) == "Large E_Bullet" && GlobalControl.area == "SG_12")
        {
            float x = 0;
            float y = 0;
            position = transform.position;

            if (other.CompareTag("Floor") || other.CompareTag("Ceiling") || other.CompareTag("Wall") || other.CompareTag("Player"))
            {
                for (int i = 0; i < 8; i++)
                {
                    // 8-spread circle
                    if (i == 0) { x = 1; y = 0; }
                    else if (i == 1) { x = Mathf.Sqrt(2) / 2; y = Mathf.Sqrt(2) / 2; }
                    else if (i == 2) { x = 0; y = 1; }
                    else if (i == 3) { x = Mathf.Sqrt(2) / -2; y = Mathf.Sqrt(2) / 2; }
                    else if (i == 4) { x = -1; y = 0; }
                    else if (i == 5) { x = Mathf.Sqrt(2) / -2; y = Mathf.Sqrt(2) / -2; }
                    else if (i == 6) { x = 0; y = -1; }
                    else if (i == 7) { x = Mathf.Sqrt(2) / 2; y = Mathf.Sqrt(2) / -2; }

                    fireBullet(frag[0], new Vector2(x, y), 45 * i, 5f);
                }
            }
        }
    }

    // Parameters: object fired, trajectory, object/sprite rotation, object velocity
    void fireBullet(GameObject bullet, Vector2 direction, float rotation2, float speed)
    {
        // Fires a bullet from the pool
        GameObject b = null;

        if (pooling)
        {
            b = EnemyObjectPooler.SharedInstance.GetPooledObject();
        }
        else
        {
            b = Instantiate(bullet) as GameObject;
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