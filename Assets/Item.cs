using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int count;

    private void OnDisable()
    {
        count++;
    }

    public void GachaActive()
    {
        if (count >= 1)
        {

        }
    }
}
