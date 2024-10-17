using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    public int screenWidth=720;             //사용자가 원하는 너비
    public int screenHeight=1080;           //사용자가 원하는 높이

    void Start()
    {
        int deviceWidth=Screen.width;
        int deviceHeight=Screen.height;

        //SetResolution 함수 사용
        Screen.SetResolution(screenWidth, (int)(((float)deviceHeight/deviceWidth)*screenWidth), true);

        //기기의 해상도 비가 더 큰 경우
        if((float)screenWidth/screenHeight<(float)deviceWidth/deviceHeight){
            //너비를 새로 설정
            float newWidth=((float)screenWidth/screenHeight)/((float)deviceWidth/deviceHeight);
            //계산 결과를 토대로 Rect 새로 설정
            Camera.main.rect=new Rect((1f-newWidth)/2f, 0f, newWidth, 1f);
        }else{      //게임의 해상도비가 더 큰 경우
            float newHeight=((float)deviceWidth/deviceHeight)/((float)screenWidth/screenHeight);
            //계산 결과를 토대로 Rect 새로 설정
            Camera.main.rect=new Rect(0f, (1f-newHeight)/2f, 1f, newHeight);
        }
    }
}
