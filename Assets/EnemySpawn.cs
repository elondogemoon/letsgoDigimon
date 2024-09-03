using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.CreatePool(enemy, 10);   
        ObjectPoolManager.Instance.DequeueObject(enemy,this.transform.position);
    }

    
}
