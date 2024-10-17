using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class superMove : MonoBehaviour
{
    Button btn;                         //오브젝트의 버튼 컴포넌트
    Image img;                          //이미지 값 설정
    GameObject cam;                         //카메라 오브젝트 설정
    PlayerState plrStat;                //플레이어 상태를 가져오기 위함
    public BlockDataManager bdm;        //블록 데이터매니저
    public float screenTime=0.3f;         //연출 진행 시간
                                          //0.5 == 약 4~5초로 측정됨
    public float zoomSize=2;              //연출용 줌 설정값
    public GameObject superOption;      //필살기 진행 설정값 (기술 처리)

    public AudioSource snd_Super;       //필살기 사용 사운드

    void Start(){
        //게임오브젝트의 이미지 컴포넌트를 가져옴
        img=GetComponent<Image>();
        //게임오브젝트의 버튼 컴포넌트를 가져옴
        btn=GetComponent<Button>();
        //카메라 오브젝트를 찾아옴
        cam=GameObject.Find("Main Camera");
        //플레이어 오브젝트를 찾아옴
        plrStat=GameObject.Find("Player").GetComponent<PlayerState>();
    }

    void Update()
    {
        //슈퍼게이지 값에 따라 필살기 이미지가 채워짐
        img.fillAmount=(bdm.superGauge)/1000f;
        //만약 필살 게이지가 이상이면
        if(bdm.superGauge>=1000f){
            //버튼 클릭 활성화
            btn.interactable=true;
            //버튼 색 변경
            GetComponent<Image>().color=new Color(133f/255f, 141f/255f, 0, 1f);
        }else{
            //버튼 색 변경
            GetComponent<Image>().color=new Color(36/255f, 36/255f, 36/255f, 1f);
        }

        //디버깅용 키입력처리
        if(Input.GetKeyDown(KeyCode.V) && bdm.superGauge>=1000f){DoSuper();}
    }

    //필살기 버튼 클릭시 처리
    public void DoSuper(){
        //플레이어가 블록에 깔린게 아닐 때
        if(plrStat.waitTillBreak==false){
            //버그 방지용 플레이어 상태 변경
            plrStat.waitTillBreak=false;

            //필살 관련 데이터 비활성화 처리
            btn.interactable=false;
            img.fillAmount=0;
            bdm.superGauge=0;
            bdm.combo=0;
            //버튼 색 변경
            GetComponent<Image>().color=new Color(36/255f, 36/255f, 36/255f, 1f);

            //필살기 시각적 연출 처리 시작
            StartCoroutine(ScreenBlack());
        }
    }

    //필살기 진행시간 (암전, 줌, 슬로우 등) 처리
    IEnumerator ScreenBlack(){
        //시간진행용
        float timer=0;

        ////연출 처리
        //사운드 재생
        snd_Super.Play();
        //원본값 임시 저장
        float sizeTemp=cam.GetComponent<Camera>().orthographicSize; //카메라 줌
        float yPosTemp=cam.GetComponent<CamFollowPlr>().cameraYPos; //Y좌표차
        //설정값 변경
        cam.GetComponent<CamFollowPlr>().cameraYPos=0f;
        Time.timeScale=0.05f;

        //설정한 시간에 도달할 때까지
        while(timer<screenTime){
            //카메라의 줌을 서서히 높여줌
            cam.GetComponent<Camera>().orthographicSize=Mathf.Lerp(sizeTemp, zoomSize, (timer*1.2f)/screenTime);

            //시간값을 더해줌
            timer+=Time.smoothDeltaTime/screenTime;
            //쿨타임이 끝나기 전까진 진행 안함
            yield return null;
        }

        //쿨타임 종료 후 연출 정상화
        cam.GetComponent<Camera>().orthographicSize=sizeTemp;
        cam.GetComponent<CamFollowPlr>().cameraYPos=yPosTemp;
        Time.timeScale=1f;

        //오브젝트 호출(임시)
        for(int i=0;i<7;i++){
            Vector3 plrPos=GameObject.Find("Player").transform.position;
            GameObject superTemp=Instantiate(superOption, new Vector3(plrPos.x, plrPos.y+1f*i, plrPos.z), Quaternion.identity);
            superTemp.SetActive(true);
        }
    }
}
