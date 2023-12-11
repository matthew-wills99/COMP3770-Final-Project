using System;
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
        try 
        {
            Destroy(gameObject, lifespan);
        }
        catch (Exception)
        {
            // tried to destroy bullet that was already destroyed because it hit something
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy" && sender != "enemy")
        {
            other.GetComponentInParent<TurretController>().Hit();
            Destroy(gameObject);
        }

        if(other.tag == "Player" && sender != "Player")
        {
            other.GetComponentInParent<PlayerController>().Hit();
            Destroy(gameObject);
        }

        if(other.tag == "Monster")
        {
            other.GetComponent<Monster>().TakeDamage();
            Debug.Log("Monster hit");
        }

        if(other.tag != "bullet")
        {
            Destroy(gameObject);
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
