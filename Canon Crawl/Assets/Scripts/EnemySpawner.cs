﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(1f, 5f)][SerializeField] float secondsbetweenSpawns = 1f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemies"); 
    }

    IEnumerator  SpawnEnemies()
    {
        while (numberOfEnemies > 0)
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = parent;
            numberOfEnemies--;
            yield return new WaitForSeconds(secondsbetweenSpawns);
        }

    }
}
