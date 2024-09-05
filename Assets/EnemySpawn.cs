using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    [SerializeField] Digimon player;
    [SerializeField] Enemy _enemy;
    
    private void Start()
    {
        _enemy.target = player.transform;
        ObjectPoolManager.Instance.CreatePool(enemy, 10);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPoolManager.Instance.DequeueObject(enemy,this.transform.position);
        }
    }


}
