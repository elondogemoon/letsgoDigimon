using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum UpgradeState
{
    low,
    middle,
    high,
}

public class Digimon : MonoBehaviour, IHIt
{
    public float MaxHP { get; set; }
    public float CurrentHp { get; set; }
    public int MaxMP { get; set; }
    public int CurrentMP { get; set; }
    public int CoolDown { get; set; }
    public float Damage { get; set; }

    public float SkillDamage { get; set; }

    public int _evolutionNum;

    private int _currentEvolutionNum;

    [SerializeField] public float EvolutionGauge;

    [SerializeField] private UpgradeState _upgradeState;

    Animator animator;
    Collider atkCollider;
    protected virtual void Awake()
    {
        ApplyUpgradeState();
        animator = GetComponentInChildren<Animator>();
        atkCollider = GetComponentInChildren<Collider>();
        _evolutionNum = 1;
        _currentEvolutionNum = 0;
    }

    private void ApplyUpgradeState()
    {
        switch (_upgradeState)
        {
            case UpgradeState.low:
                MaxHP = 100;
                MaxMP = 100;
                Damage = 10;
                SkillDamage = 20;
                break;
            case UpgradeState.middle:
                MaxHP = 200;
                MaxMP = 200;
                Damage = 20;
                SkillDamage = 40;
                break;
            case UpgradeState.high:
                MaxHP = 300;
                MaxMP = 250;
                Damage = 40;
                SkillDamage = 80;
                break;
        }

        CurrentHp = MaxHP;
        CurrentMP = 0;
        CoolDown = 0;
    }

    public void Hit(float damage)
    {
        CurrentHp -= damage;
        animator.SetTrigger("Damaged");
        Debug.Log("hithit");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IHIt hit = GetComponent<IHIt>();
            hit.Hit(Damage);
            Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.LookAt(target);
            animator.SetTrigger("Atk");
            StartCoroutine(AtkColliderOn());
        }
    }


    public void OnEvolution()
    {
        StartCoroutine(EvolutionStart());

        ApplyUpgradeState();

        transform.GetChild(_currentEvolutionNum).gameObject.SetActive(false);
        transform.GetChild(_evolutionNum).gameObject.SetActive(true);
        
        _currentEvolutionNum++; 
        _evolutionNum++;
    }

    public IEnumerator EvolutionStart()
    {
        Vector3 finalRotation = new Vector3(0, 180, 0); // 진화가 끝날 때의 목표 회전값
        float duration = 10f; // 총 지속 시간
        float initialSpeed = 10f; // 초기 회전 속도
        float speedIncrease = 400f; // 회전 속도 증가율
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
           
            float currentSpeed = initialSpeed + (speedIncrease * (elapsedTime / duration));

            float rotationAngle = currentSpeed * Time.deltaTime;

            transform.Rotate(0, rotationAngle, 0);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return transform.DORotate(finalRotation, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

        if (_upgradeState == UpgradeState.low)
        {
            _upgradeState = UpgradeState.middle;
        }
        else if (_upgradeState == UpgradeState.middle)
        {
            _upgradeState = UpgradeState.high;
        }
    }
    IEnumerator AtkColliderOn()
    {
        atkCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        atkCollider.enabled = false;
    }
    //TODO : 전투 방식, Json 저장, 뽑기, UI 


}
