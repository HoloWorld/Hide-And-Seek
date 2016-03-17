using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Camera cam;
    public GameObject bugdieprefap;
    public AudioClip bugdeadsount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        { 
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Click event called : " + Input.mousePosition);

            if (!Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                return;
            }
            checktarget(hit);
        }

    }
    void checktarget(RaycastHit hit)
    {
        Debug.Log("Hit log : " + hit.transform.tag);
        if(hit.transform.tag == "Bug")
        {
            Instantiate(bugdieprefap, hit.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(bugdeadsount, hit.transform.position);
            Destroy(hit.transform.gameObject);
        }
    }
    
}
