using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList;    //베치 데이터

    // Start is called before the first frame update
    void Start()
    {
        //SaveDataList 초기화 
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[] { };
        //씬 이름 불러오기
        string stageName = PlayerPrefs.GetString("LastScene");
        //씬 이름을 키로하여 저장 데이터 읽어오기
        string data = PlayerPrefs.GetString(stageName);
        if (data != "")
        {
            //--- 저장 된 데이터가 존재할 경우  ---
            //JSON에서 SaveDataList로 변환하기
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for (int i = 0; i < arrangeDataList.saveDatas.Length; i++)
            {
                SaveData savedata = arrangeDataList.saveDatas[i]; //배열에서 가져오기
                //태그로 게임 오브젝트 찾기
                string objTag = savedata.objTag;
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for (int ii = 0; ii < objects.Length; ii++)
                {
                    GameObject obj = objects[ii]; //배열에서 GameObject 가져오기
                    //GameObject의 태그 확인하기
                    if (objTag == "Door")       //문
                    {
                        Door door = obj.GetComponent<Door>();
                        if (door.arrangeId == savedata.arrangeId)
                        {
                            Destroy(obj);  //arrangeId가 같으면 제거
                        }
                    }
                    else if (objTag == "ItemBox")   //보물 상자
                    {
                        ItemBox box = obj.GetComponent<ItemBox>();
                        if (box.arrangeId == savedata.arrangeId)
                        {
                            box.isClosed = false;   //arrangeIdd가 같으면 열기
                            box.GetComponent<SpriteRenderer>().sprite = box.openImage;
                        }
                    }
                    else if (objTag == "Item")      //아이템
                    {
                        ItemData item = obj.GetComponent<ItemData>();
                        if (item.arrangeId == savedata.arrangeId)
                        {
                            Destroy(obj);   //arrangeId가 같으면 제거
                        }
                    }
                    else if (objTag == "Enemy")      //적
                    {
                        EnemyController enemy = obj.GetComponent<EnemyController>();
                        if (enemy.arrangeId == savedata.arrangeId)
                        {
                            Destroy(obj);   //arrangeId가 같으면 제거
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //배치 Id 설정
    public static void SetArrangeId(int arrangeId, string objTag)
    {
        if (arrangeId == 0 || objTag == "")
        {
            //기록하지 않음
            return;
        }
        //추가 하기 위해 하나 많이 SaveData 배열 만들기
        SaveData[] newSavedatas = new SaveData[arrangeDataList.saveDatas.Length + 1];
        //데이터 복사
        for (int i = 0; i < arrangeDataList.saveDatas.Length; i++)
        {
            newSavedatas[i] = arrangeDataList.saveDatas[i];
        }
        //SaveData 만들기
        SaveData savedata = new SaveData();
        savedata.arrangeId = arrangeId; //Id를 기록
        savedata.objTag = objTag;       //태그 기록
        //SaveData 추가
        newSavedatas[arrangeDataList.saveDatas.Length] = savedata;
        arrangeDataList.saveDatas = newSavedatas;
    }

    //기록된 데이터 저장
    public static void SaveArrangeData(string stageName)
    {
        if (arrangeDataList.saveDatas != null && stageName != "")
        {
            //SaveDataList를 JSON 데이터로 변환
            string saveJson = JsonUtility.ToJson(arrangeDataList);
            //씬 이름을 키로하여 저장
            PlayerPrefs.SetString(stageName, saveJson);
        }
    }
}
