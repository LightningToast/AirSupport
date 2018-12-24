using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public ParticleSystem muzzleFlash;
    public GameObject bulletSpawnLoc;
    public GameObject bullet;
    public GameObject impact;
    public float range = 10f;

    AudioSource audioSrc;

    float nextTimeToFire = 0f;
    float fireRate = 4;
	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public bool Shoot()
    {
        if(Time.time < nextTimeToFire)
        {
            return false;
        }

        audioSrc.Play();
        muzzleFlash.Play();

        GameObject proj = (GameObject)Instantiate(bullet, bulletSpawnLoc.transform.position, transform.rotation);
        proj.GetComponent<Rigidbody>().AddForce(bulletSpawnLoc.transform.forward * 100000f);
        Destroy(proj, 1f);

        RaycastHit hit;
        if(Physics.Raycast(bulletSpawnLoc.transform.position, transform.forward, out hit, range))
        {
            GameObject imp = (GameObject) Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imp, 0.2f);



            if (hit.collider.gameObject.GetComponent<TargetManager>() != null)
            {
                hit.collider.gameObject.GetComponent<TargetManager>().Hit();
            }
        }

        StartCoroutine(Recoil());

        nextTimeToFire = Time.time + 1.0f / fireRate;

        return true;
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
