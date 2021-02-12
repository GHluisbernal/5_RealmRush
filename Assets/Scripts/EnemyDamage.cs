using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 10;


    private void OnParticleCollision(GameObject other)
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
