﻿using UnityEngine;
using System.Collections;

public class TesterBall : MonoBehaviour {
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
           
        rb.AddForce(movement*10.0f);
    }
}
    