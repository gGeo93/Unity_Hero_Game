using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuirksSlidersFunctionality : MonoBehaviour
{
    QuirksSliders quirksSliders;
    void Awake() 
    {
        quirksSliders = GetComponent<QuirksSliders>();
    }
    public void QuirkEndurance(Slider quirkSlider,ref float reductionRythm, float rate)
    {
        quirkSlider.value += reductionRythm * Time.deltaTime;
        if(quirkSlider.value <= quirkSlider.minValue + 0.01f)
            reductionRythm = rate;
    }
    public void QuirkEndurance(Slider quirkSlider, int lossOfEnergy)
    {
        quirkSlider.value -= lossOfEnergy;
    }
    public void QuirkRefill(Slider quirkSlider,ref float fillingRythm, float rate)
    {
        quirkSlider.value += fillingRythm * Time.deltaTime;
        if(quirkSlider.value >= quirkSlider.maxValue - 0.01f)
            fillingRythm = rate;
    }
    public void QuirkRefill(Slider quirkSlider,ref int fillingRythm, int rate)
    {
        quirkSlider.value += fillingRythm;
        if(quirkSlider.value >= quirkSlider.maxValue - 0.01f)
            fillingRythm = rate;
    }
}
