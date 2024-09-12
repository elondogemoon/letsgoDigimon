using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using System.IO;
public class UiManager : Singleton<UiManager>
{
    [SerializeField] private Image gachaImage; // UI �̹��� ������Ʈ
    [SerializeField] private TextMeshProUGUI text; // UI �ؽ�Ʈ ������Ʈ
    [SerializeField] private Button gachaBtn;
    [SerializeField] private TextMeshProUGUI waveText;

    public void UpdateUI(GachaItem item)
    {
        Sprite itemSprite = LoadSpriteFromPath(item.imagePath);
        gachaImage.sprite = itemSprite;
        gachaImage.enabled = true;
        text.enabled = true;
        text.text = $"{item.name} {item.rarity}����� ȹ���ߴ�!";
        StartCoroutine(DisableUI());
    }
    private Sprite LoadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path); // Resources �������� ��������Ʈ �ҷ�����
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
