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
        if (Input.GetKeyDown(KeyCode.L))
        {
            Bugmovement.bugmove();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Trigger" + other.tag);
        if (other.tag == "Bug")
        {
            Bugmovement.bugmove();
        }
    }
}
