using System.Collections;
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
    public Vector3 jumpApplied;
    public Vector3 forceGravity;
    public float groundDist;
    public Collider colPlayer;
    public float gravity = -9.81f;
    public float gravityScale;
    public float jumpHeight = 28f;
    public bool isGrounded;
    public float minGravityScale = .25f;
    public float maxGravityScale = .5f;

    public bool isJumping;


    public Quaternion cameraRotation;
    public Vector3 cameraPosition;

    // Use this for initialization
    void Start() {
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
        groundDist = colPlayer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate()
    {
        
        checkIsGrounded();
        //expose velocity in inspector
        showVelocity = rbPlayer.velocity;
        cameraRotation = Camera.main.transform.rotation;
        cameraPosition = Camera.main.transform.position;

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        if(!isGrounded && gravityScale == 0)
        {
            gravityScale = maxGravityScale;
        }

        if(!isJumping && !isGrounded && gravityScale < minGravityScale)
        {
            gravityScale = minGravityScale;
        }

        if(!isJumping && isGrounded && gravityScale > 0)
        {
            gravityScale = 0;
        }

        //calculating input forces
        float xSpeed = inputH * speed;
        float ySpeed = inputV * speed;

        //Capping diagnoal move speed
        if(xSpeed + ySpeed > speed)
        {
            var overPercent = 1 - (speed / (xSpeed + ySpeed));
            xSpeed *= overPercent;
            ySpeed *= overPercent;
        }


        //Quaternion newRotation = new Quaternion(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, 1f);

        Vector3 lookRotation = new Vector3(transform.position.x - Camera.main.transform.position.x, 0f, transform.position.z - Camera.main.transform.position.z);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        forceApplied = new Vector3(xSpeed, gravity * gravityScale * Time.deltaTime, ySpeed);



        //Adding Jump
        if ((Input.GetButtonDown("Jump")) && isGrounded )
        {
            //StartLerping();
            jumpApplied = new Vector3(0.0f, jumpHeight, 0.0f);
            rbPlayer.AddForce(jumpApplied, ForceMode.Impulse);

            ////Ascent gravity
        //    gravityScale = 0;
        //    isGrounded = false; 
        //    isJumping = true;
        }

        forceApplied.y = forceApplied.y + gravity * gravityScale;

        //applying input forces relative to direction
        rbPlayer.AddRelativeForce(forceApplied, ForceMode.VelocityChange);
    }

    private void checkIsGrounded()
    {
        var position = transform.position;

        

        var raycast01 = Physics.Raycast(position, Vector3.down, groundDist + 0.01f);
        position.x -= .13f;
        var raycast02 = Physics.Raycast(position, Vector3.down, groundDist + 0.01f);
        position.x += .26f;
        var raycast03 = Physics.Raycast(position, Vector3.down, groundDist + 0.01f);
        position.z -= .13f;
        var raycast04 = Physics.Raycast(position, Vector3.down, groundDist + 0.01f);
        position.z += .26f;
        var raycast05 = Physics.Raycast(position, Vector3.down, groundDist + 0.01f);

        isGrounded = (raycast01 || raycast02 || raycast03 || raycast04 || raycast05);

        if(isGrounded)
        {
            isJumping = false;
        }
    }
}
