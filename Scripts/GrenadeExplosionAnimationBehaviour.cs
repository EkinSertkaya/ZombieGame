using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosionAnimationBehaviour : MonoBehaviour
{
    Animator grenadeExplosionAnimation;
    AudioSource gameplayAudioSource;
    CameraBehaviour cameraBehaviourScript;

    private void Start()
    {
        cameraBehaviourScript = Camera.main.gameObject.GetComponent<CameraBehaviour>();
        gameplayAudioSource = GameObject.Find("Gameplay Audio Source").GetComponent<AudioSource>();
        grenadeExplosionAnimation = GetComponent<Animator>();
        grenadeExplosionAnimation.SetBool("Exploded", true);
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        gameplayAudioSource.PlayOneShot(cameraBehaviourScript.grenadeExplosionSFX);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
