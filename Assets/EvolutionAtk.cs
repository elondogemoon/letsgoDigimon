using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionAtk : MonoBehaviour
{
    [SerializeField] BoxCollider collider;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Evolution());
    }

    public IEnumerator Evolution()
    {
        yield return new WaitForSeconds(2);
        collider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IHIt hit = other.GetComponent<IHIt>();
            hit.Hit(100);
        }
        
    }
}
