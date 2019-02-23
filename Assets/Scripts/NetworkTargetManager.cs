using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTargetManager : NetworkBehaviour {
    public TargetManager target;
    public NetworkIdentity networkIdentity;
	// Use this for initialization
	void Start () {
        target.networkTargetManager = this;
        networkIdentity = GetComponent<NetworkIdentity>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NetworkHit()
    {
        CmdHit();
    }

    [Command]
    public void CmdHit()
    {
        RpcHit();
    }
    [Command]
    public void CmdReset()
    {
        RpcReset();
    }

    [ClientRpc]
    void RpcReset()
    {
        target.Reset();
    }
    [ClientRpc]
    void RpcHit()
    {
        target.Hit();
    }
}
