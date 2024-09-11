using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // UI에서 아이템이 배치될 패널
    [SerializeField] private GameObject inventorySlotPrefab; // 슬롯 프리팹
    [SerializeField] private List<Sprite> gachaItemImages = new List<Sprite>(); // 뽑힌 가챠 아이템들의 이미지 목록

    // 가챠를 통해 아이템을 획득할 때 호출되는 메서드
    public void AddItemToInventory(GachaItem gachaItem)
    {
        if (gachaItem.Image != null)
        {
            gachaItemImages.Add(gachaItem.Image); // 아이템 이미지 추가
            UpdateInventoryUI();
        }
    }

    // UI 업데이트 (새로운 아이템이 추가되면 인벤토리에 반영)
    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // 기존 UI 슬롯 삭제
        }

        foreach (var itemImage in gachaItemImages)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel); // 슬롯 생성
            slot.GetComponent<Image>().sprite = itemImage; // 슬롯에 이미지 설정
        }
    }
}
