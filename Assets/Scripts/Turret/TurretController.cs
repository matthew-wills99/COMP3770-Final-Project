using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    public GameObject barrel;
    public GameObject bullet;
    public PlayerController playerController;

    [Header("Settings")]
    public float shootCooldown = 1f;
    public float bulletSpeed = 10f;
    public float health = 100f;
    public float healthDecrement = 5f;

    private bool canShoot = true;

    void Update()
    {
        if(canShoot)
        {
            StartCoroutine(Shoot());
        }
        transform.LookAt(playerController.GetPlayerTransform().position);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        health -= healthDecrement;
        Debug.Log("hit enemy: " + health.ToString());
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        GameObject bulletObj = Instantiate(bullet, barrel.transform.position, transform.rotation);

        Vector3 shootDir = (playerController.GetPlayerTransform().transform.position - barrel.transform.position).normalized;
        bulletObj.transform.GetComponent<Bullet>().InitBullet(shootDir, "enemy", bulletSpeed);

        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }
}
