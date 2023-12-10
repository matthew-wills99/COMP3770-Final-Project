using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private float speed;
    public void InitBullet(Vector3 shootDir, float speed=10f)
    {
        this.shootDir = shootDir;
        this.speed = speed;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * shootDir;
    }
}
