using UnityEngine;
using System.Collections;

public class SetTheManY : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void setTheYPosition(float y)
    {
        this.transform.position = new Vector3( this.transform.position.x,y, this.transform.position.z);
    }
}
