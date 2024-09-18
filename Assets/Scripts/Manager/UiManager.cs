using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using System.IO;
public class UiManager : Singleton<UiManager>
{
    [SerializeField] private Image gachaImage; // UI 이미지 컴포넌트
    [SerializeField] private TextMeshProUGUI text; // UI 텍스트 컴포넌트
    [SerializeField] private Button gachaBtn;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject specialGacha;
    [SerializeField] Item item;
    private Coroutine disableUICoroutine;
    public void UpdateUI(GachaItem item)
    {
        Sprite itemSprite = LoadSpriteFromPath(item.imagePath);
        gachaImage.sprite = itemSprite;
        gachaImage.enabled = true;
        text.enabled = true;
        text.text = $"{item.name} {item.rarity}등급을 획득했다!";
        if (disableUICoroutine != null)
        {
            StopCoroutine(DisableUI());
        }
        disableUICoroutine = StartCoroutine(DisableUI());
    }

    private Sprite LoadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path); 
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(1);
        gachaImage.enabled = false;
        text.enabled = false;
    }

    public void UpdateWaveUI(int count)
    {
        waveText.text = $"Wave : {count} ";
    }
    public void ActiveGachaBtn(bool isCanGacha)
    {
        if (isCanGacha)
        {
            gachaBtn.interactable = true;
        }
        else
        {
            gachaBtn.interactable = false;
        }
    }

    public void OnGachaBtnClick()
    {
        item.OnGachaButtonClick();
    }

    public void GachaEvent(bool isSpecialGacha)
    {
        specialGacha.SetActive(isSpecialGacha);
    }

}
