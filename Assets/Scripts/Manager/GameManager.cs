using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Digimon Player;
    [SerializeField] List<Enemy> Enemies = new List<Enemy>();  // 모든 적을 관리할 리스트
    [SerializeField] CinemachineVirtualCamera vcam;

    private float _zoomInFOV = 30f;  // 줌인 시 FOV 값
    private float _zoomOutFOV = 90f;  // 줌아웃 시 FOV 값
    private float _zoomSpeed = 2f;    // 줌 속도

    public void InitTarget(Enemy enemy)
    {
        enemy.target = Player.transform;
        AddEnemyToList(enemy);  // 적을 리스트에 추가
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
            StartCoroutine(EvolutionCameraRoutine(10f));  
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

    private IEnumerator ZoomIn()
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

    private IEnumerator ZoomOut()
    {
        float startFOV = vcam.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        // 서서히 줌아웃 (FOV가 _zoomOutFOV까지 서서히 증가)
        while (vcam.m_Lens.FieldOfView < _zoomOutFOV)
        {
            elapsedTime += Time.deltaTime * _zoomSpeed;
            vcam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _zoomOutFOV, elapsedTime);
            yield return null;
        }
    }

    // 진화 중 카메라가 줌인 및 줌아웃하는 코루틴
    public IEnumerator EvolutionCameraRoutine(float duration)
    {
        yield return StartCoroutine(ZoomIn());

        yield return new WaitForSeconds(duration / 2);

        yield return StartCoroutine(ZoomOut());
    }
}
