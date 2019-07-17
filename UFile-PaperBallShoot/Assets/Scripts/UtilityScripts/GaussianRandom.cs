using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianRandom : MonoBehaviour
{
    public static double generateRandom()
    {
        Random rand = new Random();
        
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        double randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log10(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        Debug.Log("Random Gaussian Numbers: u1 " + u1 + ", u2 " + u2 + ", stdNorm " + randStdNormal);

        return randStdNormal;
    }
}
