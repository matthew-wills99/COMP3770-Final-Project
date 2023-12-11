using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    
    private float speed;
    private string sender;
    public void InitBullet(Vector3 shootDir, string sender, float speed=10f, float lifespan=10f)
    {
        this.shootDir = shootDir;
        this.speed = speed;
        this.sender = sender;
        Destroy(gameObject, lifespan);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy" && sender != "enemy")
        {
            other.GetComponentInParent<TurretController>().Hit();
        }

        if(other.tag == "Player" && sender != "Player")
        {
            other.GetComponentInParent<PlayerController>().Hit();
        }
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
