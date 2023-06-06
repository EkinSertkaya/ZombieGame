using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSensorBehaviour : MonoBehaviour
{
    MineBehaviour mineBehaviour;

    CameraBehaviour cameraBehaviourScript;

    AudioSource gameplayAudioSource;

    [SerializeField] GameObject mineExplosionVFX;


    int targetsInRadius = 0;

    private int TargetsInRadius
    {
        get
        {
            return targetsInRadius;
        }

        set
        {
            targetsInRadius = value;

            if(targetsInRadius >= 5)
            {
                Instantiate(mineExplosionVFX, transform.position + new Vector3(0f, 2.5f, 0f), Quaternion.Euler(0f, 0f, 90f));
                gameplayAudioSource.PlayOneShot(cameraBehaviourScript.mineExplosionSFX);
                mineBehaviour.mineExplosionLogic.MineExplosionStarter();    
            }
        }
    }

    private void Start()
    {
        gameplayAudioSource = GameObject.Find("Gameplay Audio Source").GetComponent<AudioSource>();
        cameraBehaviourScript = Camera.main.gameObject.GetComponent<CameraBehaviour>();
        mineBehaviour = GetComponentInParent<MineBehaviour>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            ++TargetsInRadius;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            --TargetsInRadius;
        }
    }
}
