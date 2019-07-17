using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    
    public float shotRotate;
    public float handRotate;
    public int noiseSpread;
    public bool gaussianNoiseActive;


    public GameObject bulletPrefab;
    [SerializeField]
    private OVRInput.Controller m_controller;
    private PhysicsSphereController child;

    private Vector3 spawnVector = new Vector3(4.76f, -0.004f, 2.41f);
    private Vector3 shotVector;
    private Vector3 defaultShot = new Vector3(1, 0, 0);
    private float timeLast = 0;
    // Start is called before the first frame update
    void Start()
    {
        child = GetComponentInChildren<PhysicsSphereController>();
        shotVector = defaultShot;
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        //Trigger is currently pressed down
        //Oculus unity has weird bug, where button presses arent registered however if you click the home button and go back, then they start working.
        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        //print("" + OVRInput.GetConnectedControllers());
        child = GetComponentInChildren<PhysicsSphereController>();
        bool isLoaded = false;

        if (child != null)
        {
            isLoaded = child.isLoaded;
        }



        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger, m_controller) && isLoaded)
        {
            if (gaussianNoiseActive)
            {
                double Rand = GaussianRandom.generateRandom();
                float angle = (float)(Rand) * noiseSpread;
                rotate_shotVector(angle);
            }
            if (Time.fixedTime - timeLast >= 0.3f)
            {
                child.fire(shotVector);
                playShotNoise();
                timeLast = Time.fixedTime;
            }
        }
        if (OVRInput.Get(OVRInput.RawButton.A) && !isLoaded)
        {
            if (Time.fixedTime - timeLast >= 0.3f)
            {
                Debug.Log(">> Shot Reloaded");
                var shot = Instantiate(bulletPrefab, transform);
                shot.transform.localPosition = spawnVector;
                shot.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

        rotate_shotVector(shotRotate);
        rotated_hand(handRotate);

    }

    //rotates the shotVector along the y axis relative to the hand, and normalizes the vector.
    public void rotate_shotVector(float degrees)
    {
        shotVector = (Quaternion.Euler(0, 0, degrees) * (defaultShot)).normalized;
        
    }

    public void rotated_hand(float degrees)
    {
        transform.localRotation = Quaternion.Euler(-90, -90, degrees);
    }

    void playShotNoise()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

}
