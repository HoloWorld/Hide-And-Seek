using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Handdown : MonoBehaviour {


    public float downspeed = 1.0f;
    public float defaultheight = 5.0f;
    private bool isdown = true;
    private bool keyisdown = false;
    public float minimunheight = 0.0f;
    public int nextscene = 1;

    private KeyCode downkey;
    void Awake()
    {
        downkey = KeyCode.K;
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //gameObject.transform.Translate(new Vector3(0, -downspeed, 0));

        //키가 눌러졌을 경우 내려감
        if (Input.GetKeyDown(downkey))
        {
            keyisdown = true;
        }
        //키를 뗏을 경우 올라감
        if(Input.GetKeyUp(downkey))
        {
            keyisdown = false;
        }

        Debug.Log("Key is down : " + keyisdown + " / is Down " + isdown);
        if (keyisdown)
        {
            //어떤 물체에 부딪혔거나, 최하 높이에 도달했을 경우는 안내려감
            if (isdown)
            {
                gameObject.transform.Translate(new Vector3(0, -downspeed,0));
                //gameObject.transform.Translate(new Vector3( -downspeed, 0,0));
            }
        }
        else
        {
            //키를 뗏을 경우 올라감
            if(gameObject.transform.position.y < defaultheight)
                gameObject.transform.Translate(new Vector3(0,downspeed,0));
        }

        //버그 체크
        if (gameObject.transform.position.y < minimunheight)
            isdown = false;
        if (gameObject.transform.position.y >= defaultheight)
            isdown = true;

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Colision to Map : " + col.transform.tag);
        //맵에 부딪혔을 경우는 더 안내려감
        if (col.transform.tag == "Map")
        {
            isdown = false;
        }
        //벌레일 경우 죽임
        if(col.transform.tag == "Bug")
        {
            GameManager.gamescore += col.transform.GetComponent<NavMeshAgent>().speed * 10;
            col.transform.GetComponent<NavMeshAgent>().speed = 0;
            col.GetComponent<Animator>().SetTrigger("IsDead");
        }

        if(col.transform.tag == "guu")
        {
            SceneManager.LoadScene(nextscene);
            Destroy(col.gameObject);
        }

        if(col.transform.tag == "BugHead")
        {
            col.GetComponentInParent<Bugmovement>().playerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.transform.tag == "Map")
        {
            isdown = true;
        }
    }
        

}
