using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private WindController windController;
    private Rigidbody rigidBody;
    public float forceMult;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        windController = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rigidBody.AddForce(windController.windForce, ForceMode.Force);
    }
}
