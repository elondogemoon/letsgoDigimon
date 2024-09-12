using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargrayMonSkill : MonoBehaviour
{
    private readonly Digimon _digimon;
    [SerializeField] GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
            prefab.SetActive(true);
        }
    }
}
