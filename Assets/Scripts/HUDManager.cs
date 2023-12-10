using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public TMP_Text healthLabel;
    public TMP_Text weaponLabel;
    void Start()
    {
        updateHealth();
        updateWeapon();
    }

    void Update()
    {
        updateHealth();
        updateWeapon();
    }

    void updateHealth()
    {
        if(healthLabel.text != "Health: " + playerController.GetHealth().ToString())
        {
            healthLabel.text = "Health: " + playerController.GetHealth().ToString();
        }
    }

    void updateWeapon()
    {
        if(weaponLabel.text != playerController.GetWeapon())
        {
            weaponLabel.text = playerController.GetWeapon();
        }
    }
}
