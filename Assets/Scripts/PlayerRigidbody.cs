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

        forceApplied = new Vector3(xSpeed, forceApplied.y = gravity * gravityScale * Time.deltaTime, ySpeed);

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

        //if(isJumping && gravityScale <= minGravityScale)
        //{
        //   gravityScale += .001f;
        //}

        //Decreasing jump height if nor holding button
        //Max jump height of jumpHeight
        //min of 0.5 * jumpHeight
        //if (Input.GetButtonUp("Jump") && !isGrounded && isLerping)
        //{
        //    var killJump = -1 * rbPlayer.velocity.y;
        //    forceApplied.y = killJump;
        //}




        forceApplied.y = forceApplied.y + gravity * gravityScale;

        //applying input forces
        rbPlayer.AddForce(forceApplied, ForceMode.VelocityChange);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var test = hit;
    }

    //public float pushPower = 2.0F;

    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{    
    //    Rigidbody body = hit.collider.attachedRigidbody;
    //    if (body == null || body.isKinematic)
    //        return;

    //    if (hit.moveDirection.y < -0.3F)
    //        return;

    //    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    //    body.velocity = pushDir * pushPower;
    //}
}
