using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;
	Rigidbody rb;
	NavMeshAgent nav;
	Transform target;

	void Awake()
	{
		rb = GetComponent <Rigidbody> ();
		nav = GetComponent <NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Target").transform;
	}
	
	void FixedUpdate() {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		Move(h, v);

		if(IsObstacle ())
			Debug.Log ("Target is invisible");
		else
			Debug.Log ("Target is visible");


	}

	// Player movement controll
	void Move (float h, float v)
	{
		Vector3 movement = new Vector3 (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;
		
		rb.MovePosition (transform.position + movement);
	}

	// if there is a obstacle between player and target, return true
	// else return false
	bool IsObstacle()
	{
		bool isOb = true;

		// set nav
		nav.SetDestination (target.position);

		// test for obstacle between target and player
		isOb = nav.remainingDistance > Vector3.Distance (this.transform.position, target.position) + 0.1;

		// stop nav because Player's nav is just for finding path.
		nav.Stop ();

		return isOb;
	}
}
