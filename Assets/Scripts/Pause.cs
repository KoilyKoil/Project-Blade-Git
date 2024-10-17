using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool pauseState=false;                  //일시정지 상태
    public BlockDataManager bdm;            //블록 데이터매니저, 게임 상태를 가져오기 위함

    public void DoPause(){
        //게임 중일때만 작동
        if(bdm.onGameplay){
            //일시정지 상태라면
            if(pauseState){
                //일시정지 해제
                pauseState=false;
                Time.timeScale=1.0f;
            }else{      //일시정지 상태가 아니라면
                //일시정지 활성화
                pauseState=true;
                Time.timeScale=0f;
            }   
        }
    }
}
