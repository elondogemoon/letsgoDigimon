using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public Digimon digimon;  // Digimon 객체를 참조
    public Button EvolutionButton;  // UI 버튼 참조
    private Action action;
    [SerializeField] GameObject vir2;
    [SerializeField] Button GatchaButton;
    [SerializeField] Button SkillButton;
    private void Awake()
    {
        action = RegistEvent;
        GatchaButton.interactable = false;
    }

    private void Update()
    {
        if (digimon.EvolutionGauge >= 100&& digimon._evolutionNum < 3)
        {
            EvolutionButton.interactable = true;
        }
        
        else 
        {
            EvolutionButton.interactable = false;
        }
        if (digimon.CurrentMP >= 100)
        {
            SkillButton.interactable = true;
        }
        else
        {
            SkillButton.interactable = false;
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

    public void OnGatcha()
    {
        UiManager.Instance.OnGachaBtnClick();
        GameManager.Instance.StartGatcha();
    }

    public void OnSkill()
    {
        digimon.ActiveSkill();
    }

}
