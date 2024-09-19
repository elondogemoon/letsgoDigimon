using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class Gatcha : Singleton<Gatcha>
{
    [SerializeField] public List<GachaItem> gachaItems = new List<GachaItem>();
    [SerializeField] public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();
    public event Action<Dictionary<string, GachaResult>> gachaResultUpdated;
    public GameObject vcam;
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트를 참조할 변수
    public GameObject SpecialGacha;
    private string filePath;
    private bool specialTaskExecuted = false;

    public Image fadeImage;
    private void OnEnable()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json";
        LoadResult();
    }

    private void Start()
    {
        // VideoPlayer가 끝났을 때 이벤트 등록
        if (videoPlayer != null)
        {
            
            videoPlayer.loopPointReached += OnVideoEnd; // 비디오가 끝났을 때 호출될 메서드 등록
        }
    }

    public void PerformGatcha()
    {
        GachaItem selectedItem = GetRandomByWeight(gachaItems);

        if (selectedItem != null)
        {
            Debug.Log($"선택된 아이템: {selectedItem.name}, 등급: {selectedItem.rarity}");

            UiManager.Instance.UpdateUI(selectedItem);
            //SpawnItem(selectedItem);
            AddGachaResult(selectedItem);
            CheckForSpecialRarities();
        }
    }

    private void CheckForSpecialRarities()
    {
        if (specialTaskExecuted)
        {
            Debug.Log("특별 작업이 이미 실행되었습니다.");
            return;
        }

        bool hasRare = false;
        bool hasUnique = false;

        foreach (var result in gachaResult.Values)
        {
            if (result.rarity == "Rare")
                hasRare = true;
            if (result.rarity == "Unique")
                hasUnique = true;
        }

        if (hasRare && hasUnique)
        {
            ExecuteSpecialTask();
            GameManager.Instance.WaitEvolutioning(20f);
        }
    }

    private void ExecuteSpecialTask()
    {
        GachaItem lastItem = gachaItems[gachaItems.Count - 1];
        AddGachaResult(lastItem);

        FadeOutUI(() =>
        {
            UiManager.Instance.GachaEvent(true);
            vcam.SetActive(true);

            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
            specialTaskExecuted = true;
            Debug.Log("특별 작업이 실행되었습니다.");
        });
    }

    private void FadeOutUI(Action onComplete)
    {
        fadeImage.enabled = true;
        fadeImage.color = new Color(0, 0, 0, 0); 
        fadeImage.DOFade(1f, 2f).OnComplete(() =>
        {
            fadeImage.enabled = false;
            onComplete?.Invoke(); 
        });
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SpecialGacha.SetActive(true);
        UiManager.Instance.GachaEvent(false);
        StartCoroutine(AfterGachaEvent());
    }

    private IEnumerator AfterGachaEvent()
    {
        yield return new WaitForSeconds(4);
        fadeImage.enabled = true;
        vcam.SetActive(false);
        StartCoroutine(EndEvent());

    }
    private IEnumerator EndEvent()
    {
        yield return new WaitForSeconds(3);
        fadeImage.enabled = false;
    }

    private GachaItem GetRandomByWeight(List<GachaItem> items)
    {
        float totalWeight = 0;
        foreach (var item in items)
        {
            totalWeight += item.weight;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float accumulatedWeight = 0;

        foreach (var item in items)
        {
            accumulatedWeight += item.weight;
            if (randomValue < accumulatedWeight)
            {
                return item;
            }
        }
        return null;
    }

    private void AddGachaResult(GachaItem item)
    {
        gachaResult[item.name] = new GachaResult
        {
            name = item.name,
            rarity = item.rarity,
            path = item.imagePath
        };

        gachaResultUpdated?.Invoke(gachaResult);
        StoreResult();
    }

    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log("결과가 JSON으로 저장되었습니다.");
    }

    private void LoadResult()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gachaResult = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json);
            Debug.Log("기존 가챠 결과를 불러왔습니다.");
            gachaResultUpdated?.Invoke(gachaResult);
        }
        else
        {
            Debug.Log("저장된 가챠 결과가 없습니다.");
        }
    }

    //private GameObject SpawnItem(GachaItem item)
    //{
    //    GameObject spawnedObject = Instantiate(item.itemObject);
    //    spawnedObject.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
    //    return spawnedObject;
    //}
}
