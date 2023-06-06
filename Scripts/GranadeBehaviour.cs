using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject explosionAnimation;
 
    int explosionLayer;
    CircleCollider2D explosionRadius;

    private void Start()
    {
        explosionLayer = LayerMask.NameToLayer("Player");
        explosionRadius = GetComponentInChildren<CircleCollider2D>();
        StartCoroutine(Explosion());
    }
    

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(explosionAnimation, transform.position, Quaternion.identity);
        explosionRadius.gameObject.layer = explosionLayer;
        while (gameObject)
        {
            explosionRadius.radius += 0.01f;
            yield return new WaitForSeconds(0.001f);
            if(explosionRadius.radius >= 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
