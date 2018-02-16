﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour {

    public Rigidbody rbPlayer;
    public float inputH;
    public float inputV;
    public float maxSpeed;
    public float speed;
    public Vector3 showVelocity;
    public float slideCoef;
    public Vector3 forceApplied;
    public Vector3 forceGravity;
    public float groundDist;
    public Collider colPlayer;
    private float gravity = -75f;
    public float gravScale;
    public bool checkIsGrounded;


    // Use this for initialization
    void Start() {
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {


        groundDist = colPlayer.bounds.extents.y;
    }

    private void FixedUpdate()
    {

        //expose velocity in inspector
        showVelocity = rbPlayer.velocity;

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        

        if (!isGrounded())
        {
            gravScale = 1f;
        }
        else
        {
            gravScale = 0.0f;
        }

        if(inputH > 0)
        {
            var test = " ";
        }

        //calculating input forces
        float xSpeed = inputH * speed;
        float ySpeed = inputV * speed;

        xSpeed = xSpeed <= -.5f ? -.5f : xSpeed;
        xSpeed = xSpeed >= .5f ? .5f : xSpeed;
        ySpeed = ySpeed <= -.5f ? -.5f : ySpeed;
        ySpeed = ySpeed >= .5f ? .5f : ySpeed;

        forceApplied = new Vector3(xSpeed, gravity * gravScale * Time.deltaTime, ySpeed);

        //applying input forces
        rbPlayer.AddForce(forceApplied, ForceMode.VelocityChange);

        //applying gravity
        //forceGravity = new Vector3(0.0f, gravity*gravScale*Time.deltaTime, 0.0f);

        //rbPlayer.AddForce(forceGravity, ForceMode.VelocityChange);

        

    }

    bool isGrounded()
    {
        checkIsGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDist + 0.01f);
        return checkIsGrounded;
    }
}
