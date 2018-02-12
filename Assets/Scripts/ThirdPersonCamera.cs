﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offsetBegin;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offsetBegin = offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        float moveMouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if(Input.GetAxis("Fire3") == 1)
        {
            offset = offsetBegin;
        }

        //offset.z += moveMouseWheel;
        if(offset.z + moveMouseWheel < offsetBegin.z + 1 && offset.z + moveMouseWheel > offsetBegin.z - 5)
            offset.z += moveMouseWheel;

        transform.position = player.transform.position + offset;       
        
    }

    private void FixedUpdate()
    {
        

    }

}
