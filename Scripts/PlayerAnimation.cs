using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator playerAnimator;
    PlayerControls playerControlsScript;


    // Start is called before the first frame update
    void Start()
    {
        playerControlsScript = GetComponent<PlayerControls>();
        playerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnimaiton();
    }

    void PlayAnimaiton()
    {
        if(Mathf.Abs(playerControlsScript.movementDirection.x) > 0 && playerControlsScript.isGrounded)
        {
            playerAnimator.SetInteger("Messi", 2);
        }
        else if(Mathf.Abs(playerControlsScript.movementDirection.x) == 0)
        {
            playerAnimator.SetInteger("Messi", 0);
        }
        else if (!playerControlsScript.isGrounded)
        {
            playerAnimator.SetInteger("Messi", 0);
        }
            
    }
}
