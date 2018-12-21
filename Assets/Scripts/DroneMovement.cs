using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DroneMovement : NetworkBehaviour {
    public GameObject trackObj;
    public float moveSpeed = 0.1f;
    public float rotationSpeed = 0.1f;

    GameController controller;
    // Use this for initialization
    void Start () {
        controller = GameObject.Find("NetworkManager").GetComponent<GameController>();
        trackObj = GameObject.Find("ARCamera");
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.y = trackObj.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, rot, rotationSpeed));
            transform.position = Vector3.Lerp(transform.position, trackObj.transform.position, moveSpeed);
        }
        //transform.position = trackObj.transform.position;
	}
}
