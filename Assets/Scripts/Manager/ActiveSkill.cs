using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public Digimon digimon;  // Digimon ��ü�� ����
    public Button skillButton;  // UI ��ư ����
    private Action action;

    private void Awake()
    {
        action = RegistEvent;
        //skillButton.onClick.AddListener(OnClick);  // ��ư Ŭ�� �̺�Ʈ ����
    }

    private void Update()
    {
        // EvolutionGauge�� 100���� Ȯ���ϰ� ��ư Ȱ��ȭ/��Ȱ��ȭ
        if (digimon.EvolutionGauge >= 100)
        {
            skillButton.interactable = true;  // ��ư Ȱ��ȭ
        }
        else
        {
            skillButton.interactable = false;  // ��ư ��Ȱ��ȭ
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
        Debug.Log("����");
    }
}
