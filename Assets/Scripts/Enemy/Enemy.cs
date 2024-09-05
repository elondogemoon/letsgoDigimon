using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IHIt
{
    public Transform target { get;  set; }
    public Animator animator;
    public int Hp { get; set; }

    public float CurrentHp;

    public int Damage { get; set; }
    public float AtkRange { get; set; }
    public float CoolTime { get; set; }
    public float LastAttackTime { get; set; }

    public NavMeshAgent _nav;
    private IState _enemyState;
    public BoxCollider collider;
    private void Awake()
    {
        Hp = 100;
        CurrentHp = Hp;
        Damage = 5;
        AtkRange = 1;
        CoolTime = 3;
        GameManager.Instance.InitTarget(this);
    }
    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<BoxCollider>();
        ChangeState(new MonsterEnter(this));
    }

    private void Update()
    {
        _enemyState.ExecuteOnUpdate();
    }
    public void ChangeState(IState newState)
    {
        _enemyState?.ExitState();
        _enemyState = newState;
        _enemyState.EnterState();
    }

    public void Hit(float damage)
    {
        animator.SetTrigger("Damaged");
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            ObjectPoolManager.Instance.EnqueueObject(gameObject);
        }
        Debug.Log($"Hp :  {CurrentHp}");
    }

}

