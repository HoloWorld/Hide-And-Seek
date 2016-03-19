using UnityEngine;
using System.Collections;

public class SetTheManX : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void setTheXPosition(float x)
    {
        this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
    }
}
