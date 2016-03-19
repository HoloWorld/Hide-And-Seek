using UnityEngine;
using System.Collections;

public class catchbug : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //벌레를 강제로 움직이게 하기 위한 디버그용 부분
        
    }
    /*void OnTriggerEnter(Collider other)
    {
        //트리거 내에 벌레가 있을 경우 이동을 시켜주는데,
        //알고리즘 수정 하면 불필요.
        Debug.Log("Box Trigger" + other.tag);
        if (other.tag == "Bug")
        {
            Bugmovement.bugmove();
        }
    }*/
}
