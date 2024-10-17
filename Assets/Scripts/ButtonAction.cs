using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    //조작 가능 변수
    public GameObject Player;           //플레이어 오브젝트
    public GameObject ShieldCollider;   //쉴드판정 오브젝트
    public GameObject AtkCollider;      //공격판정 오브젝트
    public float atkEffPos=1f;             //호출할 공격 이펙트의 위치 조정
    public float shdEffPos=1f;             //호출할 쉴드 이펙트의 위치 조정

    public float jumpPower=10f;         //점프하는 힘 설정

    //쉴드 쿨타임 관련
    public float shieldCoolTime=0.8f;        //쉴드 쿨타임 설정
    public Image shieldFillAmount;           //쉴드 쿨타임 이미지 설정

    //점프 쿨타임 관련
    public float jumpCoolTime=0.2f;        //점프 쿨타임 설정
    public Image jumpFillAmount;           //점프 쿨타임 이미지 설정

    //주소 관련
    public BlockDataManager bdm;        //블록 데이터매니저 자체를 따옴
    public Transform cloneZone;             //복제품이 저장된 장소 선언
    public GameObject playBtn;              //플레이버튼

    //사운드 관련
    public AudioSource snd_jump;             //점프 사운드
    public AudioSource snd_menu;             //점프 사운드    
    public AudioSource snd_BGM;              //배경음

    //조작 불가능 변수
    Rigidbody2D rigid;                      //플레이어의 리짓 바디 따옴
    PlayerState plrStat;                    //플레이어 상태
    bool shieldAvailable = true;            //쉴드 사용가능상태 설정
    bool jumpAvailable = true;              //점프 사용가능상태 설정

    //코드 실행시 선언. 리짓바디와 플레이어 상태 초기화를 위함
    void Start(){
        rigid=Player.GetComponent<Rigidbody2D>();
        plrStat=Player.GetComponent<PlayerState>();
    }

    //디버깅용 키보드 입력
    void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){JumpButton();}
        if(Input.GetKeyDown(KeyCode.X)){ShieldButton();}
        if(Input.GetKeyDown(KeyCode.C)){AttackButton();}
    }

    //점프버튼 클릭시
    public void JumpButton(){
        //플레이어가 지상에 있고, 블록에 잠긴 상태가 아니며, 게임오버가 안됐다면. 그리고 점프 가능 상태라면
        if(plrStat.onGround==true && plrStat.waitTillBreak==false && plrStat.gameOver==false && jumpAvailable){
            //플레이어의 지상 상태를 해제한 후
            plrStat.onGround=false;
            //플레이어의 콜라이더를 가져옴
            BoxCollider2D boxCol=Player.GetComponent<BoxCollider2D>();           //플레이어의 판정
            //이후 플레이어의 판정 크기 변경
            boxCol.offset=new Vector2(boxCol.offset.x, -0.7f);
            boxCol.size=new Vector2(boxCol.size.x, 1.7f);

            //플레이어에게 힘 부여
            rigid.AddForce(Vector3.up*jumpPower, ForceMode2D.Impulse);

            //플레이어 애니메이션 및 사운드 처리
            Player.GetComponent<Animator>().SetBool("InAir", true);
            snd_jump.Play();

            //쿨타임 처리 시작
            UseCool(jumpCoolTime, 1);
        }
    }

    //쉴드버튼 클릭시
    public void ShieldButton(){
        //플레이어가 블록에 잠긴 상태가 아니고 게임오버가 아니라면
        if(plrStat.waitTillBreak==false && shieldAvailable && plrStat.gameOver==false){
            //플레이어의 현재 좌표 호출
            Vector3 plrPos=Player.transform.position;
            //방어 이펙트 호출
            GameObject temp=Instantiate(ShieldCollider, new Vector3(plrPos.x, plrPos.y+shdEffPos, plrPos.z), Quaternion.identity);
            temp.SetActive(true);

            //플레이어 애니메이션 처리
            Player.GetComponent<Animator>().SetTrigger("Shield");

            //쿨타임 처리 시작
            UseCool(shieldCoolTime, 2);
        }
    }

    //공격버튼 클릭시
    public void AttackButton(){
        //플레이어가 게임오버 상태가 아니라면
        if(plrStat.gameOver==false){
            //플레이어의 현재 좌표 호출
            Vector3 plrPos=Player.transform.position;

            //플레이어 애니메이션 처리
            Player.GetComponent<Animator>().SetTrigger("Attack");

            //공격 이펙트 호출
            GameObject temp=Instantiate(AtkCollider, new Vector3(plrPos.x, plrPos.y+atkEffPos, plrPos.z), Quaternion.identity);
            temp.SetActive(true);
        }
    }

    //플레이버튼 클릭시
    public void PlayButton(GameObject self){
        //플레이버튼 비활성화
        self.SetActive(false);
        //플레이어 게임오버 상태 해제
        plrStat.gameOver=false;
        
        //블록 생성 시작
        bdm.onGameplay=true;

        //사운드 재생
        snd_menu.Play();
        snd_BGM.Play();
        
        //체력상태 원래대로 설정
        plrStat.HP=plrStat.maxHP;
        for(int i=0;i<plrStat.maxHP;i++){
            plrStat.HPUI[i].SetActive(true);
        }
    }

    //리플레이버튼 클릭시
    public void ReplayButton(GameObject self){
        //게임상태 비활성화
        bdm.onGameplay=false;
        //생성된 클론 모두 제거
        foreach(Transform child in cloneZone){
            Destroy(child.gameObject);
        }

        //그 외 데이터 처리
        bdm.stackNow=0;
        bdm.remainBlock=0;
        bdm.score=0;
        bdm.superGauge=0;
        bdm.combo=0;
        //플레이어 상태 재설정
        plrStat.waitTillBreak=false;
        plrStat.onGround=true;
        
        //사운드 재생
        snd_menu.Play();
        
        //플레이버튼 활성화
        playBtn.SetActive(true);
        //리플레이버튼 비활성화
        self.SetActive(false);
    }

    //쿨타임 관련
    //공용 쿨타임 처처리
    public void UseCool(float cool, int opt){
        Image img=null;
        switch(opt){
            case 1:
                //점프 비활성화
                jumpAvailable=false;
                img=jumpFillAmount;
                break;
            case 2:
                //쉴드 비활성화
                shieldAvailable=false;
                img=shieldFillAmount;
                break;
            default:
                break;
        }
        //이미지 잠금처리 시작
        img.fillAmount=1;
        //쿨타임 진행 코루틴 활성화
        StartCoroutine(ResetCooltime(cool, opt, img));
    }

    //쿨타임 시간 진행 처리
    IEnumerator ResetCooltime(float cool, int opt, Image img){
        //쉴드 쿨타임 이미지가 사라지기 전까지
        while (img.fillAmount>0){
            //쿨타임 이미지를 서서히 줄여주고
            img.fillAmount-=1*Time.smoothDeltaTime/cool;
            //null 반환
            yield return null;
        }
        //쿨타임 이미지가 사라지면 기능 활성화
        switch(opt){
            case 1:
                //점프 활성화
                jumpAvailable=true;
                break;
            case 2:
                //쉴드 활성화
                shieldAvailable=true;
                break;
            default:
                break;
        }
    }
}
