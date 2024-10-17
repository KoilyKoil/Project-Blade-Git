using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowCombo : MonoBehaviour
{
    public BlockDataManager bdm;        //블록 데이터매니저 자체를 따옴
    Text textComp;
    string textShow="";

    void Start(){
        textComp=GetComponent<Text>();
    }

    void Update(){
        //만약 콤보수가 3 이하면 공백처리
        if(bdm.combo<3){textShow="";}
        //만약 콤보수가 4 이상이면 콤보수 표시
        else{textShow=(bdm.combo-2).ToString()+"Combo!";}
        //콤보수 텍스트 적용
        textComp.text=textShow;
    }
}
