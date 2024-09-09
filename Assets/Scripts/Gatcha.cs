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

    [SerializeField] Image gachaImage;
    [SerializeField] TextMeshProUGUI text;
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json"; // 파일 저장 경로 설정
        LoadResult(); 
    }

    public void PerformGatcha()
    {
        GachaItem selectedItem = GetRandomByWeight(gachaItems);

        if (selectedItem != null)
        {
            Debug.Log($"선택된 아이템: {selectedItem.name}, 등급: {selectedItem.rarity}");
            AddGachaResult(selectedItem);
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
        gachaResult[item.name] = new GachaResult { name = item.name, rarity = item.rarity };
        Debug.Log($"저장된 아이템: {item.name}, 등급: {item.rarity}");
        StoreResult(); // 결과 저장
    }

    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented); // JSON으로 변환
        File.WriteAllText(filePath, json); // 파일로 저장
        Debug.Log("결과가 JSON으로 저장되었습니다.");
    }

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
        text.text = $"{item.name} {item.rarity}등급을 획득했다 !";
    }
}