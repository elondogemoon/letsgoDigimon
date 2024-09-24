using System.Collections;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject egg;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] float spawnInterval = 2f;
    private bool spawning = true;
    private int enemiesToSpawn = 5; 
    private int spawnedEnemiesCount = 0;

    private void Start()
    {
        ObjectPoolManager.Instance.CreatePool(enemy, 10);
        ObjectPoolManager.Instance.CreatePool(egg, 10);
        ObjectPoolManager.Instance.CreatePool(hitEffect, 10);
    }

    public void StartWave()
    {
        spawnedEnemiesCount = 0;
        StartCoroutine(SpawnRoutine());
    }

    public void HitEffectOn(Transform target)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(hitEffect, target.position);

        if (effect != null)
        {
            effect.SetActive(true);
            ObjectPoolManager.Instance.EnqueueObject(effect, 0.5f);
        }
    }


    public IEnumerator SpawnRoutine()
    {
        while (spawning && spawnedEnemiesCount < enemiesToSpawn)
        {
            Spawn();
            spawnedEnemiesCount++;
            yield return new WaitForSeconds(spawnInterval);
        }

        // 모든 적이 스폰되면 스폰 루틴 종료
        spawning = false;
    }

    public void SpawnEx()
    {
        spawning = true;
        StartWave(); // 새 웨이브 시작
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
        spawning = false;
    }

    public void Spawn()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomIndex];

        GameObject spawnedEnemy = ObjectPoolManager.Instance.DequeueObject(enemy);
        if (spawnedEnemy != null)
        {
            spawnedEnemy.transform.position = spawnPoint.position;
            spawnedEnemy.SetActive(true);

            Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                GameManager.Instance.InitTarget(enemyComponent);
            }
        }
    }
}
