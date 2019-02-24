using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public ParticleSystem muzzleFlash;
    public GameObject bulletSpawnLoc;
    public GameObject bullet;
    public GameObject impact;
    public float range = 10f;

    public PlayerManager networkGun;

    public bool findPlayerManager;

    AudioSource audioSrc;

    float nextTimeToFire = 0f;
    float fireRate = 4;
    public bool droneShoot = false;
	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
        if(findPlayerManager)
        {
            networkGun = GameObject.Find("LocalPlayerManager").GetComponent<PlayerManager>();

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!droneShoot)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
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
            Destroy(imp, 5f);
            imp.transform.parent = hit.collider.gameObject.transform;


            if (hit.collider.gameObject.GetComponent<TargetManager>() != null)
            {
                //hit.collider.gameObject.GetComponent<TargetManager>().networkTargetManager.NetworkHit();
                //.Hit();
                print("Bullet hit");
                networkGun.SendHitMessage(hit.collider.gameObject);
            }
        }

        StartCoroutine(Recoil());

        nextTimeToFire = Time.time + 1.0f / fireRate;

        return true;
    }
    public bool ShootCenter()
    {
        if (Time.time < nextTimeToFire)
        {
            return false;
        }

        audioSrc.Play();
        muzzleFlash.Play();

        GameObject proj = (GameObject)Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation);
        proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * 100000f);
        print(Camera.main.transform.rotation);
        Destroy(proj, 1f);

        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green, 10, false);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            GameObject imp = (GameObject)Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imp, 5f);
            imp.transform.parent = hit.collider.gameObject.transform;


            if (hit.collider.gameObject.GetComponent<TargetManager>() != null)
            {
                //hit.collider.gameObject.GetComponent<TargetManager>().networkTargetManager.NetworkHit();
                //.Hit();
                print("Bullet hit");
                networkGun.SendHitMessage(hit.collider.gameObject);
            }
        }

        StartCoroutine(Recoil());

        nextTimeToFire = Time.time + 1.0f / fireRate;

        return true;
    }
    //cameraTank.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cameraTank.nearClipPlane));
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
