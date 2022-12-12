using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor;
    public Vector3 movementVector;
    [SerializeField] float period = 2f;
    public Rigidbody enemyrb;
    public GameObject enemy;
    public Animator animator;

    public bool isFrozen;
    public bool isFrozen2;

    public float freezeTimer = 5f;
    public float unFreeze = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<GameObject>();
        enemyrb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isFrozen)
        {
            freezeTimer += Time.deltaTime;
        }

        if (unFreeze <= freezeTimer && isFrozen)
        {
            period = 2f;
            isFrozen = false;
            enemyrb.isKinematic = false;
            freezeTimer = 0.0f;
        }

        Vector3 dirToPlayer = transform.position;

        if (period <= Mathf.Epsilon) { return; } //to prevent NaN error from happening when period equals 0. 
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1.

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner. Easy to read as well

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

        if (isFrozen == false)
        {
            animator.SetBool("isMoving", true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!isFrozen && other.gameObject.tag == "bullet")
        {
            enemyrb.isKinematic = true;
            period = 0f;
            isFrozen = true;
            if (isFrozen == true)
            {
                animator.SetBool("isMoving", false);
            }

        }

        if(!isFrozen && other.gameObject.tag == "bullet2")
        {
            isFrozen2 = true;
            enemyrb.isKinematic = true;
            period = 0f;
            if(isFrozen2 == true)
            {
                animator.SetBool("isMoving", false);
            }
        }


    }
}
