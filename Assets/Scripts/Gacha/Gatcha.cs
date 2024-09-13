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
            Debug.Log($"���õ� ������: {selectedItem.name}, ���: {selectedItem.rarity}");

            UiManager.Instance.UpdateUI(selectedItem);
            // �������� ����
            SpawnItem(selectedItem);

            // ��í ����� ����
            AddGachaResult(selectedItem);

            // rare�� unique �������� ��� ���ԵǾ� �ִ��� Ȯ��
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
            Debug.Log("rare�� unique �������� ��� ���ԵǾ� �ֽ��ϴ�. Ư�� �۾��� �����մϴ�.");
            ExecuteSpecialTask();
        }
    }

    private void ExecuteSpecialTask()
    {
        StartCoroutine(PlaySpecialEvolution());
        Debug.Log("Ư�� �۾��� ����Ǿ����ϴ�.");
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
        // ����
        StoreResult();
    }

    private void StoreResult()
    {
        string json = JsonConvert.SerializeObject(gachaResult, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log("����� JSON���� ����Ǿ����ϴ�.");
    }

    private void LoadResult()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gachaResult = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json);
            Debug.Log("���� ��í ����� �ҷ��Խ��ϴ�.");

            // �ε�� ����� UI ������Ʈ �̺�Ʈ ȣ��
            gachaResultUpdated?.Invoke(gachaResult);
        }
        else
        {
            Debug.Log("����� ��í ����� �����ϴ�.");
        }
    }

    private GameObject SpawnItem(GachaItem item)
    {
        GameObject spawnedObject = Instantiate(item.itemObject);
        spawnedObject.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
        return spawnedObject;
    }
}
