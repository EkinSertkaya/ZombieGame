using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float rateOfFire;

    

    private bool canShoot;

    private void Start()
    {
        CanShoot = false;
    }

    public bool CanShoot
    {
        get
        {
            return canShoot;
        }
        set
        {
            canShoot = value;
            if (!value)
            {
                StartCoroutine(RateOfFire());
            }
        }
    }

    IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

   

      
}

    






