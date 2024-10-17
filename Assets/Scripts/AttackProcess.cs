using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProcess : MonoBehaviour
{
    public PlayerState plrStat;         //플레이어의 상태를 가져옴
    public GameObject justAtkAnim;      //저스트 어택 애니메이션 오브젝트
    public int atkDmg=1;

    void OnTriggerEnter2D(Collider2D obj) {    
        //공격 이펙트가 블록에 닿은 경우
        if(obj.gameObject.CompareTag("Block")){
            //저스트 어택의 조건을 충족했다면
            if(plrStat.justAttackOK){
                //저스트어택 연출 시작
                Time.timeScale=0.1f;    //게임 시간을 잠깐 느리게 만듦
                //애니메이션 호출
                GameObject eff=Instantiate(justAtkAnim, obj.transform.position, Quaternion.identity);
                eff.SetActive(true);

                //동시에 블록 파괴 진행
                obj.gameObject.GetComponent<BlockData>().BlockBreak();
                //깔림상태 강제 해제
                plrStat.waitTillBreak=false;
            }else{          //저스트 어택이 아니었다면
                //플레이어가 블록에 깔린 상태라면
                if(plrStat.waitTillBreak==true){
                    //잠김상태 해제
                    plrStat.waitTillBreak=false;
                    //블록 제거
                    obj.gameObject.GetComponent<BlockData>().BlockBreak();
                }else{
                    //블록의 체력 감소
                    obj.gameObject.GetComponent<BlockData>().blockHP-=atkDmg;
                }
            }
        }
    }
}
