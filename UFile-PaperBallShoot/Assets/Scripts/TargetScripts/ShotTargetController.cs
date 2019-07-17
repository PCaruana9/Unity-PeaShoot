using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTargetController : MonoBehaviour

{
    /*
     * Attach this script to turn the game object into an acceptable target for a shot
     * 
     * Author: Peter Caruana
     * Vision Labs, York University
     * Copyright (C) creative commons
     */
    public Material material;
    public Material defaultMaterial;
    public ExperimentController experimentController;
    // Start is called before the first frame update
    void Start()
    {
        material.color = defaultMaterial.color;
        experimentController = GameObject.FindGameObjectWithTag("ExpCont").GetComponent<ExperimentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "ExperimentObject")
        {
            Destroy(other);
            material.color = Color.green;
            //experimentController.EndAndPrepare();

        }
    }
}
