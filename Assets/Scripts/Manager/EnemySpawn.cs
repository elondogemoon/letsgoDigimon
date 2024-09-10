using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject egg;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] float spawnInterval = 2f;
    private bool spawning = true;

    private void Start()
    {
        ObjectPoolManager.Instance.CreatePool(enemy, 10);
        ObjectPoolManager.Instance.CreatePool(egg, 10);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (spawning)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
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
