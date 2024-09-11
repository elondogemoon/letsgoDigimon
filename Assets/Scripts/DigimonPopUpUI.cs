using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigimonPopUpUI : MonoBehaviour
{
    public List<Image> DigimonImg = new List<Image>();
    private Dictionary<string, GachaResult> digimonData = new Dictionary<string, GachaResult>();

    private void OnEnable()
    {

    }

    private void Start()
    {
        DataManager.Instance.LoadResult(digimonData);
        SetDigimonUi();

    }

    private void SetDigimonUi()
    {
        int index = 0;

        // ��ųʸ����� �����͸� ��ȸ
        foreach (var data in digimonData.Values)
        {
            if (index >= DigimonImg.Count)
            {
                break; // ���Ժ��� �� ���� �����͸� ������ ���� ����
            }

            // Digimon �̹����� ���� (����: GachaResult�� �̹��� �����Ͱ� �ִٰ� ����)
            Sprite digimonSprite = Resources.Load<Sprite>($"Images/{data.name}"); // ��δ� �ʿ信 ���� ����
            if (digimonSprite != null)
            {
                DigimonImg[index].sprite = digimonSprite; // ���Կ� �̹��� ����
                DigimonImg[index].gameObject.SetActive(true); // ���� Ȱ��ȭ
            }
            else
            {
                DigimonImg[index].gameObject.SetActive(false); // �̹����� ������ ��Ȱ��ȭ
            }

            index++;
        }

        // ���� ���� ��Ȱ��ȭ
        for (int i = index; i < DigimonImg.Count; i++)
        {
            DigimonImg[i].gameObject.SetActive(false);
        }
    }

}
