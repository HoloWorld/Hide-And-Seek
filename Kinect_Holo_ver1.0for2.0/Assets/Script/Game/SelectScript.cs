using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour {

    public GameObject[] gamecharacter;
    public GameObject spawnpoint;
    public GameObject enterpoint;
    public GameObject exitpoint;
    public GameObject Selbutton;
    public float sumondelay = 5.0f;

    public static bool charctersel = false;
    private GameObject[] hide;
    private GameObject currentcharcter;

    private NavMeshAgent nav;
    private Animator ani;
    private float bugmovegap;
    
    private int maxbug;
    private int currentbug;
    private bool buglive = false;


    void Awake()
    {
        hide = GameObject.FindGameObjectsWithTag("Hidespot");
        maxbug = gamecharacter.Length;

        Selbutton.SetActive(false);

        for (int i=0; i<maxbug; i++)
        {
            gamecharacter[i].GetComponent<Bugmovement>().enabled = false;
        }
        currentbug = 0;
        Sumonbug(currentbug);
        Invoke("waittime", sumondelay);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!charctersel)
        {
            if (buglive)
            {
                if (IsArrived())
                {
                    ani.SetBool("IsMove", false);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    buglive = false;
                    Destroybug();
                    currentbug--;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    buglive = false;
                    Destroybug();
                    currentbug++;
                }

            }
            else
            {
                if (currentcharcter == null)
                {
                    Sumonbug(currentbug);
                    Invoke("waittime", sumondelay);
                }
            }

            if (buglive)
            {
                if (Random.Range(0, 40) == 1)
                    bugmove();
            }
        }

        if(charctersel)
        {
            for (int i = 0; i < maxbug; i++)
            {
                gamecharacter[i].GetComponent<Bugmovement>().enabled = true;
            }

            DatasaveScript.selectcharnum = currentbug;
            Selbutton.SetActive(true);
        } 
       
	    
	}
    public void Loadnextscene()
    {
        SceneManager.LoadScene(2);
    }
    void Sumonbug(int bugnum)
    {
        if (bugnum < 0)
            bugnum = maxbug - 1;
        if (bugnum == maxbug)
            bugnum = 0;

        Instantiate(gamecharacter[bugnum], spawnpoint.transform.position, spawnpoint.transform.rotation);
        currentcharcter = GameObject.FindGameObjectWithTag("Bug");
        
        nav = currentcharcter.GetComponent<NavMeshAgent>();
        ani = currentcharcter.GetComponent<Animator>();

        nav.SetDestination(enterpoint.transform.position);
    }
    void Destroybug()
    {
        nav.speed = 30.0f;
        nav.SetDestination(exitpoint.transform.position);
    }
    void bugmove()
    {
        nav.speed = Random.Range(5, 30);
        int randpos;
        //임의의 범위를 정해준다.
        randpos = Random.Range(0, hide.Length);

        //nav.enabled = true;
        //다음 목표물을 선택한다.
        nav.SetDestination(hide[randpos].transform.position);
        ani.SetBool("IsMove", true);
    }
    void waittime()
    {
        buglive = true;
    }

    bool IsArrived()
    {
        if (!nav.pathPending)
            if (nav.remainingDistance <= nav.stoppingDistance)
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                    return true;

        return false;
    }
}
