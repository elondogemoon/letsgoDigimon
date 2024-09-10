using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
public class UiManager : Singleton<UiManager>
{
    [SerializeField] private Image gachaImage; // UI �̹��� ������Ʈ
    [SerializeField] private TextMeshProUGUI text; // UI �ؽ�Ʈ ������Ʈ
    [SerializeField] private Button gachaBtn;

    public void UpdateUI(GachaItem item)
    {
        gachaImage.sprite = item.Image;
        gachaImage.enabled = true;
        text.enabled = true;
        text.text = $"{item.name} {item.rarity}����� ȹ���ߴ�!";
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
