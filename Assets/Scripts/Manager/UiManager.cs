using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
public class UiManager : Singleton<UiManager>
{
    [SerializeField] private Image gachaImage; // UI 이미지 컴포넌트
    [SerializeField] private TextMeshProUGUI text; // UI 텍스트 컴포넌트
    [SerializeField] private Button gachaBtn;

    public void UpdateUI(GachaItem item)
    {
        gachaImage.sprite = item.Image;
        gachaImage.enabled = true;
        text.enabled = true;
        text.text = $"{item.name} {item.rarity}등급을 획득했다!";
        StartCoroutine(DisableUI());
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(1);
        gachaImage.enabled = false;
        text.enabled = false;
    }

    public void ActiveGachaBtn()
    {
        gachaBtn.interactable = true;
    }
}
