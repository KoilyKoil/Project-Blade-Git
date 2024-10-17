using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //상태 변수
    public bool onGround=false;     //지상상태 구분.
    public bool waitTillBreak=false;    //블록에 잠긴 상태 구분.
    public bool gameOver=false;         //게임오버 상태 처리
    public bool justAttackOK=false;     //저스트 어택 상태 구분

    //기타 변수
    public int HP=3;                //플레이어의 체력
    public int maxHP=3;             //플레이어 최대체력
    public GameObject[] HPUI={};      //체력 UI에 접근하기 위함
    public GameObject replayBtn;        //리플레이버튼 UI에 진입하기 위함
    public BlockDataManager bdm;        //블록 데이터매니저 자체를 따옴
    public float bonusRange=1.5f;       //보너스점수 증가 폭 설정

    public AudioSource snd_Down;        //착지 사운드
    public AudioSource snd_Damage;        //깔림 사운드
    public AudioSource snd_BGM;         //배경음

    //접근불가 변수
    BoxCollider2D boxCol;           //플레이어의 판정
    int bonusScore=2000;            //추가목숨 부여를 위한 목표점수 값

    void Start(){
        //플레이어의 판정박스 정보를 가져옴
        boxCol=GetComponent<BoxCollider2D>();
    }

    //점프 불능 버그 방지용
    void Update(){
        //보너스 목숨 획득의 조건을 충족할 시
        if(bdm.score>bonusScore){
            //목숨 증가
            HP++;
            //최대 목숨은 넘기지 않게 처리
            if(HP>maxHP){HP=maxHP;}
            //목숨 정보 UI에 반영
            HPUI[HP-1].SetActive(true);
            //목표점수 증가
            bonusScore=(int)(bonusScore*bonusRange);
        }
    }

    //충돌 감지
    void OnCollisionEnter2D(Collision2D obj) {
        //플레이어가 땅에 닿아있는 경우
        if(obj.gameObject.CompareTag("Ground")){
            //지상 상태로 변경
            onGround=true;
            //콜라이더 크기 변경
            //이는 블록이 플레이어를 성공적으로 가리게 하기 위함
            boxCol.offset=new Vector2(boxCol.offset.x, -1.6f);
            boxCol.size=new Vector2(boxCol.size.x, 0.09f);

            //플레이어 애니메이션 및 사운드 처리
            GetComponent<Animator>().SetBool("InAir", false);
            snd_Down.Play();

        //플레이어가 땅에 있으면서 블록에 닿은 경우
        }else if(obj.gameObject.CompareTag("Block") && onGround==true && transform.position.y<=-0.85f && waitTillBreak==false){
            //목숨이 하나 감소됨. 왜냐하면 플레이어가 완전히 덮였기 때문.
            HP--;
            //체력 UI도 한칸 비활성화
            HPUI[HP].SetActive(false);
            //블록에 잠긴 상태 활성화
            //해당 상태에서는 플레이어의 점프, 쉴드가 잠김
            waitTillBreak=true;
            
            //내부 강화레벨 감소
            bdm.GetComponent<CallBlock>().MinusLevel();
            //콤보 상태 초기화
            bdm.ResetCombo();

            //사운드 처리
            snd_Damage.Play();
            
            //만약 플레이어의 체력이 0이 되면
            if(HP<=0 && gameOver==false){
                //게임오버 상태 활성화
                gameOver=true;
                replayBtn.SetActive(true);

                //사운드 처리
                snd_BGM.Stop();

                //점수 대조 후 최고기록을 넘겼으면 점수 갱신처리
                if(bdm.score>bdm.bestScore){
                    bdm.bestScore=bdm.score;
                    PlayerPrefs.SetInt("BestScore", bdm.bestScore);
                }

                //보너스 목표점수 초기화
                bonusScore=2000;
            }
        }
    }
}
