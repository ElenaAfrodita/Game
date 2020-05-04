using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Roket : MonoBehaviour {
    /*the code must work in every frame */

    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f; // available in Inspector with this default value
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip engine;  // reference to the audio clip of the engine
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    enum State { Alive, Dying, Transcending} //states of the roket
    State state = State.Alive;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>(); //find rigidBody
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    /* Update is used for data procesing */
    void Update() {
        //print("Update");
        if(state == State.Alive)
        {
            RespondToRotationInput();
            RespondToThrustInput();
        }
    }

    private void Rotate()
    {
        throw new NotImplementedException();
    }

    void OnCollisionEnter(Collision collision)
    {
        //print("Collison");

        if (state != State.Alive){ return; } //ignore collison when dead 
        switch (collision.gameObject.tag)   // get the tag string from the game object collied with
        {
            case "Friendly":
                break;
            case "Finish":
                FinishGame();
                break;
            default:
                //print("Dead");
                Death();
                break;
        }
    }

    private void FinishGame()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", 1f);  //Load the new scene with delay 
    }

    private void Death()
    {
        state = State.Dying;
        audioSource.Stop();  //stop thrust audio
        audioSource.PlayOneShot(death);
        //  SceneManager.LoadScene(0);
        Invoke("LoadFirstLevel", 1f);
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); 
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToRotationInput()
    {
            rigidBody.freezeRotation = true;    //when the player takes control of rotation
            float rotationSpeed = rcsThrust * Time.deltaTime;   //rotation speed
            if (Input.GetKey(KeyCode.A))
            {
                //print("Rotating left"); 

                transform.Rotate(Vector3.forward * rotationSpeed);  //rotate around z=forward
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //print("Rotating right");

                transform.Rotate(-Vector3.forward * rotationSpeed);
            }
            rigidBody.freezeRotation = false;   //resume to the physics control of rotation
     }

        private void RespondToThrustInput()
        {
            if (Input.GetKey(KeyCode.Space))    //the ship can thrust while rotating
        {
            ApplyThrust();
        }
        else
            {
                audioSource.Stop();
            }
        }

    private void ApplyThrust()
    {
        // print("Thrusting");

        rigidBody.AddRelativeForce(Vector3.up * mainThrust);   //adding a force that is relative to its coordonate system
        if (!audioSource.isPlaying)
        {   //so it does not layer
            audioSource.PlayOneShot(engine); //allows us to use more audio clips
        }
    }
}