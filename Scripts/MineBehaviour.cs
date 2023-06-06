using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    public MineExplosionLogic mineExplosionLogic;

    private void Start()
    {
        mineExplosionLogic = gameObject.transform.GetChild(1).GetComponent<MineExplosionLogic>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
