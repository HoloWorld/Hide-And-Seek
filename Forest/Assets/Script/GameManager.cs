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
        //마우스 클릭 부분을 처리하기 위한 부분이다.
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
        //만약 클릭한 대상이 벌레일 경우 없애준다.
        Debug.Log("Hit log : " + hit.transform.tag);
        if(hit.transform.tag == "Bug")
        {
            Instantiate(bugdieprefap, hit.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(bugdeadsount, hit.transform.position);
            Destroy(hit.transform.gameObject);
        }
    }
    
}
