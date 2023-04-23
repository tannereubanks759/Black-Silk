using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioClip impact;
    public AudioSource player;
    public float volume = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.name == "ShootSystem")
        {
            player.PlayOneShot(impact, volume);
        }
    }
}
