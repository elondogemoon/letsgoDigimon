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
    public void LoadResult(Dictionary<string, GachaResult> dic)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // JSON 파일 읽기
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // 임시로 데이터 로드

            if (loadedData != null)
            {
                dic.Clear(); // 기존 데이터를 지우고
                foreach (var entry in loadedData)
                {
                    dic.Add(entry.Key, entry.Value); // 새 데이터를 dic에 추가
                }
                Debug.Log("기존 가챠 결과를 불러왔습니다.");
            }
        }
        else
        {
            Debug.Log("저장된 가챠 결과가 없습니다.");
        }
    }

}

