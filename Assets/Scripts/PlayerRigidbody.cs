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
    public Vector3 forceGravity;
    public float groundDist;
    public Collider colPlayer;
    private float gravity = -75f;
    public float gravScale;
    public float jumpHeight = 28f;
    public bool isGrounded;


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
        checkIsGrounded();
        //expose velocity in inspector
        showVelocity = rbPlayer.velocity;

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");        

        if (!isGrounded)
        {
            gravScale = 1f;
        }
        else
        {
            gravScale = 0.0f;
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

        forceApplied = new Vector3(xSpeed, gravity * gravScale * Time.deltaTime, ySpeed);

        //Adding Jump
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0")) && isGrounded)
        {            
            forceApplied.y = jumpHeight;
        }
        //applying input forces
        rbPlayer.AddForce(forceApplied, ForceMode.VelocityChange);

        //applying gravity
        //forceGravity = new Vector3(0.0f, gravity*gravScale*Time.deltaTime, 0.0f);

        //rbPlayer.AddForce(forceGravity, ForceMode.VelocityChange);

        

    }

    private void checkIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDist + 0.01f);
    }

    public float pushPower = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {    
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
