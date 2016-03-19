using UnityEngine;
using System.Collections;

public class CameraPerspectiveController : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
    private Vector3 justAgoPosition;
    GameObject goTemp;

    // Use this for initialization
    void Start () {
        justAgoPosition=offset = transform.position - player.transform.position;
        goTemp = GameObject.FindWithTag("Helicopter");
    }

    // Update is called once per frame
    void Update () {
        //        transform.position = player.transform.position+ offset;
        //        transform.position = new Vector3(player.transform.position.x*2,0.0f, 0.0f);//player.transform.position.z);

        float n_x, n_y, n_z;
        Vector3 Vector1 = justAgoPosition - goTemp.transform.position;
        Vector3 Vector2 = player.transform.position - goTemp.transform.position;

        n_x = Vector1.y * Vector2.z - Vector1.z * Vector2.y;
        n_y = Vector1.z * Vector2.x - Vector1.x * Vector2.z;
        n_z = Vector1.x * Vector2.y - Vector1.y * Vector2.x;
        Vector3 plz=(new Vector3(n_x, n_y, n_z));
        //        plz.Normalize(); 이렇게 하면 크기가 1인 벡터가 됨

        Vector3 setStdOfObject = new Vector3(goTemp.transform.position.x, goTemp.transform.position.y,goTemp.transform.position.z);//중심점 결정

        transform.RotateAround(setStdOfObject,plz, Vector3.Angle(justAgoPosition-goTemp.transform.position,player.transform.position - goTemp.transform.position));
        //기준, 외적 값(축), 각도
        justAgoPosition = player.transform.position;
    }

    //player는 헤드다.
    //걍 트렌스폼은 카메라
    //그리고 goTemp는 헬기
}