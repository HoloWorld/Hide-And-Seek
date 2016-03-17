using UnityEngine;
using System.Collections;

public class waterfapp : MonoBehaviour {

    public Transform bullet; //포탄 프리팹
    Transform spPoint; //발사 되는 위치
    public int power = 1800;

    int rotspeed = 120; //회전 속도

    void Start()
    {
        spPoint = GameObject.Find("Spanpoint").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        Transform obj = Instantiate(bullet, spPoint.position, spPoint.rotation) as Transform;
        obj.GetComponent<Rigidbody>().AddForce(spPoint.forward * power);
    }
}
