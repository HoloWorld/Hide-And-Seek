using UnityEngine;
using System.Collections;

public class camerarot : MonoBehaviour
{
    //카메라를 회전해 주는 소스이다.
    public float movespeed = 0.1f;
    public float rotationspeed = 1.0f;
    public float smooth = 2.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float amtMove = movespeed * Time.smoothDeltaTime;

        Vector3 movepos;
        movepos.x = 0;
        movepos.y = 0;
        movepos.z = 0;
        Vector3 rotationpos;
        rotationpos.x = 0.0f;
        rotationpos.y = 0.0f;
        rotationpos.z = 0.0f;

        /*if (Input.GetKey(KeyCode.D))
        {
            movepos.z = movespeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movepos.z = -movespeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movepos.x = movespeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movepos.x = -movespeed;
        }*/
        //Q,E 버튼으로 회전한다.
        if (Input.GetKey(KeyCode.Q))
        {
            float tiltAroundY = rotationspeed;
            rotationpos.y = 1.0f;
            transform.Rotate(rotationpos, tiltAroundY);

        }
        if (Input.GetKey(KeyCode.E))
        {
            float tiltAroundY = -rotationspeed;
            rotationpos.y = 1.0f;
            transform.Rotate(rotationpos, tiltAroundY);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            float tiltAroundZ = rotationspeed;
            rotationpos.z = 1.0f;
            transform.Rotate(rotationpos, tiltAroundZ);

        }
        if (Input.GetKey(KeyCode.C))
        {
            float tiltAroundZ = -rotationspeed;
            rotationpos.z = 1.0f;
            transform.Rotate(rotationpos, tiltAroundZ);
        }

        transform.Translate(movepos);
    }
}
