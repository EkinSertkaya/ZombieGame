using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosionBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroySelf());  
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
