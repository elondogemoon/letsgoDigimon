using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] Button startBtn;
    Dictionary<string,GachaResult> dic = new Dictionary<string,GachaResult>();
    [SerializeField]List<Digimon> digimons = new List<Digimon>();
    public void Play()
    {
        this.gameObject.SetActive(true);
    }

    private void Start()
    {
        DataManager.Instance.LoadResult(dic);
        GameManager.Instance.StartWave();
    }

}
