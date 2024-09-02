using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public interface IEventMake
{
    public void Subscribe(bool isSubscribe, Action callback);
}
public class EventManager : Singleton<EventManager>
{
    // Start is called before the first frame update
    private IEventMake _currentEventMaker;
    private List<Action> _actionSubscribeRequestList = new List<Action>();

    public void RegisterCurEventmaker(bool isRegister, IEventMake eventManer)
    {
        if (isRegister)
        {
            _currentEventMaker = eventManer;
            CheckSubscribeRequestList();
        }
        else
        {
            _currentEventMaker = null;
        }
    }
    private void CheckSubscribeRequestList()
    {
        if (_actionSubscribeRequestList.Count > 0)
        {
            foreach( var action in _actionSubscribeRequestList)
            {
                _currentEventMaker.Subscribe(true, action);
            }
            _actionSubscribeRequestList.Clear();
        }
    }
    public void RequestSubscribe(bool isSubscribe, Action callback)
    {
        if (_currentEventMaker == null)
        {
            _actionSubscribeRequestList.Add(callback);
            return;
        }
        _currentEventMaker.Subscribe(isSubscribe, callback);
    }
}
