using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private GameObject Player;
    private Vector3 direction;
    private Rigidbody rb;
    public float speed;

    private float nextTime;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        direction = Player.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * speed;

        nextTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > nextTime)
        {
            currentTime++;
            nextTime = Time.time + 1;
        }
        if (currentTime == 3)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
