using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsbetweenSpawns = 1f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] EnemyMovement enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemies"); 
    }

    IEnumerator  SpawnEnemies()
    {
        while (numberOfEnemies > 0)
        {
            Instantiate(enemyPrefab);
            numberOfEnemies--;
            yield return new WaitForSeconds(secondsbetweenSpawns);
        }

    }
}
