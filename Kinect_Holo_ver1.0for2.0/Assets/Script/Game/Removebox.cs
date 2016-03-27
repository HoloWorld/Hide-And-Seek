using UnityEngine;
using System.Collections;

public class Removebox : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        Debug.Log("Exit triger");
        Destroy(col.gameObject);
    }
}
