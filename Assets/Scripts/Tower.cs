using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform enemy;
    [SerializeField] float attackRange = 30;
    ParticleSystem bulletParticleSystem;

    private void Awake()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
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
