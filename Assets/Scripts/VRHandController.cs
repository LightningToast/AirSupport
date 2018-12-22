using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRHandController : MonoBehaviour {
    public SteamVR_Action_Boolean triggerAction;
    public SteamVR_Action_Vibration hapticFeedback;

    public SteamVR_Input_Sources hand;

    public GunController gun;

	// Use this for initialization
	void Start () {
        
 
	}
	
	// Update is called once per frame
	void Update () {
		if(triggerAction.GetState(hand))
        {
            
            
            if(gun.Shoot())
            {
                print("Shoot");
                hapticFeedback.Execute(0f, 10f, 1f, 0.1f, hand);
            }

        }
	}
}
