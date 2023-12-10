using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    public Transform blinkLocation;
    public Transform handsLocation;
    public Transform playerObject;
    public Rigidbody rb;
    public TrailRenderer tr;

    [Header("Stats")]
    public float health = 100f; // Player health

    [Header("Movement")]
    public float speedMultiplier = 25f; // Adjust the speed of the player, 25 works well

    [Header("Blink")]
    public float blinkDistance = 5f; // Adjust how far the player blinks
    public float blinkCooldown = 5f; // Adjust the cooldown of the blink ability in seconds
    private bool canBlink = true;

    private bool canAttack = true;

    public float handsDistance = 1f; // Adjust how far the player's hands are

    enum Weapon {GrenadeLauncher, Railgun, Sword};
    Weapon selectedWeapon = new Weapon();

    public delegate void ShootAction();
    public static event ShootAction onShoot;

    private Vector3 dir = new Vector3(); // Store the movement direction of the player
    
    void Start()
    {
        selectedWeapon = Weapon.GrenadeLauncher;

        dir = Vector3.zero;

        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;

    }

    void Update()
    {
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

        if(Input.GetMouseButtonDown(0))
        {
            if(onShoot != null)
            {
                onShoot();
            }
            else
            {
                Debug.Log("Failed to invoke event");
            }
        }
    }

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

    private void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        playerObject.transform.position += speedMultiplier * Time.deltaTime * dir;
    
        if(dir != Vector3.zero)
        {
            playerObject.transform.forward = dir;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public string GetWeapon()
    {
        return selectedWeapon.ToString();
    }

    public Vector3 GetPos()
    {
        return playerObject.transform.position;
    }

    public Vector3 GetBlinkPos()
    {
        return blinkLocation.position;
    }

    public Vector3 GetHandsPos()
    {
        return handsLocation.transform.position;
    }

    public Transform GetPlayerTransform()
    {
        return playerObject.transform;
    }
}   