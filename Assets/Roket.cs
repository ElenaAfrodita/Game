using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour {
    /*the code must work in every frame */

    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 250f; // available in Inspector with this default value
    [SerializeField] float mainThrust = 50f;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>(); //find rigidBody
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    /* Update is used for data procesing */
    void Update() {
        //print("Update");
        Rotation();
        Thrust();
    }

    private void Rotate()
    {
        throw new NotImplementedException();
    }

    void OnCollisionEnter(Collision collision)
    {
        // print("Collison");
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //OK
                print("OK");
                break;
            default:
                print("Dead"); //player killed
                break;
        }
    }

        private void Rotation()
        {
            rigidBody.freezeRotation = true; //when the player takes control of rotation
            float rotationSpeed = rcsThrust * Time.deltaTime; //rotation speed
            if (Input.GetKey(KeyCode.A))
            {
                //print("Rotating left"); 
                transform.Rotate(Vector3.forward * rotationSpeed); //rotate around z=forward
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //print("Rotating right");
                transform.Rotate(-Vector3.forward * rotationSpeed);
            }
            rigidBody.freezeRotation = false; //resume to the physics control of rotation
        }

        private void Thrust()
        {
            if (Input.GetKey(KeyCode.Space)) //the ship can thrust while rotating
            {
                // print("Thrusting");
                rigidBody.AddRelativeForce(Vector3.up * mainThrust);   //adding a force that is relative to its coordonate system
                if (!audioSource.isPlaying)
                {   //so it does not layer
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
    }