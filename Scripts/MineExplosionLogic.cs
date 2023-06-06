using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosionLogic : MonoBehaviour
{
    CircleCollider2D mineExplosionCollider;

    

    private void Start()
    {
        mineExplosionCollider = GetComponent<CircleCollider2D>();
    }

    IEnumerator MineExplosion()
    {
        mineExplosionCollider.enabled = true;

        while (gameObject)
        {
            mineExplosionCollider.radius += 0.05f;
            yield return new WaitForSeconds(0.001f);
            if (mineExplosionCollider.radius >= 5f)
            {
                
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void MineExplosionStarter()
    {
        StartCoroutine(MineExplosion());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().Health = 0;
            Destroy(other.gameObject);
        }
    }
}