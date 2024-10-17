using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlr : MonoBehaviour
{
    public float cameraSpeed=1.0f;      //카메라 이동속도
    public float cameraYPos=5.0f;       //카메라 Y위치조정값

    public GameObject player;           //플레이어 오브젝트

    Vector3 plrPos;                  //플레이어의 현재 좌표 저장용
    Vector3 camPos;                 //카메라의 다음 좌표 저장용
    float cameraHalfWidth, cameraHalfHeight;    //카메라 길이 저장용

    void Start()
    {
        //카메라의 가로 및 세로 절반길이 측정
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    void LateUpdate(){
        //플레이어의 좌표값 초기화
        plrPos=player.transform.position;
        camPos=new Vector3(plrPos.x,plrPos.y+cameraYPos,-10);
        //카메라 위치 갱신
        this.transform.position=Vector3.Lerp(transform.position, camPos, Time.deltaTime*cameraSpeed);
    }
}
