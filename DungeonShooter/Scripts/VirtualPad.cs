using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 70;    // 탭이 움직이는 최대 거리
    public bool is4DPad = false;    // 상하좌우로 움직이는지 여부
    GameObject player;              // 조작 할 플레이어 GameObject
    Vector2 defPos;                 // 탭의 초기 좌표
    Vector2 downPos;                // 터치 위치

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 캐릭터 가져오기
        player = GameObject.FindGameObjectWithTag("Player");
        // 탭의 초기 좌표
        defPos = GetComponent<RectTransform>().localPosition;
    }
    // Update is called once per frame
    void Update()
    {
    }

    // 다운 이벤트
    public void PadDown()
    {
        // 마우스 포인트의 스크린 좌표
        downPos = Input.mousePosition;
    }
    // 드래그 이벤트 
    public void PadDrag()
    {
        // 마우스 포인트의 스크린 좌표
        Vector2 mousePosition = Input.mousePosition;
        // 새로운 탭 위치 구하기
        Vector2 newTabPos = mousePosition - downPos;// 마우스 다운 위치로 부터의 이동 거리
        if (is4DPad == false)
        {
            newTabPos.y = 0; // 횡스크롤 일 때는  Y 값을 0 으로 한다.
        }
        // 이동 벡터 계산하기
        Vector2 axis = newTabPos.normalized; // 벡터를 정규화
        // 두 점의 거리 구하기
        float len = Vector2.Distance(defPos, newTabPos);
        if (len > MaxLength)
        {
            // 한계거리를 넘겼기 때문에 한계 좌표로 설정
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }
        // 탭 이동 시키기
        GetComponent<RectTransform>().localPosition = newTabPos;
        // 플레이어 캐릭터 이동 시키기
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(axis.x, axis.y);
    }
    // 업 이벤트
    public void PadUp()
    {
        // 탭 위치의 최기화 
        GetComponent<RectTransform>().localPosition = defPos;
        // 플레이어 캐릭터 정지 시키기
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(0, 0);
    }

    //공격
    public void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ArrowShoot shoot = player.GetComponent<ArrowShoot>();
        shoot.Attack();
    }
}
