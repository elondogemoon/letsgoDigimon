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
            Debug.Log($"���õ� ������: {selectedItem.name}, ���: {selectedItem.rarity}");

            // �������� ���� (����: �������� �����ϴ� ���� �ʿ�)
            SpawnItem(selectedItem);

            // ��í ����� ����
            AddGachaResult(selectedItem);

            // UI ������Ʈ
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

    // JSON ���Ϸ� ����
    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented); // JSON���� ��ȯ
        File.WriteAllText(filePath, json); // ���Ϸ� ����
        Debug.Log("����� JSON���� ����Ǿ����ϴ�.");
    }

    private void LoadResult()
    {
        if (File.Exists(filePath)) 
        {
            string json = File.ReadAllText(filePath); // JSON ���� �б�
            gachaResult = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // ��ųʸ��� ��ȯ
            Debug.Log("���� ��í ����� �ҷ��Խ��ϴ�.");
        }
        else
        {
            Debug.Log("����� ��í ����� �����ϴ�.");
        }
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // ���� ��ġ�� ����
        return spawnedObject;
    }
}
