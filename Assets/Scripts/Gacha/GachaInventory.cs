using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaInventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel; // UI���� �������� ��ġ�� �г�
    [SerializeField] private GameObject inventorySlotPrefab; // ���� ������
    [SerializeField] private List<Sprite> gachaItemImages = new List<Sprite>(); // ���� ��í �����۵��� �̹��� ���

    // ��í�� ���� �������� ȹ���� �� ȣ��Ǵ� �޼���
    public void AddItemToInventory(GachaItem gachaItem)
    {
        if (gachaItem.Image != null)
        {
            gachaItemImages.Add(gachaItem.Image); // ������ �̹��� �߰�
            UpdateInventoryUI();
        }
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
