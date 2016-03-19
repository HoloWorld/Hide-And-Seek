using UnityEngine;
using System.Collections;

public class cubemove : MonoBehaviour {
    GameObject player; //피할 대상
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       /* RaycastHit hit1;
        // Calculate Ray direction
        Vector3 direction = player.transform.position - transform.position;
        Debug.Log("Directrion : " + direction);
        if (Physics.Raycast(transform.position, direction, out hit1))
        {
            Debug.Log("Current hit : " + hit1.transform.tag);
            if (hit1.transform.tag == "Player")
            {
                Debug.Log("is visible in cam");
                Bugmovement.bugmove();

            }
            else
            {
                Debug.Log("this is behinde the cam");
            }
        }*/

    }
}
