using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRayCast : MonoBehaviour
{
    readonly int layerMask = ~(1 << 3) & ~(1 << 7);

    [SerializeField] public int baseDamage;
    [SerializeField] public int damage;

    GameObject playerObject;

    RaycastHit2D raycastHitInfo;

    EnemyBehaviour enemyBehaviourScript;

    SpriteRenderer enemySpriteRenderer;

    Rigidbody2D enemyRB;

    Animator enemyAnimator;

    Vector3 rayStartPoint;
    Vector3 enemyDirection;

    private void Start()
    {
        playerObject = GameObject.Find("Helmet");
    }

    private void Update()
    {
        RayCastDirection();
    }

    void RayCastDirection()
    {
        if (playerObject.transform.localScale.x > 0)
        {
            raycastHitInfo = Physics2D.Raycast(transform.position, transform.right, 10f, layerMask);
            Debug.DrawRay(transform.position, transform.right * 10);
        }
        else if (playerObject.transform.localScale.x < 0)
        {
            raycastHitInfo = Physics2D.Raycast(transform.position, -transform.right, 10f, layerMask);
            Debug.DrawRay(transform.position, -transform.right * 10);
        }
        if (raycastHitInfo)
        {
            //Debug.Log(raycastHitInfo.collider.gameObject.name);
        }
    }

    public void ApplyDamage()
    {
        if(raycastHitInfo.collider != null && raycastHitInfo.collider.gameObject.tag == "Enemy")
        {
            enemyBehaviourScript = raycastHitInfo.collider.gameObject.GetComponent<EnemyBehaviour>();
            enemySpriteRenderer = raycastHitInfo.collider.gameObject.GetComponent<SpriteRenderer>();
            enemyRB = raycastHitInfo.collider.gameObject.GetComponent<Rigidbody2D>();

            EnemyPushed();
            StartCoroutine(EnemyDamageColorChange());
            enemyBehaviourScript.Health -= damage;
        }   
    }

    IEnumerator EnemyDamageColorChange()
    {
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.04f);
        if (enemySpriteRenderer)
        {
            enemySpriteRenderer.color = enemyBehaviourScript.cachedEnemyColor;
        }
    }

    void EnemyPushed()
    {
        rayStartPoint = gameObject.transform.position;
        enemyDirection = (raycastHitInfo.collider.transform.position - rayStartPoint).normalized;
        enemyRB.AddForce(enemyDirection * 500f, ForceMode2D.Force);
        EnemyHitAnimation();
    }

    void EnemyHitAnimation()
    {
            enemyAnimator = raycastHitInfo.collider.GetComponent<Animator>();
            enemyAnimator.Play("hurt");
    }
}
