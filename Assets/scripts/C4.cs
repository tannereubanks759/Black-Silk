using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    public GameObject brick;
    public GameObject det;
    public GameObject brickPref;
    public GameObject wallPos;
    public GameObject wall;
    public GameManager gm;
    //raycast
    Ray RayOrigin;
    RaycastHit HitInfo;
    public LayerMask mask;

    public GameObject placedBrick;

    public bool placed = false;

    public float distance;
    public GameObject RockDebris;
    // Update is called once per frame
    void Update()
    {
        if (placed == true && Input.GetKeyDown(KeyCode.E))
        {
            explode();
            Destroy(this.gameObject);
        }
        if(!placed && Input.GetKeyDown(KeyCode.E))
        {
            place();
        }
    }
    void place()
    {
        RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(RayOrigin, out HitInfo, 3f, mask))
        {
            placed = true;
            Destroy(brick);
            placedBrick = Instantiate(brickPref, wallPos.transform.position, wallPos.transform.rotation);
            det.SetActive(true);
        }
    }
    void explode()
    {

        GameObject player = GameObject.Find("Player");
        gm.enableBoss();
        Destroy(placedBrick);
        Vector3 wallPosV = wallPos.transform.position;
        wall.SetActive(false);
        RockDebris.SetActive(true);
        if(Vector3.Distance(player.transform.position, wallPosV) < distance)
        {
            Debug.Log(Vector3.Distance(player.transform.position, wallPosV));
            player.GetComponent<CharacterControllerScript>().Damage(100);
        }
    }
}
