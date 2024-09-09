using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Gatcha : MonoBehaviour
{
    [SerializeField] public List<GachaItem> gachaItems = new List<GachaItem>(); // 가차 아이템 리스트
    [SerializeField] public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();

    [SerializeField] private Image gachaImage; // UI 이미지 컴포넌트
    [SerializeField] private TextMeshProUGUI text; // UI 텍스트 컴포넌트

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json"; // 파일 저장 경로 설정
        LoadResult();
    }

    public void PerformGatcha()
    {
        GachaItem selectedItem = GetRandomByWeight(gachaItems); // 가중치 기반으로 랜덤 아이템 선택

        if (selectedItem != null)
        {
            Debug.Log($"선택된 아이템: {selectedItem.name}, 등급: {selectedItem.rarity}");

            // 아이템을 스폰 (가정: 아이템을 스폰하는 로직 필요)
            GameObject spawnedObject = SpawnItem(selectedItem);

            // 가챠 결과를 저장
            AddGachaResult(selectedItem);

            // UI 업데이트
            UpdateUI(selectedItem);
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

    // 기존 JSON 파일에서 결과를 불러옴
    private void LoadResult()
    {
        if (File.Exists(filePath)) // 파일이 존재하면
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

    public void UpdateUI(GachaItem item)
    {
        gachaImage.sprite = item.Image; 
        gachaImage.enabled = true;
        text.text = $"{item.name} {item.rarity}등급을 획득했다!"; 
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // 랜덤 위치에 생성
        return spawnedObject;
    }
}
