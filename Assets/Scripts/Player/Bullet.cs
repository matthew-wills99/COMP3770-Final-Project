using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    
    private float speed;
    private float radius;
    public void InitBullet(Vector3 shootDir, float speed=10f, float lifespan=10f)
    {
        this.radius = transform.localScale.x;
        this.shootDir = shootDir;
        this.speed = speed;
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter2D()
    {
        
    }
    
    void MoveBullet()
    {
        transform.position += speed * Time.deltaTime * shootDir;
    }

    private void Update()
    {
        MoveBullet();

        
    }
}
