using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;
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
    private string filePath;

    public Dictionary<string, Digimon> LoadedDigimonList { get; private set; }

    public Dictionary<string, GachaResult> gachaResult = new Dictionary<string, GachaResult>();


    private void Awake()
    {
        LoadDigimonData();
        filePath = Application.persistentDataPath + "/gachaResults.json";

    }

    private void LoadDigimonData()
    {
        // Resources 폴더에서 XML 파일을 불러옴
        TextAsset xmlData = Resources.Load<TextAsset>("Resources/Digimon");
        if (xmlData == null)
        {
            Debug.LogError("Digimon data not found!");
            return;
        }

        // XML 파싱
        XDocument doc = XDocument.Parse(xmlData.text);
        if (doc == null)
        {
            Debug.LogError("Failed to parse Digimon data!");
            return;
        }

        LoadedDigimonList = new Dictionary<string, Digimon>();

        // XML의 data 엘리먼트를 가져옴
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

            // 각 Digimon 데이터를 딕셔너리에 추가
           // LoadedDigimonList.Add(digimon.ClassName, digimon);
        }

        Debug.Log("Digimon data loaded successfully!");
    }
    public void LoadResult(Dictionary<string,GachaResult> dic)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // JSON 파일 읽기
            dic = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // 딕셔너리로 변환
            Debug.Log("기존 가챠 결과를.");
        }
        else
        {
            Debug.Log("저장된 가챠 결과가 없습니다.");
        }
    }
}

