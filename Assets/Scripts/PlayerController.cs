using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Global Variables
    public float speed;
    public float jumpHeight;
    public CharacterController contPlayer;
    private Vector3 moveDir;
    public float gravityScale;
    public float maxGrav;
    public float minGrav;
    private float moveHor;
    private float moveVert;
    public float moveFriction;

	// Use this for initialization
	void Start () {
        contPlayer = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Movement

        moveDir = new Vector3(Input.GetAxis("Horizontal")*speed, moveDir.y, Input.GetAxis("Vertical")*speed);
        


        //Jump
        if(Input.GetButtonDown("Jump") && contPlayer.isGrounded)
        {
            moveDir.y = jumpHeight;
        }

        //Decreasing jump height if nor holding button
        //Max jump height of jumpHeight
        //min of 0.5 * jumpHeight
        if(Input.GetButtonUp("Jump") && !contPlayer.isGrounded && moveDir.y>jumpHeight/2)
        {
            moveDir.y = 0f;
        }
        //Adjusting gravity for ascent and descent

        //Descent gravity
        if(moveDir.y<0 && gravityScale != minGrav )
        {
            gravityScale = minGrav;
        }

        //Ascent gravity
        if(contPlayer.isGrounded && gravityScale != maxGrav)
        {
            gravityScale = maxGrav;
        }
        
        //apply jump
        moveDir.y = moveDir.y + Physics.gravity.y * gravityScale;

        
        contPlayer.Move(moveDir*Time.deltaTime);

        if (contPlayer.isGrounded)
        {
            moveDir.y = 0f;
        }

        //rotate based on camera
    }

    private void FixedUpdate()
    {
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Try this
        if(Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0"))
        {
            movement.y = jumpHeight;
        }
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
        */


    }

    //Stolen code from Unity documentation to add physics collision to Character Controller
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
