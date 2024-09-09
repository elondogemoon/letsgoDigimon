using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaDigimon : Digimon
{
    
    protected override IEnumerator EvolutionStart()
    {
        isEvolutioning = true;

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
        GameManager.Instance.OnEndEvolutioning();

        if (_upgradeState == UpgradeState.low)
        {
            _upgradeState = UpgradeState.middle;
        }
        else if (_upgradeState == UpgradeState.middle)
        {
            _upgradeState = UpgradeState.high;
        }
    }
    private void Update()
    {
        if (EvolutionGauge >= 100)
        {
            StartCoroutine(EvolutionStart());
            EvolutionGauge = 0;
        }
    }
}
