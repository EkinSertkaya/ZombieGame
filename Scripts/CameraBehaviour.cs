using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] public AudioClip mineExplosionSFX;
    [SerializeField] public AudioClip grenadeExplosionSFX;
    [SerializeField] public AudioClip AK47ShottingSFX;
    [SerializeField] public AudioClip minigunShootingSFX;
    [SerializeField] public AudioClip sniperShootingSFX;
    [SerializeField] public AudioClip shotgunShootingSFX;

    GameObject playerObject;

    Vector3 cameraOffSet = new Vector3(0f, 0f, -10f);
    [SerializeField] float cameraDelay;

    private void Start()
    {
        
        playerObject = GameObject.Find("Helmet");
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp( transform.position,new Vector3(playerObject.transform.position.x, 0f) + cameraOffSet, cameraDelay * Time.deltaTime);
    }
}
