using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenPoint : MonoBehaviour
{
    public GameObject objPrefab;    //발생 시킬 Prefab 데이터

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ObjectCreate()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y,-1.0f);
        //Prefab으로 GameObject 만들기
        Instantiate(objPrefab, pos, Quaternion.identity);
    }
}
