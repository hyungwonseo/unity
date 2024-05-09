using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage;        //열린 이미지
    public GameObject itemPrefab;   //담겨있는 아이템의 프리펩

    public bool isClosed = true;    //true=닫혀있다　false=열려 있다.

    public int arrangeId = 0;       //배치 식별에 사용

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    //접촉 (물리)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isClosed && collision.gameObject.tag == "Player")
        {
            //상자가 닫혀 있는 상태에서 플레이어와 접촛
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false;   //열린 상태로 하기
            if (itemPrefab != null)
            {
                //프리펩으로 아이템 만들기
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }

            //배치 Id 기록
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
