using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    private readonly string filePath = Path.Combine(Application.persistentDataPath, "gachaResults.json");

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // JSON 파일 읽기
            var dic = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // 딕셔너리로 변환
            Debug.Log("테스트로 json 받아오기 성공.");
        }
        else
        {
            Debug.Log("하 실패");
        }
    }


}
