using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;


    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        hitPoints--;

        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

}
