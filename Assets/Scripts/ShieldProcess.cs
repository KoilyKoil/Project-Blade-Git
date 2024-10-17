using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProcess : MonoBehaviour
{
    public Rigidbody2D player;                //힘 부여 처리용 플레이어 오브제
    public BlockDataManager bdm;            //블록 데이터매니저 자체를 따옴
    public float blockPower;                //블록에게 부여할 힘
    public float plrPower;                   //플레이어에게 부여할 힘

    void Update(){
        transform.position=player.gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D obj) {    
        //공격 이펙트가 블록에 닿은 경우
        if(obj.gameObject.CompareTag("Block")){
            //블록에 힘 작용
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up*blockPower*bdm.remainBlock, ForceMode2D.Impulse);
            //플레이어에 힘 작용
            player.AddForce(Vector3.down*plrPower, ForceMode2D.Impulse);
        }
    }
}
