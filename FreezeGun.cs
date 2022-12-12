using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FreezeGun : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float timeToDisappear = 5f;
    public Rigidbody bullet;
    public Rigidbody bullet2;
    public Transform barrel;
    public GameObject flash;
    public float flashToDissappear = 1.0f;
    public float ftimeToDisappear = 0.01f;
    public bool isShooting;
    public AudioSource audioSource1;

    public AudioClip clip1;

    public float canShoot = 3.0f;
    public float timesincelastShot = 0.0f;

    public float chargeTime = 3.0f;
    public float charge = 0.0f;

    public Camera fpsCam;
    public float range = 100f;

    public NavMeshAgent agent;

    public bool hasShot;


    public int maxAmmo = 10;
    public int currentAmmo;



    // Start is called before the first frame update
    void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
        audioSource1.clip = clip1;

    }

    // Update is called once per frame
    void Update()
    {
        Fire();

        TimerIncrease();

        GunShoot();

        Cooldown();

        FreezeCool();

        if (Input.GetButton("Fire2") || (Input.GetButtonDown("Fire1")))
        {
            hasShot = true;
            isShooting = true;


        }
        else
        {
            isShooting = false;
        }

    }


    public void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            if (timesincelastShot >= 0.1f)
            {
                return;
            }
            audioSource1.PlayOneShot(clip1);
            Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, barrel.position, transform.rotation);
            bulletClone.AddForce(transform.forward * bulletSpeed);
            StartCoroutine(DisappearCoroutine(bulletClone.gameObject));
            GameObject flashClone = (GameObject)Instantiate(flash, barrel.position, transform.rotation);
            StartCoroutine(DisappearflashCoroutine(flashClone.gameObject));
        }
    }

    public void Cooldown()
    {
        if (hasShot == true)
        {
            timesincelastShot += Time.deltaTime;
        }
        if (timesincelastShot >= chargeTime)
        {
            hasShot = false;
            timesincelastShot = 0;
        }
    }

    public void TimerIncrease()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            charge += Time.deltaTime;
        }
    }

    void GunShoot()
    {
        if (charge >= chargeTime)
        {

            if (currentAmmo <= 0)
            {
                return;
            }
            audioSource1.PlayOneShot(clip1);
            currentAmmo--;
            Rigidbody bulletClone = (Rigidbody)Instantiate(bullet2, barrel.position, transform.rotation);
            bulletClone.AddForce(transform.forward * bulletSpeed);
            charge = 0;
            StartCoroutine(DisappearCoroutine(bulletClone.gameObject));
            GameObject flashClone = (GameObject)Instantiate(flash, barrel.position, transform.rotation);
            StartCoroutine(DisappearflashCoroutine(flashClone.gameObject));
        }

    }

    void FreezeCool()
    {
        if (timesincelastShot >= canShoot)
        {
            isShooting = false;
            timesincelastShot = 0.0f;
        }
    }

    private IEnumerator DisappearCoroutine(GameObject bulletToDisappear)
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(bulletToDisappear);
    }

    private IEnumerator DisappearflashCoroutine(GameObject flashoDissappear)
    {
        yield return new WaitForSeconds(ftimeToDisappear);
        Destroy(flashoDissappear);
    }
}