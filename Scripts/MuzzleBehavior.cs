using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MuzzleBehavior : MonoBehaviour
{
    [SerializeField] float muzzleDisableTime;

    private void OnEnable()
    {
        StartCoroutine(MuzzleFlashWaitTime());
    }

    
    IEnumerator MuzzleFlashWaitTime()
    {
        yield return new WaitForSeconds(0.02f);
        gameObject.SetActive(false);
    }







}
