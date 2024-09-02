using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subcribe : MonoBehaviour
{
    Animator _animator;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        EventManager.Instance.RequestSubscribe(true, OnEventMakerInvoked);
    }

    private void OnDisable()
    {
        EventManager.Instance.RequestSubscribe(false, OnEventMakerInvoked);
    }

    public void OnEventMakerInvoked()
    {
        _animator.SetTrigger("Atk");
    }
}
