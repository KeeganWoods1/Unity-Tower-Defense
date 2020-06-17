using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Text enemySpawnedtext;
    [SerializeField] Text pointsText;
    [SerializeField] Text baseHPText;
    [SerializeField] Text winText;
    int numOfEnemies = 0;
    int points = 0;
    int baseHP = 10;
    public bool enemiesStillSpawning;
    public bool isVictory = true;

    private void Start()
    {
        enemySpawnedtext.text = "Enemies: " + numOfEnemies.ToString();
        pointsText.text = "Points: " + points.ToString();
        baseHPText.text = "HP: " + baseHP.ToString();
    }

    private void Update()
    {
        DisplayWinText();
    }

    public void IncrementEnemyCount()
    {
        numOfEnemies++;
        enemySpawnedtext.text = "Enemies: " + numOfEnemies.ToString();
    }

    public void DecrementEnemyCount()
    {
        numOfEnemies--;
        enemySpawnedtext.text = "Enemies: " + numOfEnemies.ToString();
    }

    public void UpdatePointsCount(int addedPoints)
    {
        points += addedPoints;
        pointsText.text = "Points: " + points.ToString();
    }
    public void UpdateBaseHP(int subbedPoints)
    {
        int deathThreshold = subbedPoints + 1;

        if (baseHP >= deathThreshold)
        {
            baseHP -= subbedPoints;
            baseHPText.text = "HP: " + baseHP.ToString();
        }

        else
        {
            baseHPText.text = "HP: 0";
        }

    }

    public void DisplayWinText()
    {
        if (numOfEnemies <= 0 && !enemiesStillSpawning && isVictory)
        {
            winText.enabled = true;
            Invoke("ReloadScene", 2f);

        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
