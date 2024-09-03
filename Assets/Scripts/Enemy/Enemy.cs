using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IHIt
{
    public int Hp { get; set; }
    
    public float CurrentHp { get; set; }

    public int Damage { get; set; }

    private float AtkRange;
    [SerializeField] Transform target;
    private NavMeshAgent _nav;

    private void Awake()
    {
        Hp = 100;
        CurrentHp = Hp;
        Damage = 5;
    }
    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
        _nav.SetDestination(target.transform.position);
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

