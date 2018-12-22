using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {
    NetworkManager nManager;
    NetworkDiscovery dManager;
    public bool AR;
    public bool VR;
	// Use this for initialization
	void Start () {
        nManager = GetComponent<NetworkManager>();
        dManager = GetComponent<NetworkDiscovery>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("p"))
        {
            nManager.StartHost();
        }
        if (Input.GetKeyDown("i"))
        {
            dManager.StartAsServer();
        }

    }
}
