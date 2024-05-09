using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2;  //제거 시간

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, deleteTime); //이정 시간후 제거하기
    }
    //게임 오브젝트제 접촉
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //접촉한 게임 오브젝트의 자식으로하기
        transform.SetParent(collision.transform);
        //충돌 판정을 비활성
        GetComponent<CircleCollider2D>().enabled = false;
        //물리 시뮬레이션을 비활성
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
