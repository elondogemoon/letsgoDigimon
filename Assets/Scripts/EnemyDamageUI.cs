using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDamageUI : MonoBehaviour
{
    
    [SerializeField] GameObject Canvas;
    [SerializeField] TextMeshProUGUI text;
    Transform _cameraTrans;
    private void Awake()
    {
        _cameraTrans = Camera.main.transform;
    }
    public void DamageUI(float damage)
    {
        Canvas.SetActive(true);

        StartCoroutine(OnDamageTxt(damage));
    }

    public IEnumerator OnDamageTxt(float damage)
    {
        text.text = damage.ToString();
        float startFontSize = 1f;
        float endFontSize = 0.2f;

        for (float i = startFontSize; i > endFontSize; i -= 0.01f) // float로 루프를 돌림
        {
            text.fontSize = i;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.7f);
        Canvas.SetActive(false);
    }

    private void Update()
    {
        transform.LookAt(_cameraTrans);
        transform.Rotate(0, 180, 0);
    }
}
