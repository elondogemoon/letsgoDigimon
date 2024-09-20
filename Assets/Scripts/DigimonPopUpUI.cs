using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigimonPopUpUI : MonoBehaviour
{
    public List<Button> changeButtons;  
    public List<Image> DigimonImg = new List<Image>();
    private Dictionary<string, GachaResult> digimonData = new Dictionary<string, GachaResult>();
    public GameObject player;
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

    private void SetDigimonUi(Dictionary<string, GachaResult> dataDic)
    {
        int index = 0;

        // 딕셔너리에서 데이터를 순회
        foreach (var data in dataDic.Values)
        {
            if (index >= DigimonImg.Count || index >= changeButtons.Count)
            {
                break;
            }

            Sprite digimonSprite = Resources.Load<Sprite>($"{data.imagepath}");
            Debug.Log($"Loaded sprite for {data.imagepath}: {(digimonSprite != null ? "Success" : "Failed")}");

            if (digimonSprite != null)
            {
                DigimonImg[index].sprite = digimonSprite;
                DigimonImg[index].gameObject.SetActive(true);

           
                int buttonIndex = index;  
                changeButtons[buttonIndex].onClick.AddListener(() => ChangeGachaedDigimon(data.modelPath));
                changeButtons[buttonIndex].gameObject.SetActive(true);
            }
            else
            {
                DigimonImg[index].gameObject.SetActive(false);
                changeButtons[index].gameObject.SetActive(false);
            }

            index++;
        }

        // 남은 슬롯 비활성화
        for (int i = index; i < DigimonImg.Count; i++)
        {
            DigimonImg[i].gameObject.SetActive(false);
            changeButtons[i].gameObject.SetActive(false);
        }
    }

    private void ChangeGachaedDigimon(string modelPath)
    {
        GameObject digimonPrefab = Resources.Load<GameObject>(modelPath);

        if (digimonPrefab != null)
        {
            Instantiate(digimonPrefab, player.transform);
            
            Debug.Log($"Changing model to: {modelPath}");
        }
    }
}
