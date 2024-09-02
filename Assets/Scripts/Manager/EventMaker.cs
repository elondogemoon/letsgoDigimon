using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour,IEventMake
{
    [SerializeField] Animator animator;

    Action _eventInvokeHandler;

    private void Start()
    {
        EventManager.Instance.RegisterCurEventmaker(true, this);
    }

    private void OnDisable()
    {
        _eventInvokeHandler = null;
    }

    public void Subscribe(bool isSubscribe, Action callback)
    {
        if(isSubscribe)
            _eventInvokeHandler += callback;
        else
            _eventInvokeHandler-= callback;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeEvent();
        }
    }

    public void InvokeEvent()
    {
        if(_eventInvokeHandler !=null)
            _eventInvokeHandler.Invoke();
    }
}
