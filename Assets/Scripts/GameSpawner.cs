using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSpawner : NetworkBehaviour {
    public GameObject targetPrefab;
    public Vector3 targetPosition;
    // Use this for initialization
    void Start () {
        SpawnTarget();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnTarget()
    {
        GameObject target = (GameObject)Instantiate(targetPrefab, targetPosition, transform.rotation);
        NetworkServer.Spawn(target);
    }
}
