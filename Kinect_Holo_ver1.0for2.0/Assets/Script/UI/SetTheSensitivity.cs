using UnityEngine;
using System.Collections;

public class SetTheSensitivity : MonoBehaviour
{
    public KinectPointController kpc;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void setTheManSensitivity(float y)
    {
        kpc.scale = y;
    }
}
