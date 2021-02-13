using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private float attackRange = 30;

    private ParticleSystem bulletParticleSystem;
    private Transform enemy;
    internal Waypoint baseWaypoint;

    private void Awake()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        SetTargetEnemy();
        if (enemy)
        {
            LookAtEnemy();
            FireAtEnemy();
        }
        else
        {
            ToggleTowerProjectile(false);
        }
    }

    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyDamage>();
        if (enemies.Length == 0) { return;  }

        var bestEnemy = enemies[0];
        var bestDistance = Vector3.Distance(transform.position, bestEnemy.transform.position);
        foreach (var enemyOnScene in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemyOnScene.transform.position);
            if (distance < bestDistance)
            {
                bestEnemy = enemyOnScene;
                bestDistance = distance;
            }
        }

        enemy = bestEnemy.transform;
    }

    private void FireAtEnemy()
    {
        var distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (distance <= attackRange)
        {
            ToggleTowerProjectile(true);
        }
        else
        {
            ToggleTowerProjectile(false);
        }
    }

    private void ToggleTowerProjectile(bool isActive)
    {
        var emissionModule = bulletParticleSystem.emission;
        emissionModule.enabled = isActive;
    }

    private void LookAtEnemy()
    {
        objectToPan.LookAt(enemy);
    }
}
