using UnityEngine;
using System.Collections;


public struct movePos //숨은 위치를 저장하기 위한 구조체
                      //중복을 피하기 위해서 만들었으나, 알고리즘이 수정될 경우 불필요
{
    public int posnum;
    public Transform pos;

    public movePos(int pos_num, Transform pos_)
    {
        posnum = pos_num;
        pos = pos_;
    }
}
public class Bugmovement : MonoBehaviour
{

    public float movetime = 2.0f; //이 시간 까지 트리거 안에 있을 경우 이동
    public Camera cam;
    GameObject player; //피할 대상
    //전부 public static 인 이유는 bugmove 함수를 외부에서 호출하기 위함
    movePos[] movepos; //숨을수 있는 위치
    NavMeshAgent nav;
    public bool playerInRange = false;
    public Animator anim;

    GameObject[] hide;

    
    int currentpos = 9; //최초 위치는 엉뚱한 곳으로 설정하여 무조건 이동하도록
    // Use this for initialization
    void Awake()
    {
        hide = GameObject.FindGameObjectsWithTag("Hidespot");
        Debug.Log("Hide spot : " + hide.Length);
        movepos = new movePos[hide.Length];
        for (int i = 0; i < hide.Length; i++)
        {
            movepos[i] = new movePos(i, hide[i].transform);
        }

        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //anim.GetComponent<Animator>();
        //Debug.Log(player.tag);
        
        bugmove();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            bugmove();
        }

        if (IsArrived())
        {
            anim.SetBool("IsMove", false);
        }
        //Debug.Log("PlayerInRange : " + playerInRange);
        //Debug.Log(timer);
        //플레이어가 범위 안에 있을 경우 시간을 샘
        /*if (!playerInRange)
            timer += Time.deltaTime;

        //특정 시간이 지났을 경우 이동
        if (timer > movetime)
        {
            //bugmove();
            playerInRange = true;
            timer = 0.0f;
        }
        if (playerInRange)
        {
            RaycastHit hit1;
            // Calculate Ray direction

            Vector3 direction = player.transform.position - transform.position;
            //Debug.Log("Directrion : " + direction);
            if (Physics.Raycast(transform.position, direction, out hit1))
            {
                Debug.Log("Current hit : " + hit1.transform.tag);
                if (hit1.transform.tag == "Player")
                {
                    Debug.Log("is visible in cam");
                    bugmove();

                }
                else
                {
                    Debug.Log("this is behinde the cam");
                }
            }
        }*/
        if (playerInRange)
        {
            nav.speed -= 2;
            bugmove();
        }
            

        if(anim.GetBool("IsDead"))
        {
            BugDead();
        }

    }

    //벌레 이동 함수이다.
    void bugmove()
    {
        playerInRange = false;
        int randpos;
        while (true)
        {
            //임의의 범위를 정해준다.
            randpos = Random.Range(0, movepos.Length);
            if (randpos != currentpos) break;
        }
        Debug.Log("Currrentpos : " + currentpos + " / Randpos : " + randpos);
        currentpos = randpos;
        //nav.enabled = true;
        //다음 목표물을 선택한다.
        nav.SetDestination(movepos[currentpos].pos.position);
        anim.SetBool("IsMove", true);
    }

    //도착 여부를 확인
    bool IsArrived()
    {
        if (!nav.pathPending)
            if (nav.remainingDistance <= nav.stoppingDistance)
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                    return true;

        return false;
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Hand")
        {
            bugmove();
        }
    }
    void BugDead()
    {
        StartCoroutine(DestroyBug());
    }
    IEnumerator DestroyBug()
    {
        yield return new WaitForSeconds(2f);
        GameManager.bugcount--;
        
        Debug.Log("Score : " + GameManager.gamescore);
        Destroy(gameObject);
        
    }
}
