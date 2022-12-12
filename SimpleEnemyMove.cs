using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float speed = 1.0f;
    public GameObject bullet;
    public GameObject bullet2;

    public Rigidbody enemy;

    public Camera fpsCam;
    public float range = 100f;

    public bool isFrozen;
    public bool isFrozen2;

    public float freezeTimer = 0.0f;
    public float unFreeze = 10f;

    public GameObject Player;
    public float enemyDistance = 4.0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(isFrozen)
        {
            freezeTimer += Time.deltaTime;
        }

        if (unFreeze <= freezeTimer && isFrozen)
        {
            enemy.isKinematic = false;
            isFrozen = false;
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
            freezeTimer = 0.0f;
            

        }

        if (isFrozen == true)
        {
            animator.SetBool("isMoving", false);
        }
        if (isFrozen == false)
        {
            animator.SetBool("isMoving", true);
        }
        if(isFrozen2 == true)
        {
            animator.SetBool("isMoving", false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if the enemy is hit by the bullet then their movement will stop. If their movement is stopped, a timer will tick up,
        //and if the timer hits enough seconds, the enemy will regain movement. timer++
        //if boject is hit again, it will unfreeze itself. 
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //If the enemy is less than 4 spaces away from the player, the enemy will change color to blue, to indicate chasing, and will chase the player. 
        if (distance < enemyDistance)
        {
            //This brings the enemy towards the player
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            agent.SetDestination(newPos);
            GetComponent<MeshRenderer>().material.color = Color.blue; //sets the enemy color to blue to indicate that their state has changed. 
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyHit");
        if (!isFrozen && other.gameObject.tag == "bullet")
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            isFrozen = true;
            enemy.isKinematic = true;
        }

        if (other.gameObject.tag == "bullet2")
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            enemy.isKinematic = true;
            isFrozen2 = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

}