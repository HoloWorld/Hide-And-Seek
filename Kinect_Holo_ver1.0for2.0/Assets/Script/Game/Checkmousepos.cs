using UnityEngine;
using System.Collections;

public class Checkmousepos : MonoBehaviour {

    GameObject handpre;
    void Awake()
    {
        //Instantiate(handpre, new Vector3(0f,0f,0f), Quaternion.identity);       
        //손의 오브젝트를 읽어온다.
        handpre = GameObject.FindGameObjectWithTag("Hand");
    }

    void Update()
    {
        //마우스 클릭 부분을 처리하기 위한 부분이다.
        RaycastHit hit;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //현재 바닥의 좌표를 알아냄
        if (Physics.Raycast(ray, out hit))
            checktarget(hit);
    }
    void OnMouseOver()
    {
        
    }
    void checktarget(RaycastHit hit)
    {
        //Debug.Log(hit.point);
        if (hit.transform.tag == "Map")
        {
            handpre.transform.position = hit.point;
            //handpre.transform.Translate(new Vector3(0, 10.0f, 0));
        }
            
        //맵 위일 경우는 위치를 옮겨준다.
            //Instantiate(handpre, hit.point, Quaternion.identity);       
        //Instantiate(handpre, new Vector3(hit.point.x, 5.0f, hit.point.z), Quaternion.identity);       
        
        
    }
}
