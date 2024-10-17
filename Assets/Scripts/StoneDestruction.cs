using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestruction : MonoBehaviour
{
    public float coolStone=3f;      //돌이 사라지는 시간 설정
    void Awake()
    {   
        //제거 코루틴 설정
        StartCoroutine(KillStone());
    }

    IEnumerator KillStone(){
        //타이머 설정
        float num=0f;
        //설정한 시간이 되기 전까지
        while(num<coolStone){
            //시간 흐름
            num+=1*Time.smoothDeltaTime/coolStone;
            yield return null;
        }
        //스스로를 제거
        Destroy(gameObject);
    }
}
