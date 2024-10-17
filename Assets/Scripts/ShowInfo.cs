using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    bool showNow=false;                     //도움말 여닫힘 표시용

    //도움말 클릭시 처리
    public void InfoClicked(){
        if(showNow){
            //도움말이 켜져있으면 꺼줌
            showNow=false;
        }else{
            //도움말이 꺼져있으면 켜줌
            showNow=true;
        }
        //도움말 전원상태 적용
        gameObject.SetActive(showNow);
    }
}
