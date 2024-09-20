using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WargrayMonSkill : MonoBehaviour
{
    private readonly Digimon _digimon;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform tr; 
    [SerializeField] float duration;
    Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Active()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gameObject.transform.position = tr.position;
        rigidbody.velocity = Vector3.zero;
       // prefab.transform.SetParent(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
            prefab.SetActive(true);
        }
    }
}
