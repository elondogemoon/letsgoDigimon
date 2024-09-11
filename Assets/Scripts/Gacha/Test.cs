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
            string json = File.ReadAllText(filePath); // JSON ���� �б�
            var dic = JsonConvert.DeserializeObject<Dictionary<string, GachaResult>>(json); // ��ųʸ��� ��ȯ
            Debug.Log("�׽�Ʈ�� json �޾ƿ��� ����.");
        }
        else
        {
            Debug.Log("�� ����");
        }
    }


}
