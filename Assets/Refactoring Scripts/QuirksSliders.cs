using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuirksSliders : MonoBehaviour
{
    //One For All Sliders
    public Slider handsAttackSlider;
    public Slider legAttackSlider;
    public Slider fingersAttackSlider;
    //Other quirks' sliders
    public Slider dangerSenseSlider;
    public Slider airFloatSlider;
    public Slider blackWhipSlider;
    public Slider smokeSlider;
    public Slider fajinSlider;
    public Slider shiftingGearsSlider;

    void Start() 
    {
        handsAttackSlider.value = 50;
        legAttackSlider.value = 50;
        fingersAttackSlider.value = 50;

        dangerSenseSlider.value = 1f;
        airFloatSlider.value = 1f;
        blackWhipSlider.value = 1f;
        smokeSlider.value = 1f;
        fajinSlider.value = 1f;
        shiftingGearsSlider.value = 1f;
    }
}
