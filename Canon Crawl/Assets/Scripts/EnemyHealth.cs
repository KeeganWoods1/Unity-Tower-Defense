using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] Transform parent;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    Scoreboard scoreboard;


    // Start is called before the first frame update
    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        AddNonTriggerBoxCollider();
    }

    private void OnParticleCollision(GameObject other)
    {
        hitFX.Play();

        AudioSource.PlayClipAtPoint(enemyHitSFX, Camera.main.transform.position, 0.1f);

        hitPoints--;

        scoreboard.UpdatePointsCount(3);

        if(hitPoints <= 0)
        {
            DeathSequence(deathFX);
        }
    }

    public void DeathSequence(ParticleSystem deathFX)
    {
        var vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.Play();

        Destroy(vfx.gameObject, vfx.main.duration);

        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);

        scoreboard.DecrementEnemyCount();

        Destroy(gameObject);
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

}
