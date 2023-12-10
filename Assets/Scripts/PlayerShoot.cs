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
    public Vector3 offset;
    private float rotZ;

    public Vector3 screenPos;
    public Vector3 worldPos;

    void Start()
    {
        offset = Vector3.zero;
    }

    private void Awake()
    {
        PlayerController.onShoot += Shoot;
    }

    void Update()
    {
        /*mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos += offset;
        cursor.position = mousePos;*/

        
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 10;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos) /*commentthisout+ cameraFollowPlayer.GetCameraPos()*/;
        worldPos += offset;
        worldPos.y = 0;

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
