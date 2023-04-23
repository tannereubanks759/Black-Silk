using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject bossEnterance;
    public GameObject bossRoom;

    public Animator bossAnim;

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
            bossAnim.SetBool("PlayerEnter", true);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }
    public void enableBoss()
    {
        bossRoom.SetActive(true);
    }

}
