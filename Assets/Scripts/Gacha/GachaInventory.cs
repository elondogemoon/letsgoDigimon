using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GachaInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // UI에서 아이템이 배치될 패널
    [SerializeField] private GameObject inventorySlotPrefab; // 슬롯 프리팹
    [SerializeField] private List<Sprite> gachaItemImages = new List<Sprite>(); // 뽑힌 가챠 아이템들의 이미지 목록

    // 가챠 아이템을 인벤토리에 추가할 때
    public void AddItemToInventory(GachaItem gachaItem)
    {
        Sprite itemSprite = LoadSpriteFromPath(gachaItem.imagePath); // 경로를 통해 스프라이트 불러오기
        if (itemSprite != null)
        {
            gachaItemImages.Add(itemSprite); // 이미지 리스트에 추가
            UpdateInventoryUI(); // UI 업데이트
        }
    }

    private void OnEnable()
    {
        UpdateInventoryUI(); // UI 업데이트
    }

    // 스프라이트 경로로부터 스프라이트를 로드하는 함수
    private Sprite LoadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path); // Resources 폴더에서 스프라이트 불러오기
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
