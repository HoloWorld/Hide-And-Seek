using UnityEngine;
using System.Collections;
using System.Threading;
public class EscapeController : MonoBehaviour {
 	
	GameObject[] hide;
	GameObject selectedObj;
	Transform player;
    NavMeshAgent nav;
	Rigidbody rb;
   
    void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		hide = GameObject.FindGameObjectsWithTag ("HideFromFront");
		
		// Select one of objects
		selectedObj = hide[Random.Range(0, hide.Length)];
		
        nav = GetComponent <NavMeshAgent> ();
		rb = GetComponent <Rigidbody> ();
	}
	
	void Update() {
		// Recognize player's position
		if (player.position.z < 0)
			hide = GameObject.FindGameObjectsWithTag ("HideFromFront");
		else if (player.position.x < 0)
			hide = GameObject.FindGameObjectsWithTag ("HideFromLeft");
		else if (player.position.x > 20)
			hide = GameObject.FindGameObjectsWithTag ("HideFromRight");

		
		// Set destination
		nav.SetDestination (selectedObj.transform.position);
		
		if (!nav.pathPending)
 		{
     		if (nav.remainingDistance <= nav.stoppingDistance)
     		{
         		if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
         		{
             		nav.Stop();
					Thread.Sleep(100);
					selectedObj = hide[Random.Range(0, hide.Length)];
					nav.Resume();
         		}
     		}
		
		}
	}
}
