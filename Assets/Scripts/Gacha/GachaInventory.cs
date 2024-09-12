using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GachaInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // UI���� �������� ��ġ�� �г�
    [SerializeField] private GameObject inventorySlotPrefab; // ���� ������
    [SerializeField] private List<Sprite> gachaItemImages = new List<Sprite>(); // ���� ��í �����۵��� �̹��� ���

    // ��í �������� �κ��丮�� �߰��� ��
    public void AddItemToInventory(GachaItem gachaItem)
    {
        Sprite itemSprite = LoadSpriteFromPath(gachaItem.imagePath); // ��θ� ���� ��������Ʈ �ҷ�����
        if (itemSprite != null)
        {
            gachaItemImages.Add(itemSprite); // �̹��� ����Ʈ�� �߰�
            UpdateInventoryUI(); // UI ������Ʈ
        }
    }

    private void OnEnable()
    {
        UpdateInventoryUI(); // UI ������Ʈ
    }

    // ��������Ʈ ��ηκ��� ��������Ʈ�� �ε��ϴ� �Լ�
    private Sprite LoadSpriteFromPath(string path)
    {
        return Resources.Load<Sprite>(path); // Resources �������� ��������Ʈ �ҷ�����
    }

    // UI ������Ʈ (���ο� �������� �߰��Ǹ� �κ��丮�� �ݿ�)
    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // ���� UI ���� ����
        }

        foreach (var itemImage in gachaItemImages)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel); // ���� ����
            slot.GetComponent<Image>().sprite = itemImage; // ���Կ� �̹��� ����
        }
    }
}
