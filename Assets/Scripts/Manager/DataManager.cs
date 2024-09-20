using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;




public class DataManager : Singleton<DataManager> 
{
    private string filePath;

    public Dictionary<string, Digimon> LoadedDigimonList { get; private set; }

    public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();


    private void Awake()
    {
        filePath = Application.persistentDataPath + "/gachaResults.json";
        LoadDigimonData();
    }

    private void LoadDigimonData()
    {
        // Resources �������� XML ������ �ҷ���
        TextAsset xmlData = Resources.Load<TextAsset>("Resources/Digimon");
        if (xmlData == null)
        {
            Debug.LogError("Digimon data not found!");
            return;
        }

        // XML �Ľ�
        XDocument doc = XDocument.Parse(xmlData.text);
        if (doc == null)
        {
            Debug.LogError("Failed to parse Digimon data!");
            return;
        }

        LoadedDigimonList = new Dictionary<string, Digimon>();

        // XML�� data ������Ʈ�� ������
        var digimonData = doc.Descendants("data");
        foreach (var data in digimonData)
        {
            var digimon = new Digimon
            {
                //ClassName = data.Attribute("className").Value,
                //Name = data.Attribute("name").Value,
                //Description = data.Attribute("description").Value,
                //PrefabName = data.Attribute("prefabName").Value,
                //RequireRank = int.Parse(data.Attribute("requireRank").Value)
            };

            // �� Digimon �����͸� ��ųʸ��� �߰�
           // LoadedDigimonList.Add(digimon.ClassName, digimon);
        }

        Debug.Log("Digimon data loaded successfully!");
    }
    public void LoadResult(Dictionary<string, GachaResult> dic)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // JSON ���� �б�
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // �ӽ÷� ������ �ε�

            if (loadedData != null)
            {
                dic.Clear(); // ���� �����͸� �����
                foreach (var entry in loadedData)
                {
                    dic.Add(entry.Key, entry.Value); // �� �����͸� dic�� �߰�
                }
                Debug.Log("���� ��í ����� �ҷ��Խ��ϴ�.");
            }
        }
        else
        {
            Debug.Log("����� ��í ����� �����ϴ�.");
        }
    }

}

