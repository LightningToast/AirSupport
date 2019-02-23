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

    public string[] VRGunNames;

    public GunController gunController;

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

                foreach(string VRGunName in VRGunNames) {
                    GameObject.Find(VRGunName).GetComponent<GunController>().networkGun = this;
                    gunController = GameObject.Find(VRGunName).GetComponent<GunController>();
                }
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
    public void SendHitMessage(GameObject target)
    {

        CmdSendHitMessage(target.name);
    }
    [Command]
    public void CmdSendHitMessage(string targetName)
    {
        GameObject target = GameObject.Find(targetName);

        target.GetComponent<TargetManager>().networkTargetManager.networkIdentity.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        target.GetComponent<TargetManager>().networkTargetManager.NetworkHit();
        target.GetComponent<TargetManager>().networkTargetManager.networkIdentity.RemoveClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
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
