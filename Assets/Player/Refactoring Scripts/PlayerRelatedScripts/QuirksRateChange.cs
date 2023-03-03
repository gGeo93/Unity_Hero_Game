using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuirksRateChange : MonoBehaviour
{
    //OFA Quirk Attacks Rates
    public int detroitSmashRate = 0;
    public int shootStyleRate = 0;
    public int fingerSmashRate = 0;
    //Other Quirk Rates
    public float dangerSenseRate = 0;
    public float airFloatingRate = 0;
    public float smokeScreenRate = 0;
    public float blackWhipRate = 0;
    public float faJinRate = 0;
    public float shiftingGearRate = 0;

    void Start() 
    {
        detroitSmashRate = 1;
        shootStyleRate = 1;
        fingerSmashRate = 1;

        faJinRate = -0.25f;
        dangerSenseRate = -0.25f;
        airFloatingRate = -0.05f;
        smokeScreenRate = 0.05f;
        blackWhipRate = -0.1f;
    }
}
