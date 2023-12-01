using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMultiplier = 0.5f; // Adjust the speed of the player
    float xDir;
    float zDir;
    void FixedUpdate()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(xDir, 0f, zDir) * speedMultiplier;

        transform.position += moveDir;
    }
}
