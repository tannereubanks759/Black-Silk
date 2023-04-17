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


    int spawnCountRandom;
    int spawnCount;
    public GameObject spider;
    public GameObject[] spawnLocations;
    bool isSpawning = false;

    bool spawnState = false;
    bool shootState = false;

    // Update is called once per frame
    void Update()
    {
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
    }
    void lookAtPlayer()
    {
        if(shootCountRandom == 0 && shootState == true)
        {
            shootCountRandom = Random.Range(40, 60);
            currentShootCount = 0;
        }


        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = newRotation;

        if (seesPlayer && !isShooting && currentShootCount <= shootCountRandom)
        {
            currentShootCount++;
            StartCoroutine(shoot());
        }
        if(currentShootCount >= shootCountRandom)
        {
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
    IEnumerator spawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(10);
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Instantiate(spider, spawnLocations[i].transform.position, Quaternion.identity);
        }
        isSpawning = false;
    }
    IEnumerator Wake()
    {
        yield return new WaitForSeconds(4);
        shootState = true;
    }
    public void spawnStateFunction()
    {
        if (spawnCountRandom == 0 && spawnState == true)
        {
            spawnCount = 0;
            spawnCountRandom = Random.Range(4, 8);
        }
        if (isSpawning == false && spawnCount <= spawnCountRandom)
        {
            spawnCount++;
            StartCoroutine(spawn());
        }
        if (spawnCount >= spawnCountRandom)
        {

            shootState = true;
            spawnState = false;
        }
    }
    public void sleeping()
    {
        StartCoroutine(Wake());
    }
}
