using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float fireRadius = 21f;
    [SerializeField] ParticleSystem cannonballs;



    // Update is called once per frame
    void Update()
    {
        TargetEnemy();
        Fire();
    }

    private void TargetEnemy()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
        }
    }

    private void Fire()
    {
        var cannonParticleEmissionModule = cannonballs.GetComponent<ParticleSystem>().emission;

        if (!targetEnemy)
        {
            cannonParticleEmissionModule.enabled = false;
            return;
        }

        

        Vector3 enemyPos = targetEnemy.position;
        float enemyDistance = Vector3.Distance(enemyPos, transform.position);

        if (enemyDistance <= fireRadius)
        {
            cannonParticleEmissionModule.enabled = true;
        }

        else
        {
            cannonParticleEmissionModule.enabled = false;
        }


    }
}
