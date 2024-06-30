using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{

    public Animator animator;


    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkpointRange;

    public static bool walking;
    public static bool attack;
    public static bool getHit;
    //Attacking
    public float timeBetweenAtacks;
    public bool AlreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (getHit)
        {
            animator.SetBool("gethit", true);
            
        }
        if (!getHit)
        {
            animator.SetBool("gethit", false);
        }
        

        if (!walking)
        {
            animator.SetBool("run", false);
        }
        if (walking)
        {
            animator.SetBool("run", true);
        }
        if (!attack)
        {
            animator.SetBool("hit", false);
        }
        if (attack)
        {
            animator.SetBool("hit", true);
        }


        //Walking Animation;






        //Check for sight or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

    }



    private void Patroling()
    {
        Debug.Log("Patroling");
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            walking = true;
            attack = false;
        }

        Vector3 distanvceToWalkPoint = transform.position - walkPoint;
        if(distanvceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }


    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log("Chaseing");
        walking = true;
        attack = false;
    }


    private void AttackPlayer()
    {
        //Stop the enemy
        agent.SetDestination(transform.position);
        walking = false;

        //transform.LookAt(player);
        if (AlreadyAttacked)
        {
            attack = false;
        }
        if (!AlreadyAttacked && !getHit)
        {
            //Attack Code
            Debug.Log("Attacked by Enemy");
            attack = true;

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAtacks);
        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }


    public static void GetHit(string hitType)
    {
        if(hitType == "normalhit")
        {
            getHit = true;
            walking = false;
            attack = false;
        }
    }


}
