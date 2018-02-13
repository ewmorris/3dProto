using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Global Variables
    public float speed;
    public float jumpHeight = 170f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(Input.GetKeyDown("space"))
        {
            movement.y = jumpHeight;
        }
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }
}
