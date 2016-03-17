using UnityEngine;
using System.Collections;


public struct movePos
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

    public float movetime = 2.0f;
    GameObject player;
    public static movePos[] movepos;
    public static NavMeshAgent nav;
    public static bool playerInRange = false;
    float timer;
    public static int currentpos = 9;
    // Use this for initialization
    void Awake()
    {
        movepos = new movePos[]{new movePos(0,GameObject.Find("1").transform),
                                new movePos(1,GameObject.Find("2").transform),
                                new movePos(2,GameObject.Find("3").transform),
                                new movePos(3,GameObject.Find("4").transform),
                                new movePos(4,GameObject.Find("5").transform),
                                new movePos(5,GameObject.Find("6").transform),
                                new movePos(6,GameObject.Find("7").transform)};

        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.tag);
        //bugmove();
    }
    void Update()
    {
        //Debug.Log("PlayerInRange : " + playerInRange);
        //Debug.Log(timer);
        if(playerInRange)
            timer += Time.deltaTime;

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

        if(other.transform.tag == "Player")
            playerInRange = true;

    }
   
    void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        timer = 0.0f;
    }
    public static void bugmove()
    {
        int randpos;
        while (true)
        {
            randpos = Random.Range(0, 5);
            if (randpos != currentpos) break;
        }
        Debug.Log("Currrentpos : " + currentpos + " / Randpos : " + randpos);
        currentpos = randpos;
        //nav.enabled = true;
        nav.SetDestination(movepos[currentpos].pos.position);
        playerInRange = false;
    }
}
