using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Singleton<GameLogicManager>
{
    [SerializeField] DigimonModelView view;

    public void OnModelChange(string name)
    {
        view.StartWithSelectDigimon(name);
    }


}
