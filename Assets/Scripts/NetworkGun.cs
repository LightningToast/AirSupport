using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGun : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendHitMessage(GameObject target)
    {
        if (hasAuthority)
        {
            target.GetComponent<TargetManager>().networkTargetManager.CmdHit();
        }
    }
}
