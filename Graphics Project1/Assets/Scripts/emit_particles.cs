using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emit_particles : MonoBehaviour {

    public ParticleSystem pe;
    public ParticleSystem pe2;
    public ParticleSystem pe3;
    public ParticleSystem pe4;

    // Use this for initialization
    void Start () {
		
	}

    void OnDestroy()
    {
        
        ParticleSystem explosion = Instantiate(pe) as ParticleSystem;
        ParticleSystem explosion2 = Instantiate(pe2) as ParticleSystem;
        ParticleSystem explosion3 = Instantiate(pe3) as ParticleSystem;
        ParticleSystem explosion4 = Instantiate(pe4) as ParticleSystem;
 
        explosion.transform.position = transform.position;
        explosion2.transform.position = transform.position;
        explosion3.transform.position = transform.position;
        explosion4.transform.position = transform.position;
     
        
     
        explosion.Play();
        explosion2.Play();
        explosion3.Play();
        explosion4.Play();
 

        Destroy(explosion, explosion.main.duration);
        Destroy(explosion2, explosion2.main.duration);
        Destroy(explosion3, explosion3.main.duration);
        Destroy(explosion4, explosion4.main.duration);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
