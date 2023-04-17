using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject bossEnterance;
    public GameObject bossRoom;

    // Start is called before the first frame update
    void Start()
    {
        bossRoom.SetActive(false);
        bossEnterance.SetActive(true);
        mainRoom.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mainRoom.SetActive(false);
            bossEnterance.SetActive(true);
        }
    }
    public void enableBoss()
    {
        bossRoom.SetActive(true);
    }
}
