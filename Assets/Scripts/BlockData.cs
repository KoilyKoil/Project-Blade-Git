using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    public int blockHP=1;           //블록 체력 설정
    public BlockDataManager bdm;    //블록 데이터매니저 자체를 따옴
    GameObject stone;        //파괴시 생성할 블록 오브젝트를 따옴
    public int callStones=5;          //블록 파괴시 생성할 돌의 개수 설정
    public float powerOfStone=2f;       //돌에 부여할 힘 설정
    public int scoreBonus=0;
    public int gaugeBonus=0;

    //사운드 관련
    public AudioSource snd_break;             //파괴 사운드

    void Start(){
        stone=transform.GetChild(0).gameObject;
    }

    void Update(){
        //블록의 체력이 0이 되면
        if(blockHP<=0){
            //스스로를 파괴
            BlockBreak();
        }
    }

    public void BlockBreak(){
        //돌 파편 생성
        for(int num=0;num<callStones;num++){
            //블록 오브젝트 복제
            GameObject temp=Instantiate(stone, transform.position, Quaternion.identity);
            temp.SetActive(true);
            //복제된 오브젝트의 리짓바디 설정
            Rigidbody2D tempRigid=temp.GetComponent<Rigidbody2D>();
            //위로 주는 힘 설정. 고정값
            tempRigid.AddForce(Vector3.up*powerOfStone, ForceMode2D.Impulse);
            //좌우로 주는 힘 설정. 무작위값
            tempRigid.AddForce(Vector3.right*Random.Range(-4, 4), ForceMode2D.Impulse);
        }

        //블록 관련 데이터처리
        bdm.stackNow++;
        int comboscore=bdm.combo-3>0?bdm.combo-3:0;
        bdm.score+=1+scoreBonus+comboscore;
        bdm.combo++;
        bdm.superGauge+=gaugeBonus+bdm.combo;
        bdm.remainBlock--;

        //화면 진동 호출
        GameObject.Find("Main Camera").GetComponent<ShakeCamera>().VibrateCamera(0.05f);
        //사운드 처리
        snd_break.Play();

        //스스로를 파괴
        Destroy(gameObject);
    }

    
}
