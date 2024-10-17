using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    public Text scoreBoard;             //점수판 오브젝트
    public Text highScoreBoard;         //최고점수판 오브젝트
    public BlockDataManager bdm;        //데이터

    //오브젝트가 활성화될 때마다
    void Update(){
        //현재 점수와 최고점수 갱신
        scoreBoard.text=bdm.score.ToString();
        highScoreBoard.text=bdm.bestScore.ToString();
    }
}
