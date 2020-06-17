using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(1f, 5f)][SerializeField] float secondsbetweenSpawns = 1f;
    [SerializeField] int maxNumberOfEnemies = 5;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform parent;
    [SerializeField] AudioClip spawnedEnemySFX;
    Scoreboard scoreboard;
    int numberOfEnemies = 0;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        StartCoroutine("SpawnEnemies");
    }

    IEnumerator  SpawnEnemies()
    {
        while (numberOfEnemies < maxNumberOfEnemies)
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = parent;
            numberOfEnemies++;
            scoreboard.IncrementEnemyCount();
            gameObject.GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            yield return new WaitForSeconds(secondsbetweenSpawns);
        }

    }
}
