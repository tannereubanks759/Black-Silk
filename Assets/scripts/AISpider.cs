using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AISpider : MonoBehaviour
{
    public GameObject player;
    public int visionRange = 3;
    public int visionRangeAttacking;
    public int visionRangeIdle;
    public float attackRange = .5f;
    public LayerMask visionLayers;

    public float health = 150;

    private NavMeshAgent agent;

    private Vector3 SpawnLocation;

    private bool attackOnCooldown = false;

    private float nextTime;

    public bool chasing = false;


    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        SpawnLocation = transform.position;
        attackOnCooldown = false;
        visionRangeAttacking = visionRange * 2;
        visionRangeIdle = visionRange;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() && attackOnCooldown == false)
        {
            agent.isStopped = false;
            Chase();
        }
        else if(attackOnCooldown == true)
        {
            visionRange = visionRangeIdle;
            agent.isStopped = false;
            agent.SetDestination(SpawnLocation);
        }
        else
        {
            agent.isStopped = true;
        }


        if (CanSeePlayer() && (Vector3.Distance(transform.position, player.transform.position) <= attackRange) && attackOnCooldown == false) 
        {
            chasing = false;
            player.GetComponent<CharacterControllerScript>().Damage(10);
            StartCoroutine(cooldown());
        }
    }
    bool CanSeePlayer()
    {
        bool inRange = Vector3.Distance(transform.position, player.transform.position) <= visionRange;

        if (inRange)
        {
            if(chasing == true)
            {
                return true;
            }
            else if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, visionRange, visionLayers))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
    void Chase()
    {
        
        if (agent == null)
        {
            return;
        }
        chasing = true;
        visionRange = visionRangeAttacking;
        agent.SetDestination(player.transform.position);

    }
    IEnumerator cooldown()
    {

        attackOnCooldown = true;
        agent.speed = agent.speed * 2;
        yield return new WaitForSeconds(4);
        agent.speed = agent.speed / 2;
        attackOnCooldown = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.name == "ShootSystem")
        {
            health -= 50;
        }
        if(health <= 0)
        {
            die();
        }
    }
    void die()
    {
        Destroy(this.gameObject);
    }
}
