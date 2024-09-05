using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public Digimon digimon;  // Digimon ��ü�� ����
    public Button EvolutionButton;  // UI ��ư ����
    private Action action;
    [SerializeField] GameObject vir2;

    private void Awake()
    {
        action = RegistEvent;
        //skillButton.onClick.AddListener(OnClick);  // ��ư Ŭ�� �̺�Ʈ ����
    }

    private void Update()
    {
        // EvolutionGauge�� 100���� Ȯ���ϰ� ��ư Ȱ��ȭ/��Ȱ��ȭ
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
