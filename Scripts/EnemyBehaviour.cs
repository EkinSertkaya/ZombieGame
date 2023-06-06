using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int health;
    InventorySystem gameManagerInventory;
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            PlayBloodSplashParticles();
            if (health <= 0)
            {
                ++InventorySystem.score;
                gameObject.layer = defaultLayerIndex;
                enemyRb.bodyType = RigidbodyType2D.Static;
                enemyBoxCollider2D.enabled = false;
                enemyAnimator.SetInteger("Jesus", -2);
                StartCoroutine(WaitForOneSecondToDie());
            }
        }
    }
    
    [SerializeField] int enemySpeed;
    int defaultLayerIndex;

    readonly float enemyMaxSpeed = 0.5f;

    [SerializeField] GameObject enemyDrop;

    Rigidbody2D enemyRb;
    Vector3 playerDirection;
    Transform playerTransform;
    Animator enemyAnimator;
    BoxCollider2D enemyBoxCollider2D;
    ParticleSystem bloodSplashParticleSystem;

    public Color cachedEnemyColor;

    private void Start()
    {
        gameManagerInventory = GameObject.Find("Game Manager").GetComponent<InventorySystem>();
        bloodSplashParticleSystem = transform.GetChild(1).GetComponent<ParticleSystem>();
        enemyBoxCollider2D = GetComponent<BoxCollider2D>();
        defaultLayerIndex = LayerMask.NameToLayer("Player");
        enemyAnimator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Helmet").GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody2D>();
        cachedEnemyColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        playerDirection = (playerTransform.position - transform.position).normalized;
            
        enemyRb.AddForce(new Vector3(enemySpeed * Time.deltaTime * playerDirection.x, 0f, 0f), ForceMode2D.Force);
        if(enemyRb.velocity.magnitude > enemyMaxSpeed)
        {
            enemyRb.velocity = Vector3.ClampMagnitude(enemyRb.velocity, enemyMaxSpeed);
        }
        EnemyFacingDirectionArrange();
    }

    

    IEnumerator WaitForOneSecondToDie()
    {
        float randomNumber = Random.Range(1f, 100f);
        int randomNumberInt = (int)randomNumber;
        Debug.Log(randomNumberInt);

        if(randomNumberInt >= 1 && randomNumberInt <= 5)
        {
            GameObject dropedItem = Instantiate(enemyDrop, transform.position, Quaternion.identity);
            Rigidbody2D dropedItemRb = dropedItem.GetComponent<Rigidbody2D>();
            dropedItemRb.AddForce(playerDirection * 5 + new Vector3(0f, 5f, 0f), ForceMode2D.Impulse);
        }
        else if (randomNumberInt >= 11 && randomNumberInt <= 21)
        {
            GameObject dropedItem = Instantiate(gameManagerInventory.weaponsAmmo[Random.Range(0, gameManagerInventory.weaponsAmmo.Length)], transform.position, Quaternion.identity);
            Rigidbody2D dropedItemRb = dropedItem.GetComponent<Rigidbody2D>();
            dropedItemRb.AddForce(playerDirection * 5 + new Vector3(0f, 5f, 0f), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);    
    }

    void PlayBloodSplashParticles()
    {
        bloodSplashParticleSystem.Play();
    }

    void EnemyFacingDirectionArrange()
    {
        if(playerDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if(playerDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    
}
