using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool constantWind;
    public float windStrength;
    public float frequency;
    public WindZone self;
    public float pulseWidth;
    public GameObject directionVectorAnchor; // Variables set in unity editor

    public Vector3 windForce;
    public Vector3 windDirection;

    private ArrayList data;
    private bool semaphore = true;
    private const float PI = 3.1415f;
    private int i;
    private float pulseStart; //starting time of the pulse
    private bool pulse;

    void Start()
    {
        i = 0;
        data = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        /* |              ^          ^
         * |              |     *    |
         * |           *  |----------|  *
         * |     *        |          |        *
         * |   *          |          |          *
         * | *            |          |           *
         * |*             |          |            *
         * |______________|__________|________________
         */

        if (constantWind)
        {
            calculateDirection();
            windForce = windStrength*windDirection * 3;
            
        }
        else
        {
            float WindSignal = Mathf.Sin(2 * PI * Time.fixedTime / frequency);

            if (pulse)
            {
                float wind = windFunction(Time.fixedTime);
                self.windMain = wind + 1;
                calculateDirection();
                Debug.Log("Wind Pulse Active");
                windForce = self.windMain * windDirection * 3;

                if (wind == 0)
                {
                    pulse = false;
                }
            }
            else
            {
                pulse = WindSignal >= (0.99f);
                pulseStart = Time.fixedTime;
                self.windMain = 1f;
                calculateDirection();
                windForce = self.windMain * windDirection * 0;
            }
            /* string row = Time.fixedTime + "," + self.windMain;
            data.Add(row);
            if((Time.fixedTime > 15) && semaphore)
            {
                saveData();
                semaphore = false;
            } */
        }

    }

    private void calculateDirection()
    {
        Vector3 O = gameObject.transform.position;
        Vector3 P = directionVectorAnchor.transform.position;
        windDirection = (P - O)/(P - O).magnitude; // unit vector
    }

    private void saveData()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\caruana9\Desktop\ForceData.csv");
        for (int i=0; i< data.Count; i++)
        {
            file.WriteLine(data[i]);

        }
        Debug.Log("Data Saved");
    }

    //returns a sustained sin^2 function, where the first and last quarters of the period are sin^2, otherwise it returns the sustained amplitude
    private float windFunction(float time)
    {
        float t = time - pulseStart;
        float L = pulseWidth; //period
        float A = windStrength;
        if (t < L/4 && t >= 0)
        {
            float sin = Mathf.Sin(2 * PI * t / L);
            return A * Mathf.Pow(sin, 2);
        }
        else if (t < 3*L/4)
        {
            return A;
        }
        else if (t <= L)
        {
            float sin = Mathf.Sin(2 * PI * t / L);
            return A * Mathf.Pow(sin, 2);
        }
        else
        {
            return 0;
        }
    }
    
}
