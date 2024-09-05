using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Enemy _enemy;
    [SerializeField] Transform[] _spawnPoint;
    private void Start()
    {
        ObjectPoolManager.Instance.CreatePool(enemy, 10);
        Spawn();
    }
    
    public void Spawn()
    {
        ObjectPoolManager.Instance.DequeueObject(enemy);
    }
}
