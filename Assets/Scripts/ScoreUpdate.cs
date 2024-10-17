using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    BlockDataManager bdm;               //블록 데이터매니저
    Text txt;                           //점수판 텍스트

    void Start(){
        //블록 데이터 매니저 가져오기
        bdm=GameObject.Find("BlockDataManager").GetComponent<BlockDataManager>();
        //텍스트 컴포넌트 가져오기
        txt=GetComponent<Text>();
    }
    void Update()
    {
            //실시간으로 점수 갱신
            txt.text=bdm.score.ToString();
    }
}
