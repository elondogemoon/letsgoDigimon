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

    public void UpdateUI(GachaItem item)
    {
        Sprite itemSprite = LoadSpriteFromPath(item.imagePath);
        gachaImage.sprite = itemSprite;
        gachaImage.enabled = true;
        text.enabled = true;
        text.text = $"{item.name} {item.rarity}등급을 획득했다!";
        StartCoroutine(DisableUI());
    }
    private Sprite LoadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path); // Resources 폴더에서 스프라이트 불러오기
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
    public void ActiveGachaBtn()
    {
        gachaBtn.interactable = true;
    }

    public void DeAciveGachaBtn()
    {
        gachaBtn.interactable = false;
    }
}
