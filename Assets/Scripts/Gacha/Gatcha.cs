using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Gatcha : MonoBehaviour
{
    [SerializeField] public List<GachaItem> gachaItems = new List<GachaItem>(); 
    [SerializeField] public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();

    private string _count;
    private string filePath;

    private void Start()
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

            // 아이템을 스폰 (가정: 아이템을 스폰하는 로직 필요)
            SpawnItem(selectedItem);

            // 가챠 결과를 저장
            AddGachaResult(selectedItem);

            // UI 업데이트
            UiManager.Instance.UpdateUI(selectedItem);
        }
    }

    private GachaItem GetRandomByWeight(List<GachaItem> items)
    {
        float totalWeight = 0;
        foreach (var item in items)
        {
            totalWeight += item.weight;
        }

        float randomValue = Random.Range(0, totalWeight);
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
        };
        StoreResult(); 
    }

    // JSON 파일로 저장
    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented); // JSON으로 변환
        File.WriteAllText(filePath, json); // 파일로 저장
        Debug.Log("결과가 JSON으로 저장되었습니다.");
    }

    private void LoadResult()
    {
        if (File.Exists(filePath)) 
        {
            string json = File.ReadAllText(filePath); // JSON 파일 읽기
            gachaResult = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // 딕셔너리로 변환
            Debug.Log("기존 가챠 결과를 불러왔습니다.");
        }
        else
        {
            Debug.Log("저장된 가챠 결과가 없습니다.");
        }
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // 랜덤 위치에 생성
        return spawnedObject;
    }
}
