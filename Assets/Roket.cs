using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour {
    /*the code must work in every frame */

    Rigidbody rigidBody;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>(); //find rigidBody
    }
	
	// Update is called once per frame
    /* Update is used for data procesing */
	void Update () {
        //print("Update");
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) //the ship can thrust while rotating
        {
            // print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up);   //adding a force that is relative to its coordonate system
        }

        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotating left"); 
            transform.Rotate(Vector3.forward); //rotate around z=forward
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //print("Rotating right");
            transform.Rotate(-Vector3.forward);
        }
    }
}
