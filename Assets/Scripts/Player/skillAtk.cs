using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillAtk : MonoBehaviour
{
    private Digimon digimon;

    private void Awake()
    {
        digimon = GetComponentInParent<Digimon>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IHIt hit = other.GetComponent<IHIt>();
            hit.Hit(digimon.SkillDamage);
            Debug.Log(other.gameObject.tag);
            Invoke("Off", 1);
        }
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

}
