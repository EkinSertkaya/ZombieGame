using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    Vector3 backgroundOffSet = new Vector3(0f, 0f, 10f);

    PlayerControls playerControlsScript;

    [SerializeField] float backgroundZeroSpeedMod= 1f;
    [SerializeField] float backgroundOneSpeedMod = 0.4f;
    [SerializeField] float backgroundTwoSpeedMod = 0.2f;
    [SerializeField] float backgroundThreeSpeedMod = 0.1f;

    Renderer backgroundZeroRenderer;
    Renderer backgroundOneRenderer;
    Renderer backgroundTwoRenderer;
    Renderer backgroundThreeRenderer;


    private void Start()
    {
        playerControlsScript = GameObject.Find("Helmet").GetComponent<PlayerControls>();
        backgroundZeroRenderer = transform.GetChild(0).GetComponent<Renderer>();
        backgroundOneRenderer = transform.GetChild(1).GetComponent<Renderer>();
        backgroundTwoRenderer = transform.GetChild(2).GetComponent<Renderer>();
        backgroundThreeRenderer = transform.GetChild(3).GetComponent<Renderer>();
    }

    private void Update()
    {
        backgroundZeroRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundZeroSpeedMod * Camera.main.transform.position.x, 0));
        backgroundOneRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundOneSpeedMod * Camera.main.transform.position.x, 0));
        backgroundTwoRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundTwoSpeedMod * Camera.main.transform.position.x, 0));
        backgroundThreeRenderer.material.SetTextureOffset("_MainTex", new Vector2(backgroundThreeSpeedMod * Camera.main.transform.position.x, 0));
    }

    void LateUpdate()   
    {
        transform.position = Camera.main.transform.position + backgroundOffSet;
    }
}
