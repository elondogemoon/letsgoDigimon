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
        Gatcha.Instance.gachaResultUpdated += SetDigimonUi;
    }

    private void OnDisable()
    {
        Gatcha.Instance.gachaResultUpdated -= SetDigimonUi;
    }

    private void Start()
    {
        DataManager.Instance.LoadResult(digimonData);
        SetDigimonUi(digimonData);
    }

    private void SetDigimonUi(Dictionary<string,GachaResult> dataDic)
    {
        int index = 0;

        // 딕셔너리에서 데이터를 순회
        foreach (var data in dataDic.Values)
        {
            if (index >= DigimonImg.Count)
            {
                break; 
            }

            Sprite digimonSprite = Resources.Load<Sprite>($"{data.path}");
            Debug.Log($"Loaded sprite for {data.path}: {(digimonSprite != null ? "Success" : "Failed")}");

            if (digimonSprite != null)
            {
                DigimonImg[index].sprite = digimonSprite; 
                DigimonImg[index].gameObject.SetActive(true); 
            }
            else
            {
                DigimonImg[index].gameObject.SetActive(false); 
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
