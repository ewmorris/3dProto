using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour {

    public Rigidbody rbPlayer;
    public Collider colPlayer;

    //jump variables
    public float gravForce; //-9.81
    public float gravScale; //1
    public float minGravScale; //0.5 unusued
    public float maxGravScale; //1 unused
    public Vector3 gravApplied;

    public float jumpForce; //10
    public Vector3 jumpApplied;

    public float groundDist;
    public bool isGrounded;

    //input variables
    public float inputH;
    public float inputV;

    public float maxSpeed;
    public float speed;

    public Vector3 forceApplied = new Vector3();

    // Use this for initialization
    void Start() {
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
        groundDist = colPlayer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update() {
        CheckIsGrounded();
        CameraRotation();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        Jump();
        InputMovement();
        ApplyInputMovement();
        
    }
    
    private void InputMovement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
    }

    private void ApplyInputMovement()
    {
        float xSpeed = inputH * speed;
        float ySpeed = inputV * speed;

        if (xSpeed + ySpeed > speed)
        {
            var overPercent = 1 - (speed / (xSpeed + ySpeed));
            xSpeed *= overPercent;
            ySpeed *= overPercent;
        }


        forceApplied = new Vector3(xSpeed, 0, ySpeed);

        rbPlayer.AddRelativeForce(forceApplied, ForceMode.VelocityChange);
    }

    private void CameraRotation()
    {
        Vector3 lookRotation = new Vector3(transform.position.x - Camera.main.transform.position.x, 0f, transform.position.z - Camera.main.transform.position.z);
        transform.rotation = Quaternion.LookRotation(lookRotation);
    }

    private void ApplyGravity()
    {
        gravScale = (rbPlayer.velocity.y < 0)? maxGravScale : minGravScale;
        gravScale = (isGrounded) ? 0.0f : gravScale;
        gravApplied = new Vector3(0.0f, gravForce * gravScale, 0.0f);
        rbPlayer.AddForce(gravApplied, ForceMode.Acceleration);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpApplied = new Vector3(0.0f, jumpForce, 0.0f);
            rbPlayer.AddForce(jumpApplied, ForceMode.Impulse);
        }
    }

    private void CheckIsGrounded()
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
        
    }
}
