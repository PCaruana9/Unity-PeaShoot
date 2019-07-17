using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public GameObject realHand;
    public HandCursorController realHandController;
    public Collider collider;
    public Rigidbody rigidbody;
    public  bool objectGrabbed = false;

    private Vector3 prevPosition;
    private Vector3 currPosition;
    private Vector3 handVelocity;
    private float timeDelta = 0.01f;
    private float prevTime;
    private float currTime;
    private Stack<Vector3> velocityHistory = new Stack<Vector3>();
    [SerializeField]
    private OVRInput.Controller m_controller;
    // Start is called before the first frame update
    void Start()
    {
        realHandController = GameObject.FindGameObjectWithTag("Cursor").GetComponent<HandCursorController>();
        if (realHand == null)
        {
            
            realHand = realHandController.realHand ;
        }
        
        prevPosition = realHand.transform.localPosition;
        currPosition = Vector3.zero;
        prevTime = 0;
        handVelocity = Vector3.zero;
        velocityHistory = new Stack<Vector3>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        OVRInput.Update();
        //Trigger is currently pressed down
        //Oculus unity has weird bug, where button presses arent registered however if you click the home button and go back, then they start working.
        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        //print("" + OVRInput.GetConnectedControllers());

        calculateVelocity();

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger, m_controller) && !objectGrabbed & !realHandController.holdingItem)
        {
            
            if (collider.bounds.Contains(realHand.transform.position))
            {
                realHandController.holdingItem = true; //semaphore lock for grabable objects
                Debug.Log(">>  " + gameObject.name + "Trigger Is pressed");
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                rigidbody.isKinematic = false;
                transform.SetParent(realHand.transform);
                grabed();
            }


        }
        else if (!OVRInput.Get(OVRInput.RawButton.RIndexTrigger, m_controller) && objectGrabbed)
        {
            realHandController.holdingItem = false;
            Debug.Log(">>  " + gameObject.name + " Trigger Released");
            transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            rigidbody.isKinematic = false;
            droped();
            Vector3 avgVelocity = velocityAverage();
            //Average velocity of previous 3 velocity frames for smoothness
            throwObject(avgVelocity*3);

        }

        //Calculate hand velocity
        

    }
    void calculateVelocity()
    {
        currTime = Time.fixedTime;
        currPosition = realHand.transform.position;
        if (currTime - prevTime >= timeDelta)
        {

            velocityHistory.Push((currPosition - prevPosition) / timeDelta);
            prevTime = currTime;
            prevPosition = new Vector3(currPosition.x, currPosition.y, currPosition.z);
        }
    }
    Vector3 velocityAverage()
    {
        Vector3 avg = (velocityHistory.Pop() + velocityHistory.Pop() + velocityHistory.Pop() + velocityHistory.Pop() + velocityHistory.Pop() + velocityHistory.Pop()) / 6;
        velocityHistory.Clear();
        return avg;
    }
    void throwObject(Vector3 velocity)
    {
        Debug.Log("Object Thrown with " + velocity.ToString() + ": " + velocity.magnitude);
        rigidbody.AddForce(velocity, ForceMode.Impulse);
        
    }

    void grabed()
    {
        objectGrabbed = true;
    }
    void droped()
    {
        objectGrabbed = false;
    }
   
}
