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

    GameObject temporalDigimonObj;

    public GameObject player;
    private void OnEnable()
    {
        Gatcha.Instance.gachaResultUpdated += SetDigimonUi;
    }

    private void OnDisable()
    {
        Gatcha.Instance.gachaResultUpdated -= SetDigimonUi;
    }


    //private void ClearDigimonObject()
    //{
    //    _digimonObjList.ForEach(e => { DestroyImmediate(e); });
    //    _digimonObjList.Clear();
    //}

    private void Start()
    {
        DataManager.Instance.LoadResult(digimonData);
        SetDigimonUi(digimonData);
    }

    private void CheckSelectFirstDigimon(int index, string modelPath)
    {
        //ù��° ���� �ڵ� ����
        if (index == 0)
        {
            ChangeGachaedDigimon(modelPath);
        }
    }

    private void SetDigimonUi(Dictionary<string, GachaResult> dataDic)
    {
        int idx = 0;

        // ��ųʸ����� �����͸� ��ȸ
        foreach (var data in dataDic.Values)
        {
            if (idx >= DigimonImg.Count || idx >= changeButtons.Count)
            {
                break;
            }

            Sprite digimonSprite = Resources.Load<Sprite>($"{data.imagepath}");
            Debug.Log($"Loaded sprite for {data.imagepath}: {(digimonSprite != null ? "Success" : "Failed")}");

            if (digimonSprite != null)
            {
                DigimonImg[idx].sprite = digimonSprite;
                DigimonImg[idx].gameObject.SetActive(true);

           
                int buttonIndex = idx;  
                changeButtons[buttonIndex].onClick.AddListener(() => ChangeGachaedDigimon(data.modelPath));
                changeButtons[buttonIndex].gameObject.SetActive(true);
            }
            else
            {
                DigimonImg[idx].gameObject.SetActive(false);
                changeButtons[idx].gameObject.SetActive(false);
            }

            CheckSelectFirstDigimon(idx, data.modelPath);

            idx++;
        }

        // ���� ���� ��Ȱ��ȭ
        for (int i = idx; i < DigimonImg.Count; i++)
        {
            DigimonImg[i].gameObject.SetActive(false);
            changeButtons[i].gameObject.SetActive(false);
        }
    }

    private void ChangeGachaedDigimon(string modelPath)
    {
        if(temporalDigimonObj != null)
        {
            DestroyImmediate(temporalDigimonObj);
        }

        GameObject digimonPrefab = Resources.Load<GameObject>(modelPath);

        if (digimonPrefab != null)
        {
            var gObj = Instantiate(digimonPrefab, player.transform);
            temporalDigimonObj = gObj;

            Debug.Log($"Changing model to: {modelPath}");
        }
    }
}
