using UnityEngine;
using System.Collections;

public class ZoominoutButton : MonoBehaviour {

    public Vector3 MaxSize;
    private Vector3 Size;

    void Update()
    {
        Size.x = Mathf.Abs(Mathf.Sin(Time.time) * MaxSize.x);

        Size.y = Mathf.Abs(Mathf.Sin(Time.time) * MaxSize.y);

        Size.z = Mathf.Abs(Mathf.Sin(Time.time) * MaxSize.z);


        gameObject.transform.localScale = Size;
    }
}
