using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] itemDrops;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")    
        {
            Instantiate(itemDrops[Random.Range(0, 3)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
