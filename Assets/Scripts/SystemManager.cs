using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public BlockDataManager bdm;

    void Start(){
        bdm.bestScore=PlayerPrefs.GetInt("BestScore", 0);
    }

    void OnApplicationQuit() {
        PlayerPrefs.SetInt("BestScore", bdm.bestScore);    
    }
}
