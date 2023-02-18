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
    public void QuirkEndurance(Slider quirkSlider,ref float reductionRythm, float fillRate)
    {
        quirkSlider.value += reductionRythm * Time.deltaTime;
        if(quirkSlider.value <= quirkSlider.minValue + 0.01f)
            reductionRythm = fillRate;
    }
    public void QuirkRefill(Slider quirkSlider,ref float fillingRythm, float reductionRate)
    {
        quirkSlider.value += fillingRythm * Time.deltaTime;
        if(quirkSlider.value >= quirkSlider.maxValue - 0.01f)
            fillingRythm = reductionRate;
    }
}
