using UnityEngine;
using System.Collections;
using System.Threading;


public class EscapeController : MonoBehaviour {
 	
	GameObject[] hide;
	GameObject selectedObj;
	Transform player;
    NavMeshAgent nav;
   
    void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		hide = GameObject.FindGameObjectsWithTag ("HideFromFront");
		
		// Select one of objects
		selectedObj = hide[Random.Range(0, hide.Length)];
		
        nav = GetComponent <NavMeshAgent> ();
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

		// Is it arrived to destination?
		if (IsArrived ()) {
			StartCoroutine (StopEscape ());
			selectedObj = hide [Random.Range (0, hide.Length)];
		} else {	// On the way to destination

			// Escape player or keep going
			// 1) Escape player
			// Set new destination most far from player
			// 2) Keep going
			// Just go to first destination and sleep for a while

		}
	}

	bool IsArrived() {
		if (!nav.pathPending)
			if (nav.remainingDistance <= nav.stoppingDistance)
				if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
					return true;
				
		return false;
	}
		
	// Stop escaping for a while
	IEnumerator StopEscape() {
		nav.Stop();

		yield return new WaitForSeconds(3);

		nav.Resume();
	}
}
