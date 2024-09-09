using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Gatcha : MonoBehaviour
{
    [SerializeField] public List<GachaItem> gachaItems = new List<GachaItem>(); // ���� ������ ����Ʈ
    [SerializeField] public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();

    [SerializeField] private Image gachaImage; // UI �̹��� ������Ʈ
    [SerializeField] private TextMeshProUGUI text; // UI �ؽ�Ʈ ������Ʈ

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json"; // ���� ���� ��� ����
        LoadResult();
    }

    public void PerformGatcha()
    {
        GachaItem selectedItem = GetRandomByWeight(gachaItems); // ����ġ ������� ���� ������ ����

        if (selectedItem != null)
        {
            Debug.Log($"���õ� ������: {selectedItem.name}, ���: {selectedItem.rarity}");

            // �������� ���� (����: �������� �����ϴ� ���� �ʿ�)
            GameObject spawnedObject = SpawnItem(selectedItem);

            // ��í ����� ����
            AddGachaResult(selectedItem);

            // UI ������Ʈ
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

    // JSON ���Ϸ� ����
    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented); // JSON���� ��ȯ
        File.WriteAllText(filePath, json); // ���Ϸ� ����
        Debug.Log("����� JSON���� ����Ǿ����ϴ�.");
    }

    // ���� JSON ���Ͽ��� ����� �ҷ���
    private void LoadResult()
    {
        if (File.Exists(filePath)) // ������ �����ϸ�
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

    public void UpdateUI(GachaItem item)
    {
        gachaImage.sprite = item.Image; 
        gachaImage.enabled = true;
        text.text = $"{item.name} {item.rarity}����� ȹ���ߴ�!"; 
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // ���� ��ġ�� ����
        return spawnedObject;
    }
}
