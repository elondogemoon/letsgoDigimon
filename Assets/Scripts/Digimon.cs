using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UpgradeState
{
    low,
    middle,
    high,
}
public class Digimon : MonoBehaviour,IHIt
{
    public int MaxHP { get; set; }
    public int CurrentHp { get; set; }
    public int MaxMP { get; set; }
    public int CurrentMP { get; set; }  
    public int CoolDown { get; set; }
    [SerializeField] public float EvolutionGauge;
    [SerializeField] UpgradeState _upgradeState;
    private void Awake()
    {
        if (_upgradeState == UpgradeState.low)
        {
            MaxHP = 100;
            MaxMP = 100;
            CurrentHp = MaxHP;
            CurrentMP = 0;
            CoolDown = 0;
        }
        if(_upgradeState == UpgradeState.middle)
        {
            MaxHP = 200;
            MaxMP = 200;
            CurrentHp = MaxHP;
            CurrentMP = 0;
            CoolDown = 0;
        }
        if( _upgradeState == UpgradeState.high)
        {
            MaxHP = 300;
            MaxMP = 250;
            CurrentHp = MaxHP;
            CurrentMP = 0;
            CoolDown = 0;
        }
    }



    public void Hit(int damage)
    {

    }
}
