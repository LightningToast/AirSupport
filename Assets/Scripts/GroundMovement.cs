using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroundMovement : NetworkBehaviour {
    public GameObject trackedObject;
    public string trackedObjectName = "[CameraRig]/Camera";
    public float moveSpeed = 0.01f;
	// Use this for initialization
	void Start () {

        trackedObject = GameObject.Find(trackedObjectName);

    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {
            transform.position = trackedObject.transform.position;
            transform.rotation = trackedObject.transform.rotation;
            /*Vector3 pos = transform.position;
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
            transform.position = pos;*/
        }
    }
}
