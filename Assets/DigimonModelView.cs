using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigimonModelView : MonoBehaviour
{
    [SerializeField] GameObject agumon;
    [SerializeField] GameObject veemon;
    private string selectedDigimonName;

    public void StartWithSelectDigimon(string digimonName)
    {
        // ���õ� ������ �̸� ����
        selectedDigimonName = digimonName;

        // ��� ������ ��Ȱ��ȭ
        agumon.SetActive(false);
        veemon.SetActive(false);

        // ���õ� ������ Ȱ��ȭ
        if (selectedDigimonName == "Agumon")
        {
            agumon.SetActive(true);
        }
        else if (selectedDigimonName == "Veemon")
        {
            veemon.SetActive(true);
        }
    }
}
