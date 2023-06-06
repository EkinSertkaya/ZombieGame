using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] int weaponIndex;
    TextMeshProUGUI ammoText;
    InventorySystem gameManagerInventory;

    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
        gameManagerInventory = GameObject.Find("Game Manager").GetComponent<InventorySystem>();
    }

    void Update()
    {
        if(weaponIndex == 0)
        {
            ammoText.text = "AK = " + gameManagerInventory.akBullets;
        }
        if (weaponIndex == 1)
        {
            ammoText.text = "Shotgun = " + gameManagerInventory.shotgunBullets;
        }
        if (weaponIndex == 2)
        {
            ammoText.text = "Sniper = " + gameManagerInventory.sniperBullets;
        }
        if (weaponIndex == 3)
        {
            ammoText.text = "Minigun = " + gameManagerInventory.minigunBullets;
        }
    }
}
