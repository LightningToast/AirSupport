using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {
    public GameObject trackedObject;
    public GameObject controlledObject;

    public string trackedName;
    public GameObject controlledPrefab;

    public string ARTrackedName;
    public GameObject ARControlledPrefab;

    public string[] VRTrackedName;
    public GameObject[] VRControlledPrefab;

    GameController controller;
    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("NetworkManager").GetComponent<GameController>();
        if (isLocalPlayer) {
            if (controller.AR)
            {
                trackedName = ARTrackedName;
                controlledPrefab = ARControlledPrefab;

                trackedObject = GameObject.Find(trackedName);

                CmdSpawnPlayer(true);
            }
            if (controller.VR)
            {
                //trackedName = VRTrackedName;
                //controlledPrefab = VRControlledPrefab;

                trackedObject = GameObject.Find(trackedName);

                CmdSpawnPlayer(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if (controller.AR)
            {
               //controlledObject.transform.position = trackedObject.transform.position;
                //controlledObject.transform.rotation = trackedObject.transform.rotation;
            }
            if (controller.VR)
            {
               //controlledObject.transform.position = trackedObject.transform.position;
                //controlledObject.transform.rotation = trackedObject.transform.rotation;
            }

        }
	}
    [Command]
    void CmdUpdatePosition()
    {

    }
    [Command]
    void CmdSpawnPlayer(bool AR)
    {
        if (AR)
        {
            trackedName = ARTrackedName;
            controlledPrefab = ARControlledPrefab;

            //trackedObject = GameObject.Find(trackedName);

            controlledObject = (GameObject)Instantiate(controlledPrefab, transform.position, transform.rotation);
            NetworkServer.SpawnWithClientAuthority(controlledObject, base.connectionToClient);

            controlledObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            //trackedName = VRTrackedName;
            //controlledPrefab = VRControlledPrefab;

            //trackedObject = GameObject.Find(trackedName);

            for (int count = 0; count < VRControlledPrefab.Length; count++)
            {
                controlledObject = (GameObject)Instantiate(VRControlledPrefab[count], transform.position, transform.rotation);
                NetworkServer.SpawnWithClientAuthority(controlledObject, base.connectionToClient);


            }

            //controlledObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
