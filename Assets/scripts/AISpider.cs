using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AISpider : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 playerPos;
    public int visionRange = 20;
    public int visionRangeAttacking;
    public int visionRangeIdle;
    public float attackRange = .5f;
    public LayerMask visionLayers;

    public AudioSource impactSource;
    public AudioClip impactClip;
    public AudioClip deathClip;

    public float health = 150;

    public NavMeshAgent agent;

    private Vector3 SpawnLocation;

    private bool attackOnCooldown = false;

    private float nextTime;

    public bool chasing = false;

    public Animator anim;


    public AudioSource spiderAudio;
    public AudioClip deathclip;

    public boss bossScript;
    float rate = 3;

    public GameManager gm;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        isDead = false;
        if(this.gameObject.tag == "bossMiniSpider")
        {

            player = GameObject.FindGameObjectWithTag("Player");
            bossScript = GameObject.Find("boss").GetComponent<boss>();
        }
        agent = GetComponent<NavMeshAgent>();
        SpawnLocation = transform.position;
        attackOnCooldown = false;
        visionRangeAttacking = visionRange * 2;
        visionRangeIdle = visionRange;
        health = 100;
        nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {

            playerPos = player.transform.position;
            if (CanSeePlayer() && attackOnCooldown == false)
            {
                agent.isStopped = false;

                anim.SetBool("IsWalking", true);
                Chase();
            }
            else if (attackOnCooldown == true)
            {
                visionRange = visionRangeIdle;
                agent.isStopped = false;
                if (agent.transform.position != SpawnLocation)
                {

                    anim.SetBool("IsWalking", true);
                }

                agent.SetDestination(SpawnLocation);
            }
            else
            {

                anim.SetBool("IsWalking", false);
                agent.isStopped = true;
            }


            if (CanSeePlayer() && (Vector3.Distance(transform.position, player.transform.position) <= attackRange) && attackOnCooldown == false)
            {
                chasing = false;
                player.GetComponent<CharacterControllerScript>().Damage(10);
                StartCoroutine(cooldown());
            }

            if (Time.time > nextTime)
            {
                rate = Random.Range(3, 7);
                spiderAudio.Play();
                nextTime = Time.time + rate;
            }
        }
        
    }
    bool CanSeePlayer()
    {
        bool inRange = Vector3.Distance(transform.position, playerPos) <= visionRange;

        if (inRange)
        {
            if(chasing == true)
            {
                return true;
            }
            else if (Physics.Raycast(transform.position, playerPos - transform.position, out RaycastHit hit, visionRange, visionLayers))
            {
                if(hit.collider.gameObject.name == "Player")
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
        agent.SetDestination(playerPos);

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
        if (!isDead)
        {
            if (other.gameObject.name == "ShootSystem")
            {
                if (health > 75)
                {
                    gm.PlaySpiderSound(impactClip);
                }
                else
                {
                    impactSource.PlayOneShot(deathClip, 1f);
                }
                health -= 50;
            }
            if (health <= 0)
            {
                die();
            }
        }
        

    }
    void die()
    {
        
        spiderAudio.clip = deathclip;
        spiderAudio.volume = 1;
        spiderAudio.maxDistance = 100;
        spiderAudio.Play();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        isDead = true;
        bossScript.RemoveSpider(this.gameObject);
        anim.SetBool("Dead", true);
    }
}
