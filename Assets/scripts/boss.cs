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
    public bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }
    void lookAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = newRotation;

        if (seesPlayer && !isShooting)
        {
            StartCoroutine(shoot());
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
}
