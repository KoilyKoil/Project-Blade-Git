using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpSet : MonoBehaviour
{
    //값 조정용 대상 오브젝트
    public PlayerState plrStat;            //목숨 처리 관련
    public AttackProcess atkPrg;             //공격력 처리 관련
    public CheckJustAttack justAtk;            //저스트어택 처리 관련
    public BlockData bData;              //점수 처리 관련

    public BlockDataManager bdm;                 //블록 데이터 매니저
    public GameObject[] toggleBtn;          //최고점수 상태에 따른 버튼 활성화 여부

    void Update(){
        //버튼 활성화여부 처리
        int bsc=bdm.bestScore;
        if(bsc>=500){toggleBtn[0].SetActive(true);}else{toggleBtn[0].SetActive(false);}
        if(bsc>=1000){toggleBtn[1].SetActive(true);}else{toggleBtn[1].SetActive(false);}
        if(bsc>=2000){toggleBtn[2].SetActive(true);}else{toggleBtn[2].SetActive(false);}
        if(bsc>=5000){toggleBtn[3].SetActive(true);}else{toggleBtn[3].SetActive(false);}
        if(bsc>=10000){toggleBtn[4].SetActive(true);}else{toggleBtn[4].SetActive(false);}
        if(bsc>=20000){toggleBtn[5].SetActive(true);}else{toggleBtn[5].SetActive(false);}
    }

    public void ClickBtn(int index){
        //파워업 활성화 처리
        if(toggleBtn[index].GetComponent<Toggle>().isOn==true){
            switch(index){
                case 0:
                    plrStat.maxHP=4;
                    break;
                case 1:
                    plrStat.bonusRange=1.2f;
                    break;
                case 2:
                    atkPrg.atkDmg=2;
                    break;
                case 3:
                    bData.gaugeBonus=10;
                    break;
                case 4:
                    justAtk.pos.y=-0.59f;
                    break;
                case 5:
                    bData.scoreBonus=2;
                    break;
                default:
                    break;
            }
        }else{
            switch(index){
                case 0:
                    plrStat.maxHP=3;
                    break;
                case 1:
                    plrStat.bonusRange=1.5f;
                    break;
                case 2:
                    atkPrg.atkDmg=1;
                    break;
                case 3:
                    bData.gaugeBonus=0;
                    break;
                case 4:
                    justAtk.pos.y=-0.8554693f;
                    break;
                case 5:
                    bData.scoreBonus=0;
                    break;
                default:
                    break;
            }
        }
    }
}
