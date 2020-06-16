using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] Transform parent;


    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void OnParticleCollision(GameObject other)
    {
        hitFX.Play();
        hitPoints--;

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

        Destroy(gameObject);
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

}
