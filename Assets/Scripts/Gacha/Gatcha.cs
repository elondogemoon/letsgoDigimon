using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;

public class Gatcha : Singleton<Gatcha>
{
    [SerializeField] public List<GachaItem> gachaItems = new List<GachaItem>();
    [SerializeField] public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();
    public event Action<Dictionary<string, GachaResult>> gachaResultUpdated;

    private string filePath;

    private void OnEnable()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json";
        LoadResult();
    }

    public void PerformGatcha()
    {
        GachaItem selectedItem = GetRandomByWeight(gachaItems);

        if (selectedItem != null)
        {
            Debug.Log($"선택된 아이템: {selectedItem.name}, 등급: {selectedItem.rarity}");

            UiManager.Instance.UpdateUI(selectedItem);
            // 아이템을 스폰
            SpawnItem(selectedItem);

            // 가챠 결과를 저장
            AddGachaResult(selectedItem);

            // rare와 unique 아이템이 모두 포함되어 있는지 확인
            CheckForSpecialRarities();
        }
    }

    private void CheckForSpecialRarities()
    {
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
            Debug.Log("rare와 unique 아이템이 모두 포함되어 있습니다. 특별 작업을 실행합니다.");
            ExecuteSpecialTask();
        }
    }

    private void ExecuteSpecialTask()
    {
        StartCoroutine(PlaySpecialEvolution());
        Debug.Log("특별 작업이 실행되었습니다.");
    }

    private IEnumerator PlaySpecialEvolution()
    {

        yield return new WaitForSeconds(5);
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
        // 저장
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

            // 로드된 결과로 UI 업데이트 이벤트 호출
            gachaResultUpdated?.Invoke(gachaResult);
        }
        else
        {
            Debug.Log("저장된 가챠 결과가 없습니다.");
        }
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
        return spawnedObject;
    }
}
