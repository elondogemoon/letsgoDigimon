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
        // 선택된 디지몬 이름 저장
        selectedDigimonName = digimonName;

        // 모든 디지몬 비활성화
        agumon.SetActive(false);
        veemon.SetActive(false);

        // 선택된 디지몬만 활성화
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
