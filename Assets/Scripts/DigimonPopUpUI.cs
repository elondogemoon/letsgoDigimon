using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigimonPopUpUI : MonoBehaviour
{
    public List<Image> DigimonImg = new List<Image>();
    private Dictionary<string, GachaResult> digimonData = new Dictionary<string, GachaResult>();

    private void OnEnable()
    {

    }

    private void Start()
    {
        DataManager.Instance.LoadResult(digimonData);
        SetDigimonUi();

    }

    private void SetDigimonUi()
    {
        int index = 0;

        // 딕셔너리에서 데이터를 순회
        foreach (var data in digimonData.Values)
        {
            if (index >= DigimonImg.Count)
            {
                break; // 슬롯보다 더 많은 데이터를 받으면 루프 중지
            }

            // Digimon 이미지를 설정 (가정: GachaResult에 이미지 데이터가 있다고 가정)
            Sprite digimonSprite = Resources.Load<Sprite>($"Images/{data.name}"); // 경로는 필요에 따라 변경
            if (digimonSprite != null)
            {
                DigimonImg[index].sprite = digimonSprite; // 슬롯에 이미지 설정
                DigimonImg[index].gameObject.SetActive(true); // 슬롯 활성화
            }
            else
            {
                DigimonImg[index].gameObject.SetActive(false); // 이미지가 없으면 비활성화
            }

            index++;
        }

        // 남은 슬롯 비활성화
        for (int i = index; i < DigimonImg.Count; i++)
        {
            DigimonImg[i].gameObject.SetActive(false);
        }
    }

}
