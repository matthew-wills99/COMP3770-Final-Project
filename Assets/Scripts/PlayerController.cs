using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private TrailRenderer tr;

    // Adjust the speed of the player
    public float speedMultiplier = 0.5f;
    private float xDir;
    private float zDir;

    // Dash variables
    private bool canDash = true;
    private bool isDashing = false;

    // Adjust the speed multiplier when dashing
    public float dashSpeedMultiplier = 3f;

    // Adjust the time the player dashes for
    public float dashTime = 0.2f;

    // Adjust the cooldown of the dash
    public float dashCooldown = 1f;

    void Update()
    {
        // Check if the shift key is pressed and the player can dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            // Start the dash
            StartCoroutine(Dash());
        }
    }

    // Dash coroutine
    private IEnumerator Dash()
    {
        /*
        Start of the dash, set the canDash bool to false and isDashing bool to true, starting the dash
        Store the original speed multiplier in a variable so we dont lose it
        Start the trail renderer, then wait for the dash to finish

        Return the speed multiplier to the original value, stop the trail renderer
        Set the isDashing bool back to false indicating the dash is finished
        Wait for the cooldown and return the canDash bool back to true
        */
        canDash = false;
        isDashing = true;

        float originalSpeed = speedMultiplier;
        speedMultiplier *= dashSpeedMultiplier;
        tr.emitting = true;

        Debug.Log("Dash Start");

        yield return new WaitForSeconds(dashTime);

        speedMultiplier = originalSpeed;
        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        Debug.Log("Dash End");

        canDash = true;
        
    }

=======
    public float speedMultiplier = 0.5f; // Adjust the speed of the player
    float xDir;
    float zDir;
>>>>>>> parent of ddf606e (Dash movement, refactoring & commenting)
    void FixedUpdate()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(xDir, 0f, zDir) * speedMultiplier;

        transform.position += moveDir;
    }
}
