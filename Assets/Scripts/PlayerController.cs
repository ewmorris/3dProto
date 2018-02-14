using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Global Variables
    public float speed;
    public float jumpHeight;
    //public Rigidbody rbPlayer;
    public CharacterController contPlayer;
    private Vector3 moveDir;
    public float gravityScale;

	// Use this for initialization
	void Start () {
        contPlayer = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        moveDir = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, Input.GetAxis("Vertical")*speed);

        if(Input.GetButtonDown("Jump"))
        {
            moveDir.y = jumpHeight;
        }

        moveDir.y = moveDir.y + Physics.gravity.y * gravityScale;
        contPlayer.Move(moveDir*Time.deltaTime);

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
