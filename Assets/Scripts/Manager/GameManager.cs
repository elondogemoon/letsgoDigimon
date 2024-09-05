using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Digimon Player;
    [SerializeField] Enemy Enemy;

    public void InitTarget(Enemy enemy)
    {
        enemy.target = Player.transform;
    }
}
