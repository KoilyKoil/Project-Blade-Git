using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    //스스로를 제거하는 함수
    public void DoDestruction(){
        Destroy(gameObject);
    }

    public void DestroyResetTime(){
        //시간 정상화
        Time.timeScale=1.0f;

        //플레이어 상태 정상화
        GameObject.Find("Player").GetComponent<PlayerState>().waitTillBreak=false;

        Destroy(gameObject);
    }
}
