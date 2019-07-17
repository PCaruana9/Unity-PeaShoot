using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSphereController : MonoBehaviour
{
    
    public bool isLoaded;
    public Rigidbody rigidbody;
    public float shotStrength;
    
    public GameObject trail;
    //public GameObject particleSystem;
    void Start()
    {
        isLoaded = true;
    }

    void Update()
    {
        
        
    }

    public void fire(Vector3 shotVector)
    {
        Debug.Log("Trigger pressed >> Fireing shot!");
        trail.GetComponent<TrailRenderer>().Clear();
        trail.SetActive(true);
        transform.parent = null;
        Vector3 force = shotVector*shotStrength;//new Vector3(shotStrength, 0, 0);
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddRelativeForce(force, ForceMode.Impulse);
        isLoaded = false;
    }

    
}
