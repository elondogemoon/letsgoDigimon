using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int count;

    private void Start()
    {
        count = 0;
        GachaActive(); 
    }

    private void OnDisable()
    {
        count++;
        GachaActive();
    }
    public void OnGachaButtonClick()
    {
        if (count > 0)
        {
            count--; 
            GachaActive(); 
        }
    }

    public void GachaActive()
    {
        if (count == 0) 
        {
            UiManager.Instance.ActiveGachaBtn(false);
        }
        else if (count <= 1) 
        {
            UiManager.Instance.ActiveGachaBtn(true);
        }
        
    }
}
