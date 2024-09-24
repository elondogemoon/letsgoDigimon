using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionAtk : MonoBehaviour
{
    [SerializeField] BoxCollider boxcollider;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Evolution());
    }

    public IEnumerator Evolution()
    {
        yield return new WaitForSeconds(2f);
        boxcollider.enabled = true;
        Invoke("Off", 0.5f);
    }

    public void Off()
    {
        boxcollider.enabled = false;
    }
    
}
