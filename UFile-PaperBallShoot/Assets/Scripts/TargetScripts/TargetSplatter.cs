using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSplatter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject child;
    void Start()
    {
        Renderer childRend = child.GetComponent<Renderer>();
        childRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void OnCollisionEnter(Collision collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;
        child.transform.position = contactPoint;
        child.transform.localPosition = child.transform.localPosition - new Vector3(0, 0.35f, 0);
        Renderer childRend = child.GetComponent<Renderer>();
        childRend.enabled = true;
        Destroy(collision.gameObject);
    }
}
