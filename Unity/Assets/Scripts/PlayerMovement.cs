using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;
	Rigidbody rb;
	
	void Awake()
	{
		rb = GetComponent <Rigidbody> ();
	}
	
	void FixedUpdate() {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		Move(h, v);
	
	}
	
	void Move (float h, float v)
	{
		Vector3 movement = new Vector3 (h, 0f, v);
		//Vector3 movement = new Vector3 (0f, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		
		rb.MovePosition (transform.position + movement);
	}
	
}
