using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public float range = 10f;

    float nextTimeToFire = 0f;
    float fireRate = 4;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if(Time.time < nextTimeToFire)
        {
            return;
        }
        muzzleFlash.Play();


        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            GameObject imp = (GameObject) Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imp, 0.2f);
        }

        StartCoroutine(Recoil());

        nextTimeToFire = Time.time + 1.0f / fireRate;
    }

    IEnumerator Recoil()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos;
        endPos.z -= 0.01f;
        transform.localPosition = endPos;

        transform.Rotate(Vector3.right * -3f);

        yield return new WaitForSeconds(0.05f);


        transform.localPosition = startPos;
        transform.Rotate(Vector3.right * 3f);
    }
}
