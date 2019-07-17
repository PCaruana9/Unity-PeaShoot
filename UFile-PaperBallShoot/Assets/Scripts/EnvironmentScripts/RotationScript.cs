using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    /*
     * RotationScript.cs
     * Author: Peter Caruana
     * York University (C) 2019
     * Vision Labs
     * 
     * This is a utility Script. Slap this bad boy on a GameObject and it will rotate
     */

    public float rotationSpeed;
    public bool x;
    public bool y;
    public bool z;

    private Vector3[] xyzAxis = new Vector3[3];
    
    // Start is called before the first frame update
    void Start()
    {
        xyzAxis[0] = new Vector3(1, 0, 0); //x
        xyzAxis[1] = new Vector3(0, 1, 0); //y
        xyzAxis[2] = new Vector3(0, 0, 1); //z
    }

    // Update is called once per frame
    void Update()
    {
        maskAxisChoice();

        if (x)
        {
            transform.Rotate(xyzAxis[0], rotationSpeed);
        }
        else if (y)
        {
            transform.Rotate(xyzAxis[1], rotationSpeed);
        }
        else if (z)
        {
            transform.Rotate(xyzAxis[2], rotationSpeed);
        }
        
    }

    void maskAxisChoice()
    {
        if (x)
        {
            y = false;
            z = false;
        }
        else if (y)
        {
            x = false;
            z = false;
        }
        else if (z)
        {
            x = false;
            y = false;
        }
    }
}
