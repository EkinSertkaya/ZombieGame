using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedAmmoBehaviour : MonoBehaviour
{
    InventorySystem gameManagerInventory;
    [SerializeField] int weaponCode; // AK = 0, Minigun = 1, Shotgun = 2, Sniper = 3
    [SerializeField] int dropedAKAmmo;
    [SerializeField] int dropedMinigunAmmo;
    [SerializeField] int dropedShotgunAmmo;
    [SerializeField] int dropedSniperAmmo;

    private void Start()
    {
        gameManagerInventory = GameObject.Find("Game Manager").GetComponent<InventorySystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (weaponCode == 0)
            {
                gameManagerInventory.akBullets += dropedAKAmmo;
                Destroy(gameObject);
            }
            else if (weaponCode == 1)
            {
                gameManagerInventory.minigunBullets += dropedMinigunAmmo;
                Destroy(gameObject);
            }
            else if (weaponCode == 2)
            {
                gameManagerInventory.shotgunBullets += dropedShotgunAmmo;
                Destroy(gameObject);
            }
            else if (weaponCode == 3)
            {
                gameManagerInventory.sniperBullets += dropedSniperAmmo;
                Destroy(gameObject);
            }
        }
    }
}
