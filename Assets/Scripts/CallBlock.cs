using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class CallBlock : MonoBehaviour
{
    //블록 데이터매니저 자체를 따옴
    public BlockDataManager bdm;
    //복제품을 저장하기 위한 장소 선언
    public Transform cloneZone;
    //목표 업그레이드 스택값
    public int upgradeStack=0;
    
    //시스템에서 사용되는 업그레이드값
    int realUpgradeStack=0;
    //현재 레벨상태. 스택이 채워질 때마다 1씩 증가
    int level=0;

    void Start(){
        realUpgradeStack=upgradeStack;
    }

    void Update(){
        //게임플레이 상태일 때만 블록 생성
        if(bdm.onGameplay){
            //현재 남아있는 블록이 없다면
            if(bdm.remainBlock<=0){
                //현재 레벨+3~5개의 블록 개수 설정
                int pickedNum=Random.Range(3+level, 5+level);
                //남은 블록 개수 설정
                bdm.remainBlock=pickedNum;

                //여러개의 블록 스폰시 블록간 간격 조절용 변수 선언
                float spawnYSet=2.7f;
                //최초 스폰포인트 선언
                Vector3 spawnVec=bdm.destination;

                //설정된 블록 개수만큼
                for(int block=0;block<pickedNum;block++){
                    //블록 생성
                    GameObject newBlock=Instantiate(bdm.block, spawnVec, Quaternion.identity);
                    newBlock.transform.parent=cloneZone;
                    newBlock.SetActive(true);
                    //블록 데이터 설정, 체력은 현재 레벨의 영향을 받음
                    newBlock.GetComponent<BlockData>().blockHP=Random.Range(1, 1+level);
                    newBlock.GetComponent<BlockData>().bdm=bdm;
                    newBlock.GetComponent<Rigidbody2D>().mass=3;
                    //블록 체력에 따른 이미지 색상 설정
                    switch(newBlock.GetComponent<BlockData>().blockHP){
                        case 1:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(160f/255f, 110f/255f, 240f/255f, 1f);
                            break;
                        case 2:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(115f/255f, 127f/255f, 225f/255f, 1f);
                            break;
                        case 3:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(122f/255f, 186f/255f, 234f/255f, 1f);
                            break;
                        case 4:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(135f/255f, 230f/255f, 146f/255f, 1f);
                            break;
                        case 5:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(217f/255f, 219f/255f, 35f/255f, 1f);
                            break;
                        case 6:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(202f/255f, 148f/255f, 55f/255f, 1f);
                            break;
                        case 7:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(205f/255f, 62f/255f, 72f/255f, 1f);
                            break;
                        default:
                            newBlock.GetComponent<SpriteRenderer>().color=new Color(123f/255f, 123f/255f, 123f/255f, 1f);
                            break;

                    }
                    //스폰 간격 조정
                    spawnVec.y=spawnVec.y+spawnYSet;
                }
            }
        }else{      //게임 진행상태가 아닐때
            //업그레이드상태 초기화
            realUpgradeStack=upgradeStack;
            level=0;
        }
        

        //현재 업그레이드 스택을 모두 채웠다면
        if(bdm.stackNow>=realUpgradeStack){
            //레벨 1 증가
            level++;
            //스택 초기화
            bdm.stackNow=0;
            //현재 레벨값만큼 목표 스택값 증가
            realUpgradeStack+=level;
        }
    }

    //깔림상태 돌입시 레벨 낮추기용 함수
    public void MinusLevel(){
        //내부 레벨 값 조정
        level-=1;
        //0이하가 될 시 레벨 재조정
        if(level<0){
            level=0;
        }
    }
}
