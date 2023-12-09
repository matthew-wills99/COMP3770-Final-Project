using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public TrailRenderer tr;

    [Header("Movement")]
    public float speedMultiplier = 25f; // Adjust the speed of the player, 25 works well

    [Header("Blink")]
    public float blinkDistance = 5f; // Adjust how far the player blinks
    public float blinkCooldown = 5f; // Adjust the cooldown of the blink ability in seconds
    private bool canBlink = true;

    private Vector3 dir; // Store the movement direction of the player
    
    void Start()
    {
        dir = new Vector3();
        dir = Vector3.zero;

        orientation.transform.position = new Vector3(0, 1, blinkDistance);
    }

    void Update()
    {
        Move();

        // Player blinks
        if(Input.GetKeyDown(KeyCode.LeftShift) && canBlink)
        {
            StartCoroutine(Blink());
        }
    }

    public IEnumerator Blink()
    {
        Debug.Log("Blink");
        canBlink = false;
        tr.emitting = true;

        transform.position = orientation.position;

        yield return new WaitForSeconds(0.1f);
        tr.emitting = false;
        yield return new WaitForSeconds(blinkCooldown);

        canBlink = true;
        Debug.Log("Blink ready.");
    }

    void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        transform.position += speedMultiplier * Time.deltaTime * dir;
    
        if(dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }
}   