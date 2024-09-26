using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartGame : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] GameObject AdventureCam;
    [SerializeField] GameObject PlayerCam;
    [SerializeField] GameObject IngameUI;
    public GameObject gobj;
    public Image fadeImage;  // ���̵� ȿ���� �� �̹���
    private float _moveDuration = 5f;
    private float _fadeDuration = 3f;  // ���̵�ƿ� ���� �ð�

    public void Play()
    {
        StartCoroutine(Cam());
        AdventureCam.SetActive(true);
        StartCoroutine(MoveAdventureCam());
        GameManager.Instance.InitPlayerState();
    }

    private IEnumerator Cam()
    {
        StartCoroutine(FadeOutUI());
        
        yield return new WaitForSeconds(5);
        gobj.SetActive(false);
        IngameUI.SetActive(true);
        GameManager.Instance.StartWave();
    }

    private IEnumerator MoveAdventureCam()
    {
        Vector3 startPosition = AdventureCam.transform.position;
        Vector3 endPosition = startPosition + new Vector3(-10, 0, 0);  // X�� ���̳ʽ� �������� �̵�

        float elapsedTime = 0f;

        while (elapsedTime < _moveDuration)
        {
            AdventureCam.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // �� ������ ���
        }
        AdventureCam.SetActive(false);
    }

    private IEnumerator FadeOutUI()
    {
        fadeImage.enabled = true;  // ���̵� �̹����� Ȱ��ȭ
        Color fadeColor = fadeImage.color;  // ���� ���� ��������
        float elapsedTime = 0f;
        
        // ���� ���� 0���� 1�� ������ ����
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / _fadeDuration);  // ���� �� ����
            fadeImage.color = fadeColor;  // ����� ���� �� ����
            yield return null;
        }
        fadeColor.a = 1f;  
        fadeImage.color = fadeColor;
        PlayerCam.SetActive(true);
    }
}
