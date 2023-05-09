using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 5f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;

    float countdown;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }//if
    }

    void Explode()
    {
        //Debug.Log("BOOM!");

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        FindObjectOfType<AudioManager>().Play("Powerup Hit");

        foreach (Collider nerbyObject in colliders)
        {
            Rigidbody rb = nerbyObject.GetComponent<Rigidbody>();
            
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
            if(nerbyObject.gameObject.CompareTag("Ball"))
            {
                nerbyObject.gameObject.tag = "Ignore Ball";
            }
        }//foreach

        Destroy(gameObject);
    }//Explode
}
