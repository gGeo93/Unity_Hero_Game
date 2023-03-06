using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuirksSlidersFunctionality : MonoBehaviour
{
    QuirksSliders quirksSliders;
    [SerializeField] PlayerAnimatingConditions playerAnimatingConditions;
    [SerializeField] ParticleForces particleForces;
    void Awake() 
    {
        quirksSliders = GetComponent<QuirksSliders>();
    }
    public float QuirkEndurance(Slider quirkSlider, float reductionRythm, float rate)
    {
        quirkSlider.value += reductionRythm * Time.deltaTime;
        if(quirkSlider.value <= quirkSlider.minValue + 0.01f)
        {
            reductionRythm = rate;
            rate = - reductionRythm;
        }    
        return reductionRythm;
    }
    public void QuirkEndurance(Slider quirkSlider, int lossOfEnergy)
    {
        quirkSlider.value -= lossOfEnergy;
    }
    public float QuirkRefill(Slider quirkSlider,float fillingRythm, float rate)
    {
        quirkSlider.value += fillingRythm * Time.deltaTime;
        if(quirkSlider.value >= quirkSlider.maxValue - 0.01f)
            fillingRythm = rate;
        return fillingRythm;
    }
    public int QuirkRefill(Slider quirkSlider, int fillingRythm, int rate)
    {
        quirkSlider.value += fillingRythm;
        if(quirkSlider.value >= quirkSlider.maxValue - 0.01f)
            fillingRythm = rate;
        return fillingRythm;
    }
}
