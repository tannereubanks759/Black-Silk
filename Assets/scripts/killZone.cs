using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
    public boss boss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boss.SetSeesPlayer(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boss.SetSeesPlayer(false);
        }
    }
}
