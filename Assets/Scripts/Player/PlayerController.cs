using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    public Transform blinkLocation; // The transform that indicates where the player will blink to
    public Transform playerObject; // The capsule representing the player
    //public Rigidbody rb; // The rigidbody attached to the capsule
    public TrailRenderer tr; // The trail renderer attached to the capsule
    public Rigidbody rb;

    [Header("Stats")]
    public float health = 100f; // Player health
    public float healthDecrement = 5f; // How much health the player loses each hit

    [Header("Movement")]
    public float speedMultiplier = 25f; // Adjust the speed of the player, 25 works well

    [Header("Blink")]
    public float blinkDistance = 5f; // Adjust how far the player blinks
    public float blinkCooldown = 5f; // Adjust the cooldown of the blink ability in seconds
    private bool canBlink = true; // Keep track of if the player can blink or not

    [Header("Attacking")]
    public float attackCooldown = 0.1f; // Adjust how long the player must wait before shooting or attacking again
    private bool canAttack = true; // Keep track of if the player can attack or not

    //private bool canAttack = true; // Not yet used

    public float handsDistance = 1f; // Adjust how far the player's hands are

    // Weapon enumerator to keep track of which weapon is selected
    public enum Weapon {GrenadeLauncher, Railgun, Sword};
    Weapon selectedWeapon = new Weapon();

    // Shoot event
    public delegate void ShootAction();
    public static event ShootAction onShoot;

    private Vector3 dir = new Vector3(); // Store the movement direction of the player
    
    void Start()
    {
        // Default weapon
        selectedWeapon = Weapon.GrenadeLauncher;

        // Init direction of player
        dir = Vector3.zero;

        // Reset rotation because unity sucks?
        //rb.centerOfMass = Vector3.zero;
        //rb.inertiaTensorRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        rb.velocity = dir * speedMultiplier * Time.fixedDeltaTime * 50;
    }

    void Update()
    {

        //Freezing player movement for dialogue
        
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        
        
        Move();

        // Player blinks
        if(Input.GetKeyDown(KeyCode.LeftShift) && canBlink)
        {
            StartCoroutine(Blink());
        }

        // Player changes weapons
        if(Input.GetKeyDown(KeyCode.Z))
        {
            CycleWeapons();
        }

        // Player shoots
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Shoot());
        }
    }

    /// <summary>
    /// Called when the player is hit by a bullet
    /// see Bullet.cs
    /// </summary>
    public void Hit()
    {
        health -= healthDecrement;
        Debug.Log("hit player " + health.ToString());
    }

    /// <summary>
    /// Cycle through each weapon in the weapons enumerator.
    /// </summary>
    private void CycleWeapons()
    {
        if(selectedWeapon == Weapon.GrenadeLauncher)
        {
            selectedWeapon = Weapon.Railgun;
        }
        else if(selectedWeapon == Weapon.Railgun)
        {
            selectedWeapon = Weapon.Sword;
        }
        else if(selectedWeapon == Weapon.Sword)
        {
            selectedWeapon = Weapon.GrenadeLauncher;
        }
    }

    /// <summary>
    /// Blinks to the position of the BlinkLocation object.
    /// Uses the trail renderer to visualize.
    /// </summary>
    private IEnumerator Blink()
    {
        Debug.Log("Blink");
        canBlink = false;
        tr.emitting = true;

        playerObject.transform.position = blinkLocation.position;

        yield return new WaitForSeconds(0.1f);
        tr.emitting = false;
        yield return new WaitForSeconds(blinkCooldown);

        canBlink = true;
        Debug.Log("Blink ready.");
    }

    private IEnumerator Shoot()
    {
        canAttack = false;
        Debug.Log("Shoot");

        // Start shoot event if it exists
        if(onShoot != null)
        {
            onShoot();
        }
        else
        {
            Debug.Log("Failed to invoke event");
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        Debug.Log("Shoot ready");
    }

    /// <summary>
    /// Calculate the direction of travel based on user input,
    /// then moves the player accordingly and rotates towards the direction of travel.
    /// </summary>
    private void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        //rb.position += dir * speedMultiplier * Time.fixedDeltaTime;
        //rb.AddForce(dir);

        //rb.MovePosition(rb.position + dir);


        //playerObject.transform.Translate(speedMultiplier * Time.deltaTime * dir);
        //playerObject.transform.Translate(speedMultiplier * Time.deltaTime * dir.z);
        
        // Won't be zero if the player has moved
        /*if(dir != Vector3.zero)
        {
            // Rotate the player such that the forward direction of the player is the direction of travel
            playerObject.transform.LookAt(dir);
        }*/

        //playerObject.transform.Translate(playerObject.transform.forward * speedMultiplier * Time.deltaTime * dir);
    }

    //-------------------------------------------------------------------------------------
    //    ______     __  __                    ___        _____      __  __                
    //   / ____/__  / /_/ /____  __________   ( _ )      / ___/___  / /_/ /____  __________
    //  / / __/ _ \/ __/ __/ _ \/ ___/ ___/  / __ \/|    \__ \/ _ \/ __/ __/ _ \/ ___/ ___/
    // / /_/ /  __/ /_/ /_/  __/ /  (__  )  / /_/  <    ___/ /  __/ /_/ /_/  __/ /  (__  ) 
    // \____/\___/\__/\__/\___/_/  /____/   \____/\/   /____/\___/\__/\__/\___/_/  /____/                                                                                                                                                                                                                                                                                                                                                                                                                   
    //-------------------------------------------------------------------------------------

    public float GetHealth()
    {
        return health;
    }

    public Weapon GetWeapon()
    {
        return selectedWeapon;
    }

    public Vector3 GetPos()
    {
        return playerObject.transform.position;
    }

    public Vector3 GetBlinkPos()
    {
        return blinkLocation.position;
    }

    public Transform GetPlayerTransform()
    {
        return playerObject.transform;
    }
}   