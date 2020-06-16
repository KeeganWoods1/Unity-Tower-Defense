using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] Transform objectToPan;
    [SerializeField] float fireRadius = 21f;
    [SerializeField] ParticleSystem cannonballs;

    Transform targetEnemy;
    Waypoint baseWaypoint;


    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        AimAtEnemy();
        Fire();
    }

    public Waypoint GetBaseWaypoint()
    {
        return baseWaypoint;
    }

    public void SetBaseWaypoint(Waypoint waypoint)
    {
        baseWaypoint = waypoint;
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyHealth>();

        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyHealth testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private void AimAtEnemy()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
        }
    }

    private Transform GetClosest(Transform closestEnemy, Transform testTransform)
    {
        float distanceToClosest = Vector3.Distance(closestEnemy.position, transform.position);
        float distanceToTest = Vector3.Distance(testTransform.position, transform.position);

        if (distanceToClosest > distanceToTest)
        {
            closestEnemy = testTransform;
        }

        return closestEnemy;

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
