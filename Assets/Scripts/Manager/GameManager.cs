using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Digimon Player;
    [SerializeField] List<Enemy> Enemies = new List<Enemy>();  // 모든 적을 관리할 리스트

    public void InitTarget(Enemy enemy)
    {
        enemy.target = Player.transform;
        AddEnemyToList(enemy);  // 적을 리스트에 추가
    }

    private void AddEnemyToList(Enemy enemy)
    {
        if (!Enemies.Contains(enemy))
        {
            Enemies.Add(enemy);
        }
    }

    public void WaitEvolutioning()
    {
        if (Player.isEvolutioning)
        {
            StopAllEnemies();
        }
        
    }

    public void OnEndEvolutioning()
    {
        ResumeAllEnemies();
    }
    private void StopAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.StopWhenEvolution();
        }
    }

    private void ResumeAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.ResumeEnemy();
        }
    }
}
