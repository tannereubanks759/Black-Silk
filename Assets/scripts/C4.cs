using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    public GameObject brick;
    public GameObject det;
    public GameObject brickPref;
    public GameObject wallPos;
    //raycast
    Ray RayOrigin;
    RaycastHit HitInfo;
    public LayerMask mask;

    public GameObject placedBrick;

    public bool placed = false;
    

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
            placedBrick = Instantiate(brickPref, wallPos.transform.position, Quaternion.identity);
            det.SetActive(true);
        }
    }
    void explode()
    {
        Destroy(placedBrick);
    }
}
