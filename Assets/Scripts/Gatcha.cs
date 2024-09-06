using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatcha : MonoBehaviour
{
    [SerializeField] List<float> weight = new List<float>();



    private void Start()
    {
        GetRandomByWeight(weight);
    }
    private int GetRandomByWeight(List<float> weight)
    {
        float totalWeight = 0;

        foreach(var i in weight)
        {
            totalWeight += i;
        }

        float randomValue = Random.Range(0,totalWeight);
        float accumulatedWeight = 0;
        
        for(int i =0;i<weight.Count;i++)
        {
            accumulatedWeight += weight[i];
            if (randomValue < accumulatedWeight)
            {
                Debug.Log(i);
                return i;
            }
        }
        return 0;
    }
}
