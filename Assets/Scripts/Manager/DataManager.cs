using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaedDigimonData
{
    public float id;
    public float PosX;
    public float PosZ;
    public int rank;
    public string className;

    public GachaedDigimonData(float id, float posX, float posZ)
    {
        this.id = id;
        PosX = posX;
        PosZ = posZ;
    }
}

public class GameData
{
    public string id;
    public string name;
    public float hp;
    public float evolutionGauge;
    public float skillDamage;
    public List<GachaedDigimonData> Digimondata;
}
public class DataManager : Singleton<DataManager>
{
    public void LoadData()
    {

    }

    public void SaveData()
    {   

    }
}
