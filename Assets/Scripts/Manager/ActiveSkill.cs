using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public Digimon digimon;  // Digimon 객체를 참조
    public Button EvolutionButton;  // UI 버튼 참조
    private Action action;
    [SerializeField] GameObject vir2;

    private void Awake()
    {
        action = RegistEvent;
        //skillButton.onClick.AddListener(OnClick);  // 버튼 클릭 이벤트 연결
    }

    private void Update()
    {
        // EvolutionGauge가 100인지 확인하고 버튼 활성화/비활성화
        if (digimon.EvolutionGauge >= 100&& digimon._evolutionNum < 3)
        {
            EvolutionButton.interactable = true;
            //EventSystem.current.SetSelectedGameObject(skillButton.gameObject);
        }
        
        else
        {
            EvolutionButton.interactable = false;
        }
    }

    public void OnClick()
    {
        if (action != null)
        {
            action.Invoke();
            StartCoroutine(destroyvir());
        }
    }

    IEnumerator destroyvir()
    {
        vir2.SetActive(true);
        yield return new WaitForSeconds(8f);
        vir2.SetActive(false);
    }

    public void RegistEvent()
    {
        digimon.EvolutionGauge = 0;
        digimon.OnEvolution();
    }
}
