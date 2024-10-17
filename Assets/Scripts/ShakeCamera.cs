using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float shakeAmount;               //흔들리는 정도 설정
    float shakeTime;                        //흔들리는 시간 설정

    //카메라 진동 호출용 함수
    public void VibrateCamera(float time){
        //설정한 시간만큼 카메라 진동
        shakeTime=time;
    }

    void Update(){
        //흔들림 설정 시간 변동이 감지되면
        if(shakeTime>0){
            //카메라 진동처리
            transform.position=Random.insideUnitSphere*shakeAmount+transform.position;
            //설정한 시간만큼 설정
            shakeTime-=Time.deltaTime;
        }else{
            //만일 설정 시간이 0 이하로 가면 0으로 재설정
            shakeTime=0f;
        }
    }
}
