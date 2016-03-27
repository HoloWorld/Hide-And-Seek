using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Camera cam;
    public Animator anim;
    public float levelStartDelay = 2f;
    public GameObject bugprefap;

    public static int bugcount;
    public static float gamescore = 0;
    public float time = 60.0f;
    private float timer;

    private GameObject bugsawn;
    private GameObject levelImage;
    private GameObject Timer;

    public GameObject gamefail;
    public GameObject clearText;

    private Text levelText;
    private Text scoreText;
    
    private int level = 1;
    private int titlenum = 0;

    private bool isGamerun;

    // Use this for initialization

    void Awake()
    {
        InitGame(); 
    }

    void InitGame()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        bugsawn = GameObject.Find("Bugspan");
        
        Timer = GameObject.Find("Clock");

        levelText.text = "Stage - " + level;
        clearText.SetActive(false);
        gamefail.SetActive(false);
        levelImage.SetActive(true);
        
        bugcount = level;
        Invoke("HideLevelImage", levelStartDelay);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        createBug(level);
        Timer.GetComponent<Animator>().SetBool("IsTimer",true);
        time = 60.0f;
        isGamerun = true;
    }

    void createBug(int gamelevel)
    {
        bugprefap.GetComponent<NavMeshAgent>().speed = level * 10;
        for (int i = 0; i < gamelevel; i++)
        {
            Vector3 randpos = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            Instantiate(bugprefap, bugsawn.transform.position + randpos, bugsawn.transform.rotation);
        }
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(isGamerun)
        {
            //마우스 클릭 부분을 처리하기 위한 부분이다.
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //Debug.Log("Click event called : " + Input.mousePosition);

                if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    return;
                }
                checktarget(hit);
                //Debug.Log("Bugcount : " bugcount);

            }
            timer += Time.deltaTime;

            if (timer > 1)
            {
                time -= 1.0f;
                Debug.Log(time);
                timer = 0.0f;
            }

            if (!isbugdead())
            {
                Timer.GetComponent<Animator>().SetBool("IsTimer", false);
                gamescore += time * 10;
                isGamerun = false;
                showbutton();
            }

            if(time == 0.0f)
            {
                isGamerun = false;
                gamefail.SetActive(true);
            }

        }
    }
    void checktarget(RaycastHit hit)
    {
        //만약 클릭한 대상이 벌레일 경우 없애준다.
        Debug.Log("Hit log : " + hit.transform.tag);
        if(hit.transform.tag == "Bug")
        {
            hit.transform.GetComponent<NavMeshAgent>().speed = 0;
            hit.transform.GetComponent<Animator>().SetTrigger("isDead");
            //Destroy(hit.transform.gameObject);
        }
    }

    void showbutton()
    {
        clearText.SetActive(true);
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score : " + gamescore;
    }
    public void goBackTitle()
    {
        SceneManager.LoadScene(titlenum);
    }
    public void loadNextStage()
    {
        level++;
        levelImage.SetActive(true);
        InitGame();
    }
    public static bool isbugdead()
    {
        Debug.Log("Bug count : " + bugcount);
        if (bugcount == 0)
            return false;
        else
            return true;
    }
}
