using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckJustAttack : MonoBehaviour
{
    public GameObject player;       //플레이어 오브젝트를 가져옴
    public Vector3 pos;             //저스트어택 위치 설정용
    PlayerState plrStat;             //플레이어 상태 가져옴

    void Start(){
        //플레이어 상태를 가져옴
        plrStat=player.GetComponent<PlayerState>();
    }

    void Update(){
        //오브젝트의 위치와 플레이어의 위치 동기화
        transform.position=pos;
        plrStat.justAttackOK=false;
    }

    void OnTriggerStay2D(Collider2D obj) {
        //공중에서 블록이 감지되어 있을 때에만
        if(obj.gameObject.CompareTag("Block") && plrStat.onGround==true && plrStat.waitTillBreak==false){
            //저스트 어택 상태 활성화
             plrStat.justAttackOK=true;
        }
    }
}
