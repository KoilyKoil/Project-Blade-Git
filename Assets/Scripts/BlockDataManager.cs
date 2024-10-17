using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataManager : MonoBehaviour
{
    //블록 현재 스택 설정
    public int stackNow=0;
    //남아있는 블록 확인
    public int remainBlock=0;
    //파괴한 블록 수 저장
    public int score=0;
    //게임 시작 및 종료시 받아낼 최고점수
    public int bestScore=0;
    //콤보 데이터 관리
    public int combo=0;
    //필살 게이지 관리
    public int superGauge=0;


    //블록 생성좌표
    public Vector3 destination;
    //블록 오브젝트
    public GameObject block;
    //게임플레이상태 확인
    public bool onGameplay=false;

    public void ResetCombo(){
        combo=0;
    }
}
