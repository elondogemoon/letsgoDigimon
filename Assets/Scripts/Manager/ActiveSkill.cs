using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public Digimon digimon;  // Digimon 객체를 참조
    public Button skillButton;  // UI 버튼 참조
    private Action action;

    private void Awake()
    {
        action = RegistEvent;
        //skillButton.onClick.AddListener(OnClick);  // 버튼 클릭 이벤트 연결
    }

    private void Update()
    {
        // EvolutionGauge가 100인지 확인하고 버튼 활성화/비활성화
        if (digimon.EvolutionGauge >= 100)
        {
            skillButton.interactable = true;  // 버튼 활성화
        }
        else
        {
            skillButton.interactable = false;  // 버튼 비활성화
        }
    }

    public void OnClick()
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public void RegistEvent()
    {
        Debug.Log("반응");
    }
}
