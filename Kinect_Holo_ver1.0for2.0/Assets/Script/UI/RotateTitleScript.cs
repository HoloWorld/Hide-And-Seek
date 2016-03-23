//used to rotate the title on the titleMenu Scene
//this script is not that important

using UnityEngine;
using System.Collections;

public class RotateTitleScript : MonoBehaviour {

	public Vector3 MaxRotation;
	private  Vector3 Rotation; 
	
	void Update () 
	{
		Rotation.x = Mathf.Sin (Time.time) * MaxRotation.x;
		
		Rotation.y = Mathf.Sin (Time.time) * MaxRotation.y + 90.0f;

		Rotation.z = Mathf.Sin (Time.time) * MaxRotation.z - 30.0f;


		gameObject.transform.rotation = Quaternion.Euler(Rotation);
	}
}
