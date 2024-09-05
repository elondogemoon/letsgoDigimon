using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAtk : MonoBehaviour
{
    private Digimon _digimon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IHIt hit = other.GetComponent<IHIt>();
            hit.Hit(_digimon.Damage);
        }
    }
}
