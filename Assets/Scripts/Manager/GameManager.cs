using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Digimon Player;
    [SerializeField] private List<Enemy> Enemies = new List<Enemy>();  // ��� ���� ������ ����Ʈ
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Gatcha gatcha; // Gatcha ��ũ��Ʈ ����
    [SerializeField] private GameObject egg;

    private float _zoomInFOV = 30f;  // ���� �� FOV ��
    private float _zoomOutFOV = 90f;  // �ܾƿ� �� FOV ��
    private float _zoomSpeed = 2f;    // �� �ӵ�

    private int _waveCount = 0; // ���� ���̺� ī��Ʈ
    private int _enemyKillCount = 0;  // ���� ���̺꿡�� óġ�� ���� ��
    private int _eggCount = 0;
  
    public void StartWave()
    {
        Debug.Log($"���̺� {_waveCount + 1} ����");
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
        SpawnManager.Instance.StopSpawn();  // ���� ����
        yield return new WaitForSeconds(5);  // 5�� ���
        StartWave();  // ���� ���̺� ����
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
            Enemies.Add(enemy);  // ���� ����Ʈ�� �߰�
        }
    }
    
    public void StartGatcha()
    {
        if (gatcha != null)
        {
            gatcha.PerformGatcha();  // Gatcha ����
        }
    }

    public void WaitEvolutioning(float time)
    {
        if (Player.isEvolutioning)
        {
            StopAllEnemies();  // ��ȭ �� �� ����
            StartCoroutine(EvolutionCameraRoutine(time));
        }
    }

    public void WaitSpecialGacha()
    {
        StopAllEnemies();
    }

    public void OnEndEvolutioning()
    {
        ResumeAllEnemies();  // ��ȭ �Ϸ� �� �� �簳
    }

    private void StopAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.StopWhenEvolution();  // �� ����
        }
    }

    private void ResumeAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.ResumeEnemy();  // �� �簳
        }
    }

    public void AddEgg()
    {
        _eggCount++;
        Debug.Log($"�� ����: {_eggCount}");
        CheckEggCount();
    }

    public void UseEgg()
    {
        if (_eggCount > 0)
        {
            _eggCount--;
            Debug.Log($"�� ����: {_eggCount}");
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
        Debug.Log($"óġ�� �� ��: {_enemyKillCount}");

        // ���� 5���� ������ ���� ���̺�� �Ѿ
        if (_enemyKillCount >= 5)
        {
            _enemyKillCount = 0; // ų ī��Ʈ �ʱ�ȭ
            WaveCount();
        }
    }

    public void RandomSpawnEgg(Transform spawnPoint)
    {
        //var rand = Random.Range(0, 10);
        //if (rand == 0)
        //{
            ObjectPoolManager.Instance.DequeueObject(egg, spawnPoint.position);
            Debug.Log("���� �����Ǿ����ϴ�.");
        //}
    }

    #region cameraZoom
    private IEnumerator ZoomIn()//��ȭ ī�޶� ����
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

    private IEnumerator ZoomOut()//��ȭ ī�޶� �� �ƿ�
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
