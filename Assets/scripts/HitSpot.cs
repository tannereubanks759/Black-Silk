using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpot : MonoBehaviour
{
    public Animator anim;
    public string animName;
    public boss boss;
    public AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        source = this.gameObject.GetComponentInParent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.name == "ShootSystem")
        {
            Debug.Log("Hit");
            source.clip = clip;
            source.Play();
            boss.health--;
            anim.SetBool(animName, true);
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
