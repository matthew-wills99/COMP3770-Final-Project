using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public Transform cursor;
    public PlayerController playerController;
    public CameraFollowPlayer cameraFollowPlayer;
    private Vector3 mousePos;
    private Vector3 rotation;
    private float rotZ;

    public Vector3 screenPos;
    public Vector3 worldPos;

    private void Awake()
    {
        PlayerController.onShoot += Shoot;
    }

    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 10;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos) /*+ cameraFollowPlayer.GetCameraPos()*/;
        worldPos = new Vector3(worldPos.x, worldPos.y, worldPos.z);

        cursor.position = worldPos;




        /*mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotation = mousePos - transform.position;
        float angle = Mathf.Atan2(rotation.x, rotation.z)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        /*
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotZ, 0);*/
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, playerController.GetHandsPos(), Quaternion.identity);

        Vector3 shootDir = playerController.GetPlayerTransform().transform.forward;
        bulletObj.transform.GetComponent<Bullet>().InitBullet(shootDir);
    }
}
