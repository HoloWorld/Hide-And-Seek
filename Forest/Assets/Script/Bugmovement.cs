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
    GameObject player; //피할 대상
    //전부 public static 인 이유는 bugmove 함수를 외부에서 호출하기 위함
    public static movePos[] movepos; //숨을수 있는 위치
    public static NavMeshAgent nav;
    public static bool playerInRange = false;
    GameObject[] hide;

    float timer;
    public static int currentpos = 9; //최초 위치는 엉뚱한 곳으로 설정하여 무조건 이동하도록
    // Use this for initialization
    void Awake()
    {
        hide = GameObject.FindGameObjectsWithTag("Hidespot");
        Debug.Log("Hide spot : " + hide.Length);
        movepos = new movePos[hide.Length];
        for (int i=0; i< hide.Length; i++)
        {
            movepos[i] = new movePos(i,hide[i].transform);
        }
       
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.tag);
        //bugmove();
    }
    void Update()
    {
        //Debug.Log("PlayerInRange : " + playerInRange);
        //Debug.Log(timer);
        //플레이어가 범위 안에 있을 경우 시간을 샘
        if(playerInRange)
            timer += Time.deltaTime;

        //특정 시간이 지났을 경우 이동
        if (timer >= movetime)
        {
            timer = 0.0f;
            bugmove();
        }
    }
    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        Debug.Log("PlayerInRange : "+ playerInRange);
        //사용자 범위에 계속 존재할 경우
        if(other.transform.tag == "Player")
            playerInRange = true;

    }
   
    void OnTriggerExit(Collider other)
    {
        //사용자 범위에 벗어낫을 경우 false
        playerInRange = false;
        timer = 0.0f;
    }
    //벌레 이동 함수이다.
    public static void bugmove()
    {
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
        playerInRange = false;
    }
}
