using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] float spawnInterval;

    IEnumerator EnemySpawn()
    {
        int spawnedEnemyCounter = 0;
        bool isSpawning = true;

        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(enemy1, transform.position, Quaternion.identity);
            spawnedEnemyCounter++;
            /*if(spawnedEnemyCounter >= 14)
            {
                StopCoroutine(EnemySpawn());
                isSpawning = false;
            }*/
        }
    }

    private void OnEnable()
    {
        StartCoroutine(EnemySpawn());
    }

}
