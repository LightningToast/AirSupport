using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroundMovement : NetworkBehaviour {
    public GameObject trackedObject;
    public float moveSpeed = 0.01f;
	// Use this for initialization
	void Start () {
        trackedObject = GameObject.Find("VRPlayer");
	}
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {
            Vector3 pos = transform.position;
            if (Input.GetKey("w"))
            {
                pos.z += moveSpeed;
            }
            if (Input.GetKey("s"))
            {
                pos.z -= moveSpeed;
            }
            if (Input.GetKey("a"))
            {
                pos.x += moveSpeed;
            }
            if (Input.GetKey("d"))
            {
                pos.x -= moveSpeed;
            }
            transform.position = pos;
        }
    }
}
