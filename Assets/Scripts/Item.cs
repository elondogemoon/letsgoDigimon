using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isPool;

    public void OnGachaButtonClick()
    {
        GameManager.Instance.UseEgg();
    }

}
