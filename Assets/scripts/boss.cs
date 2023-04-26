using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 5f;
    public bool seesPlayer;
    public GameObject Web;
    public GameObject WebPos;
    bool isShooting;
    int shootCountRandom;
    int currentShootCount;

    public float health = 6;


    int spawnCountRandom = 3;
    int spawnCount;
    public GameObject spider;
    public GameObject[] spawnLocations;
    bool isSpawning = false;

    bool spawnState = false;
    bool shootState = false;

    public Animator anim;

    private bool OnCeiling = false;

    public List<GameObject> spiders;

    // Update is called once per frame

    public bool dead = false;

    void Update()
    {
        if (!dead)
        {
            Debug.Log(spiders.Count);
            if (shootState)
            {
                lookAtPlayer();

            }
            else if (spawnState)
            {

                spawnStateFunction();
            }
            else
            {
                sleeping();
            }
            if (health == 0)
            {
                dead = true;
            }
        }
        else
        {
            Die();
        }
        
    }
    void lookAtPlayer()
    {
        if(shootCountRandom == 0 && shootState == true)
        {
            shootCountRandom = Random.Range(20, 40);
            currentShootCount = 0;
        }

        
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        if (transform.rotation != targetRotation)
        {
            anim.SetBool("isTurning", true);
        }
        else
        {
            anim.SetBool("isTurning", false);
        }
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = newRotation;
        

        if (seesPlayer && !isShooting && currentShootCount < shootCountRandom)
        {

            currentShootCount++;
            StartCoroutine(shoot());
        }
        if(currentShootCount >= shootCountRandom &&shootState == true)
        {
            shootCountRandom = 0;
            OnCeilingSet(true);
            spawnState = true;
            shootState = false;
        }
    }
    public void SetSeesPlayer(bool value)
    {
        seesPlayer = value;
    }
    IEnumerator shoot()
    {
        
        isShooting = true;
        yield return new WaitForSeconds(1);
        Instantiate(Web, WebPos.transform.position, Quaternion.identity);
        isShooting = false;
    }
    
    IEnumerator Wake()
    {
        yield return new WaitForSeconds(4);
        shootState = true;
    }
    public void spawnStateFunction()
    {
        
        if (spawnCount < spawnCountRandom && spiders.Count < 1)
        {
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                spiders.Add(Instantiate(spider, spawnLocations[i].transform.position, Quaternion.identity));
            }

            spawnCount++;
        }
        if (spawnCount == spawnCountRandom && spiders.Count < 1) 
        {
            
            shootState = true;
            OnCeilingSet(false);
            spawnCount = 0;
            spawnState = false;
        }
    }
    public void sleeping()
    {
        StartCoroutine(Wake());
    }
    public void EnterFalse()
    {
        anim.SetBool("PlayerEnter", false);
    }
    public void OnCeilingSet(bool value)
    {
        if(OnCeiling != value)
        {

            Debug.Log("SetCeiling");
            Debug.Log("SpawnCount: " + spawnCount);
            anim.SetBool("onCeiling", value);
            OnCeiling = value;
        }
        
    }
    
    public void RemoveSpider(GameObject spider)
    {
        if (spiders.Contains(spider))
        {
            spiders.Remove(spider);
        }
    }
    public void Die()
    {
        anim.SetBool("dead", true);
        StartCoroutine(player.GetComponent<CharacterControllerScript>().end());
    }
}
