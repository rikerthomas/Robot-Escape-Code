using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingAudio : MonoBehaviour
{

    public AudioSource audioSource1;
    public AudioClip clip1;

    public float chargeTime = 3.0f;
    public float charge = 0.0f;

    public int maxAmmo = 10;
    public int currentAmmo;

    public Transform barrel;
    public GameObject flash;
    public float ftimeToDisappear = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource1 = GetComponent<AudioSource>();
        audioSource1.clip = clip1;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentAmmo <= 0)
        {
            return;
        }
        TimerIncrease();
        if (Input.GetMouseButtonUp(1))
        {
            StopAllCoroutines();
            audioSource1.Pause();
        }
        if(charge >= chargeTime)
        {
            charge = 0;
            audioSource1.Stop();
            currentAmmo--;
        }
    }

    public void TimerIncrease()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            charge += Time.deltaTime;
            if (charge < chargeTime && !audioSource1.isPlaying)
            {
                GameObject flashClone = (GameObject)Instantiate(flash, barrel.position, transform.rotation);
                StartCoroutine(DisappearflashCoroutine(flashClone.gameObject));
                audioSource1.Play();
            }

        }
    }

    private IEnumerator DisappearflashCoroutine(GameObject flashoDissappear)
    {
        yield return new WaitForSeconds(ftimeToDisappear);
        Destroy(flashoDissappear);
    }
}
