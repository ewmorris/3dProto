using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelController : MonoBehaviour {


    public GameObject Parent;
    public Transform Parent_Tr;
    public Transform Child_Tr;
    public float speed;
    public float groundDist;
    public float groundDiff;

    // Use this for initialization
    void Start()
    {
       
    }

    void Update()
    {

        groundDist = Parent.GetComponent<PlayerRigidbody>().groundDist;

        Vector3 newPos = new Vector3(Parent_Tr.position.x, Parent_Tr.position.y - (groundDist - groundDiff), Parent_Tr.position.z);
        Child_Tr.position = newPos;

        Vector3 turnToFace = new Vector3(Parent.GetComponent<Rigidbody>().velocity.x, 0, Parent.GetComponent<Rigidbody>().velocity.z);

        transform.forward = Vector3.Lerp(transform.forward, turnToFace, Time.deltaTime * speed);
    }
}
