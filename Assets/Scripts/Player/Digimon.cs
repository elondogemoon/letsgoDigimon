using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum UpgradeState
{
    low,
    middle,
    high,
}

public class Digimon : MonoBehaviour, IHIt
{
    [SerializeField] GameObject EvolutionEffect;

    [SerializeField] GameObject LowSkillPrefab;
    [SerializeField] GameObject MiddleSkillPrefab;
    [SerializeField] GameObject HighSkillPrefab;
    public float MaxHP { get; set; }
    public float CurrentHp { get; set; }
    public int MaxMP { get; set; }
    public int CurrentMP { get; set; }
    public int CoolTime { get; set; }
    public float Damage { get; set; }
    public float SkillDamage { get; set; }
    public float LastAttackTime { get; set; }
    public bool isEvolutioning { get; set; }
    public float TargetDistance { get; set; }
    public float Rarity { get; set; }

    public float EvolutionGauge;
    public int _evolutionNum;
    protected int _currentEvolutionNum;

    public UpgradeState _upgradeState;
    public Animator animator;
    public BoxCollider atkCollider;

    public Skill _currentSkill;
    private IState _playerState;

    
    protected virtual void Awake()
    {
        ApplyUpgradeState();
        animator = GetComponentInChildren<Animator>();
        atkCollider = GetComponentInChildren<BoxCollider>();
        _evolutionNum = 1;
        _currentEvolutionNum = 0;
        _upgradeState = UpgradeState.low;
    }

    private void Start()
    {
        ChangeState(new PlayerEnter(this));
    }

    public virtual void Update()
    {
        _playerState.ExecuteOnUpdate();
    }

    public virtual void ChangeState(IState newState)
    {
        _playerState?.ExitState();
        _playerState = newState;
        _playerState.EnterState();
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
                _currentSkill = new Skill(LowSkillPrefab);
                break;
            case UpgradeState.middle:
                MaxHP = 200;
                MaxMP = 200;
                Damage = 20;
                SkillDamage = 40;
                _currentSkill = new Skill(MiddleSkillPrefab);
                break;
            case UpgradeState.high:
                MaxHP = 300;
                MaxMP = 250;
                Damage = 40;
                SkillDamage = 80;
                _currentSkill = new Skill(HighSkillPrefab);
                break;
        }
        CurrentHp = MaxHP;
        CurrentMP = 100;
    }


    public void Hit(float damage)
    {
        CurrentHp -= damage;
        EvolutionGauge += 15;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.LookAt(target);
        }
    }

    public void ActiveSkill()
    {
        ChangeState(new PlayerSkill(this));
    }

    public void OnEvolution()
    {
        isEvolutioning = true;
        StartCoroutine(EvolutionStart());
    }

    protected virtual IEnumerator EvolutionStart()
    {
        GameManager.Instance.WaitEvolutioning(10f);
        Vector3 finalRotation = new Vector3(0, 180, 0);
        float duration = 10f;
        float initialSpeed = 10f;
        float speedIncrease = 400f;
        float elapsedTime = 0f;
        bool halfwayReached = false;
        while (elapsedTime < duration)
        {
            
            float currentSpeed = initialSpeed + (speedIncrease * (elapsedTime / duration));
            float rotationAngle = currentSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAngle, 0);
            elapsedTime += Time.deltaTime;

            if (!halfwayReached && elapsedTime >= duration / 2)
            {
                EvolutionEffect.SetActive(true);

                halfwayReached = true;

                transform.GetChild(_currentEvolutionNum).gameObject.SetActive(false);

                transform.GetChild(_evolutionNum).gameObject.SetActive(true);

                animator = transform.GetChild(_evolutionNum).GetComponentInChildren<Animator>();

                _currentEvolutionNum++;
                _evolutionNum++;
            }
            yield return null;
        }

        // 최종 회전 완료
        yield return transform.DORotate(finalRotation, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
        isEvolutioning = false;
        EvolutionEffect.SetActive(false);
        GameManager.Instance.OnEndEvolutioning();

        if (_upgradeState == UpgradeState.low)
        {
            _upgradeState = UpgradeState.middle;
        }
        else if (_upgradeState == UpgradeState.middle)
        {
            _upgradeState = UpgradeState.high;
        }
        ApplyUpgradeState();
    }

}

public class Skill
{
    public GameObject skillEffect; // 스킬 이펙트 프리팹

    public Skill(GameObject effect)
    {
        skillEffect = effect;
    }

    public void Execute(Vector3 position)
    {
        if (skillEffect != null)
        {
            skillEffect.gameObject.SetActive(true);
            
        }
    }

    public void OffSkillEffect()
    {
        if(skillEffect != null)
        {
            skillEffect.gameObject.SetActive(false);
        }
    }
}
