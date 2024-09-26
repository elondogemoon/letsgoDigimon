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
    public Image fadeImage;  // 페이드 효과를 줄 이미지
    private float _moveDuration = 5f;
    private float _fadeDuration = 3f;  // 페이드아웃 지속 시간

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
        Vector3 endPosition = startPosition + new Vector3(-10, 0, 0);  // X축 마이너스 방향으로 이동

        float elapsedTime = 0f;

        while (elapsedTime < _moveDuration)
        {
            AdventureCam.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // 한 프레임 대기
        }
        AdventureCam.SetActive(false);
    }

    private IEnumerator FadeOutUI()
    {
        fadeImage.enabled = true;  // 페이드 이미지를 활성화
        Color fadeColor = fadeImage.color;  // 현재 색상 가져오기
        float elapsedTime = 0f;
        
        // 알파 값을 0에서 1로 서서히 변경
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / _fadeDuration);  // 알파 값 변경
            fadeImage.color = fadeColor;  // 변경된 알파 값 적용
            yield return null;
        }
        fadeColor.a = 1f;  
        fadeImage.color = fadeColor;
        PlayerCam.SetActive(true);
    }
}
