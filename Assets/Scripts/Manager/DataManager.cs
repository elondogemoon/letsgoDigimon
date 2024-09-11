using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
public class GachaedDigimonData
{
    public float id;
    public float PosX;
    public float PosZ;
    public int rank;
    public string className;

    public GachaedDigimonData(float id, float posX, float posZ)
    {
        this.id = id;
        PosX = posX;
        PosZ = posZ;
    }
}

public class GameData
{
    public string id;
    public string name;
    public float hp;
    public float evolutionGauge;
    public float skillDamage;
    public List<GachaedDigimonData> Digimondata;

    public GameData(string id, string name, float hp, float evolutionGauge, float skillDamage, List<GachaedDigimonData> digimondata)
    {
        this.id = id;
        this.name = name;
        this.hp = hp;
        this.evolutionGauge = evolutionGauge;
        this.skillDamage = skillDamage;
        Digimondata = digimondata;
    }
}

public class DataManager : Singleton<DataManager> 
{

    public Dictionary<string, Digimon> LoadedDigimonList { get; private set; }

    private void Awake()
    {
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
}

