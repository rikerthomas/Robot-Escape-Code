using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform barrel;

    public float bulletSpeed = 1000f;


    public float timeToDisappear = 5f;

    public bool canShootAtPlayer;

    public int hits;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (hits >= 6)
        {
            StopCoroutine(FireGun());
        }
    }

    void Shoot()
    {
        StartCoroutine (FireGun());
    }

    private IEnumerator FireGun()
    {
        yield return new WaitForSeconds(3.0f);
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, barrel.position, transform.rotation);
        bulletClone.AddForce(transform.forward * bulletSpeed);
        StartCoroutine(DisappearCoroutine(bulletClone.gameObject));

    }

    private IEnumerator DisappearCoroutine(GameObject bulletToDisappear)
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(bulletToDisappear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            hits++;
        }
    }


}


