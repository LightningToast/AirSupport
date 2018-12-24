using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    public int direction = 1;
    public float speed = 50f;
    public float targetRot = 0;

    public float resetTime = 3f;

    public bool hitBool = false;
    public bool resetBool = false;

    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(hitBool)
        {
            Hit();
            hitBool = false;
        }
        if (resetBool)
        {
            Reset();
            resetBool = false;
        }

        print(Mathf.Abs(transform.rotation.eulerAngles.x - targetRot));
        if(Mathf.Abs(transform.rotation.eulerAngles.x - targetRot) > 2f) {
            transform.Rotate(direction * Vector3.right * Time.deltaTime * speed);
        }

    }

    public void Hit()
    {
        audio.Play();
        direction = 1;
        targetRot = 90f;

        StartCoroutine(ResetTimer());
    }

    public void Reset()
    {
        direction = -1;
        targetRot = 1f;
    }

    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(resetTime);
        Reset();
    }
}
