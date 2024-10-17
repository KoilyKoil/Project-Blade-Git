using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superAttack : MonoBehaviour
{
    //플레이어의 상태를 가져옴
    public PlayerState plrStat;

    void OnTriggerEnter2D(Collider2D obj) {    
        //공격 이펙트가 블록에 닿은 경우
        if(obj.gameObject.CompareTag("Block")){
            //플레이어가 블록에 잠긴 상태라면
            if(plrStat.waitTillBreak==true){
                //잠김상태 해제
                plrStat.waitTillBreak=false;
                //블록 제거
                obj.gameObject.GetComponent<BlockData>().BlockBreak();
            }else{
                //블록의 체력 감소
                obj.gameObject.GetComponent<BlockData>().BlockBreak();
            }
        }
    }
}
