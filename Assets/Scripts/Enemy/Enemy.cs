using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IHIt
{
    public Transform target;
    public int Hp { get; set; }
    
    public float CurrentHp { get; set; }

    public int Damage { get; set; }
    public NavMeshAgent _nav;

    private float AtkRange;
    private IState _enemyState;
    private void Awake()
    {
        Hp = 100;
        CurrentHp = Hp;
        Damage = 5;
    }
    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        ChangeState(new MonsterEnter(this));
    }

    public void ChangeState(IState newState)
    {
        _enemyState?.ExitState();
        _enemyState = newState;
        _enemyState.EnterState();
    }
    public void Hit(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            gameObject.SetActive(false);
        }
        Debug.Log($"Hp :  {CurrentHp}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IHIt hit = other.GetComponent<IHIt>();
            hit.Hit(Damage);
            _nav.SetDestination(this.transform.position);
            _nav.velocity = Vector3.zero;
        }
    }

}

