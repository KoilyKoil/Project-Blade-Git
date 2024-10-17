using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSystem : MonoBehaviour
{
    //게임 프레임 고정
    void Start()
    {
        Application.targetFrameRate = 30;   
    }
}
