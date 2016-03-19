using UnityEngine;
using System.Collections;

public class SetTheManZ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void setTheZPosition(float z)
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,z);
    }
}
