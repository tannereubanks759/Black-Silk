using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    Ray RayOrigin;
    RaycastHit HitInfo;
    public LayerMask mask;

    public AudioSource sound;
    public AudioClip charge;
    public AudioClip impact;

    private Vector3 hitPoint;

    public ParticleSystem shootSystem;

    private float nextFire = 0;
    public float fireRate = 1f;



    public Animator gunPart;
    // Start is called before the first frame update
    void Start()
    {

        nextFire = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            sound.PlayOneShot(charge, .7f);
            gunPart.SetBool("Firing", true);
            nextFire = Time.time + fireRate;
        }
        
        
    }
    void shoot()
    {
        RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(RayOrigin, out HitInfo, mask))
        {
            hitPoint = HitInfo.point;
            Vector3 ShootDir = hitPoint - shootSystem.gameObject.transform.position;
            shootSystem.gameObject.transform.rotation = Quaternion.LookRotation(ShootDir, Vector3.up);
        }
        else
        {

            shootSystem.transform.rotation = this.transform.rotation;
        }
        sound.Play();
        shootSystem.Play();
    }
    void SetFireFalse()
    {
        gunPart.SetBool("Firing", false);
    }
    
}
