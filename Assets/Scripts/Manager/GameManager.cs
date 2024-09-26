using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Digimon Player;
    [SerializeField] private List<Enemy> Enemies = new List<Enemy>();  // 모든 적을 관리할 리스트
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Gatcha gatcha; // Gatcha 스크립트 참조
    [SerializeField] private GameObject egg;

    private float _zoomInFOV = 30f;  // 줌인 시 FOV 값
    private float _zoomOutFOV = 90f;  // 줌아웃 시 FOV 값
    private float _zoomSpeed = 2f;    // 줌 속도

    private int _waveCount = 0; // 현재 웨이브 카운트
    private int _enemyKillCount = 0;  // 현재 웨이브에서 처치한 적의 수
    private int _eggCount = 0;
  
    public void StartWave()
    {
        Debug.Log($"웨이브 {_waveCount + 1} 시작");
        _waveCount++;
        UiManager.Instance.UpdateWaveUI(_waveCount);
        SpawnManager.Instance.SpawnEx(); 
    }

    public void InitPlayerState()
    {
        Digimon digimon = FindObjectOfType<Digimon>();
        Player = digimon;
        Player.InitPlayer();
    }

    public void WaveCount()
    {
        StartCoroutine(WaitNextStage()); 
    }

    private IEnumerator WaitNextStage()
    {
        SpawnManager.Instance.StopSpawn();  // 스폰 중지
        yield return new WaitForSeconds(5);  // 5초 대기
        StartWave();  // 다음 웨이브 시작
    }

    public void InitTarget(Enemy enemy)
    {
        enemy.target = Player.transform;  
        AddEnemyToList(enemy);
    }

    private void AddEnemyToList(Enemy enemy)
    {
        if (!Enemies.Contains(enemy))
        {
            Enemies.Add(enemy);  // 적을 리스트에 추가
        }
    }
    
    public void StartGatcha()
    {
        if (gatcha != null)
        {
            gatcha.PerformGatcha();  // Gatcha 실행
        }
    }

    public void WaitEvolutioning(float time)
    {
        if (Player.isEvolutioning)
        {
            StopAllEnemies();  // 진화 시 적 중지
            StartCoroutine(EvolutionCameraRoutine(time));
        }
    }

    public void WaitSpecialGacha()
    {
        StopAllEnemies();
    }

    public void OnEndEvolutioning()
    {
        ResumeAllEnemies();  // 진화 완료 시 적 재개
    }

    private void StopAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.StopWhenEvolution();  // 적 중지
        }
    }

    private void ResumeAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.ResumeEnemy();  // 적 재개
        }
    }

    public void AddEgg()
    {
        _eggCount++;
        Debug.Log($"알 개수: {_eggCount}");
        CheckEggCount();
    }

    public void UseEgg()
    {
        if (_eggCount > 0)
        {
            _eggCount--;
            Debug.Log($"알 개수: {_eggCount}");
            CheckEggCount();
        }
    }

    private void CheckEggCount()
    {
        if (_eggCount <= 0)
        {
            UiManager.Instance.ActiveGachaBtn(false);
        }
        else
        {
            UiManager.Instance.ActiveGachaBtn(true);
        }
    }

    public void CountEnemy()
    {
        _enemyKillCount++;
        Debug.Log($"처치된 적 수: {_enemyKillCount}");

        // 적이 5마리 죽으면 다음 웨이브로 넘어감
        if (_enemyKillCount >= 5)
        {
            _enemyKillCount = 0; // 킬 카운트 초기화
            WaveCount();
        }
    }

    public void RandomSpawnEgg(Transform spawnPoint)
    {
        //var rand = Random.Range(0, 10);
        //if (rand == 0)
        //{
            ObjectPoolManager.Instance.DequeueObject(egg, spawnPoint.position);
            Debug.Log("알이 스폰되었습니다.");
        //}
    }

    #region cameraZoom
    private IEnumerator ZoomIn()//진화 카메라 줌인
    {
        float startFOV = vcam.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (vcam.m_Lens.FieldOfView > _zoomInFOV)
        {
            elapsedTime += Time.deltaTime * _zoomSpeed;
            vcam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _zoomInFOV, elapsedTime);
            yield return null;
        }
    }

    private IEnumerator ZoomOut()//진화 카메라 줌 아웃
    {
        float startFOV = vcam.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (vcam.m_Lens.FieldOfView < _zoomOutFOV)
        {
            elapsedTime += Time.deltaTime * _zoomSpeed;
            vcam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _zoomOutFOV, elapsedTime);
            yield return null;
        }
    }

    public IEnumerator EvolutionCameraRoutine(float duration)
    {
        yield return StartCoroutine(ZoomIn());

        yield return new WaitForSeconds(duration / 2);

        yield return StartCoroutine(ZoomOut());
    }
    #endregion

}
