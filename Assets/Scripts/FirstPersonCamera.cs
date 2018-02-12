using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    public float turnSpeed = 50f;
    public float speed = 100f;

    void MouseAiming()
    {
        var rot = new Vector3(0f, 0f, 0f);
        // rotates Camera Left
        if (Input.GetAxis("Mouse X") < 0)
        {
            rot.x -= 1;
        }
        // rotates Camera Left
        if (Input.GetAxis("Mouse X") > 0)
        {
            rot.x += 1;
        }

        // rotates Camera Up
        if (Input.GetAxis("Mouse Y") < 0)
        {
            rot.z -= 1;
        }
        // rotates Camera Down
        if (Input.GetAxis("Mouse Y") > 0)
        {
            rot.z += 1;
        }

        transform.Rotate(rot, turnSpeed * Time.deltaTime);
    }

    void KeyboardMovement()
    {
        //var sensitivity = 0.01f;
        //var movementAmount = 0.5f;
        //var movementVector = new Vector3(0f, 0f, 0f);
        //var hMove = Input.GetAxis("Horizontal");
        //var vMove = Input.GetAxis("Vertical");
        //// left arrow
        //if (hMove < -sensitivity) movementVector.x = -movementAmount;
        //// right arrow
        //if (hMove > sensitivity) movementVector.x = movementAmount;
        //// up arrow
        //if (vMove < -sensitivity) movementVector.z = -movementAmount;
        //// down arrow
        //if (vMove > sensitivity) movementVector.z = movementAmount;
        //// Using Translate allows you to move while taking the current rotation into consideration
        //transform.Translate(movementVector);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }

    void Update()
    {
        MouseAiming();
        KeyboardMovement();
    }
}
