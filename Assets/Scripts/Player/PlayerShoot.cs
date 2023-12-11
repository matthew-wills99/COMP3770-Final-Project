using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public Transform rotatePoint;
    public Transform cursor;
    public Transform hands;
    public PlayerController playerController;

    public float bulletSpeed = 10f;
    Vector3 requiredHitPoint;
    LayerMask playerLayer;

    /// <summary>
    /// https://www.youtube.com/watch?v=c3842pGVWvU
    /// </summary>
    void NewPosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~playerLayer))
        {
            Vector3 playerHeight = new Vector3(hit.point.x, playerController.GetPlayerTransform().position.y, hit.point.z);

            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            float length = Vector3.Distance(playerHeight, hitPoint);

            var deg = 30;
            var rad = deg * Mathf.Deg2Rad;

            float hypotenuse = length / Mathf.Sin(rad);

            float distanceFromCam = hit.distance;

            if(playerController.GetPlayerTransform().position.y > hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCam - hypotenuse);
            }
            else if(playerController.GetPlayerTransform().position.y < hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCam + hypotenuse);
            }
            else
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCam);
            }
            
            requiredHitPoint.y = 1; // Lock the y value of the cursor to sit just above the floor
            cursor.transform.position = requiredHitPoint; // Move the cursor to where the casted ray hits the floor
        }
    }

    void RotateHands()
    {
        rotatePoint.transform.LookAt(cursor.transform.position);
    }

    private void Awake()
    {
        PlayerController.onShoot += Shoot;
    }

    void Update()
    {
        NewPosition();
        RotateHands();
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, hands.transform.position, hands.transform.rotation);

        Vector3 shootDir = (cursor.transform.position - hands.transform.position).normalized;
        bulletObj.transform.GetComponent<Bullet>().InitBullet(shootDir, "player", bulletSpeed);
    }
}
