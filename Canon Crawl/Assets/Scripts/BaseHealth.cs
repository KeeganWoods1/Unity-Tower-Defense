using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int baseHitPoints = 10;
    [SerializeField] ParticleSystem firstExplosionFX;
    [SerializeField] ParticleSystem secondExplosionFX;
    [SerializeField] ParticleSystem thirdExplosionFX;
    [SerializeField] AudioClip baseExplosionSFX;
    Scoreboard scoreboard;
    int damageMagnitude = 3;

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    public void TakeDamage()
    {
        int deathThreshold = damageMagnitude + 1;

        if (baseHitPoints >= deathThreshold)
        {
            baseHitPoints -= damageMagnitude;
            scoreboard.UpdateBaseHP(damageMagnitude);
        }

        else
        {
            scoreboard.UpdateBaseHP(damageMagnitude);
            BaseExplosionSequence();
        }
    }

    private void BaseExplosionSequence()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(baseExplosionSFX);
        firstExplosionFX.Play();
        secondExplosionFX.Play();
        thirdExplosionFX.Play();

        Invoke("ReloadLevel", 0.5f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
}
