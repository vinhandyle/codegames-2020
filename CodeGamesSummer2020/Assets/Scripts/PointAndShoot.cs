using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshairs;
    public GameObject player;
    public GameObject bulletPrefab;
    private Vector3 target;

    public float bulletSpeed = 5.0f;
    public float useTime = 0.2f;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Player can only shoot when they have energy and are not in a menu
        if (Input.GetMouseButtonDown(0) && canShoot && GlobalControl.energyCurr > 0 && !MenuBtn.inMenu && !ReactorManage.open && GlobalControl.gunUnlocked && GlobalControl.reactor != "")
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            StartCoroutine(cooldown());
        }
    }

    void fireBullet(Vector2 direction, float rotation2)
    {
        // Fires a bullet from the pool
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = player.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation2);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            GlobalControl.energyCurr -= GlobalControl.energyUse;
        }
    }

    // Player cannot shoot for a certain after shooting, preventing spam-fire
    IEnumerator cooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(useTime);

        canShoot = true;
    }
}
