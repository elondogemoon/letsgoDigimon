using UnityEngine;

[System.Serializable]
public class GachaItem
{
    public string name;           // 아이템 이름
    public string rarity;         // 아이템 등급
    public GameObject itemObject; // 아이템에 해당하는 게임 오브젝트
    public float weight;          // 가중치
}
