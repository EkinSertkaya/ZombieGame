using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] int itemCode; // 0 = double damage, 1 = double speed, 2 = mines

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (itemCode == 0)
            {
                ++InventorySystem.doubleDamageCount;
            }
            else if (itemCode == 1)
            {
                ++InventorySystem.grenadesCount;
            }
            else if (itemCode == 2)
            {
                ++InventorySystem.minesCount;
            }
            Destroy(gameObject);
        }
    }
}
