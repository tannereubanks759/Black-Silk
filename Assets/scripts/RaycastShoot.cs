using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    Ray RayOrigin;
    RaycastHit HitInfo;
    public LayerMask mask;

    private Vector3 hitPoint;

    public ParticleSystem shootSystem;

    private float nextFire = 0;
    public float fireRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            shootSystem.Stop();
            RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(RayOrigin, out HitInfo, 30f, mask))
            {
                hitPoint = HitInfo.point;
                Vector3 ShootDir = hitPoint - shootSystem.gameObject.transform.position;
                shootSystem.gameObject.transform.rotation = Quaternion.LookRotation(ShootDir, Vector3.up);
                shootSystem.Play();
            }
            else
            {
                shootSystem.transform.rotation = this.transform.rotation;
                shootSystem.Play();
            }
            nextFire = Time.time + fireRate;
        }
        
        
    }
}
