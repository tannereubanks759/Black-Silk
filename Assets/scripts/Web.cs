using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private GameObject Player;
    private Vector3 direction;
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        direction = Player.transform.position - this.transform.position;
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * speed;
    }

}
