using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Digimon Player;
    [SerializeField] List<Enemy> Enemies = new List<Enemy>();  // ��� ���� ������ ����Ʈ

    public void InitTarget(Enemy enemy)
    {
        enemy.target = Player.transform;
        AddEnemyToList(enemy);  // ���� ����Ʈ�� �߰�
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
