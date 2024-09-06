using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnAtk : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IHIt hit = other.GetComponent<IHIt>();
            hit.Hit(enemy.Damage);
        }
    }
}
