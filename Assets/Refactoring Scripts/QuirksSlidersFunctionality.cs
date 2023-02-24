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
    public void GeneralOFAAttacksPower(float OFAmp)
    {
        OFAmp = playerAnimatingConditions.canUseOneforAll ? OFAmp / 100f : 0f;
        OnCalculatingOFAHandsDamage(OFAmp);
        OnCalculatingOFALegDamage(OFAmp);
        OnCalculatingOFAFingersDamage(OFAmp);
    }
    private void OnCalculatingOFAHandsDamage(float OFAperCent)
    {
        particleForces.punchDamage = Mathf.FloorToInt((0.40f + OFAperCent) * (quirksSliders.handsAttackSlider.value) + 10);
    }
    private void OnCalculatingOFALegDamage(float OFAperCent)
    {
        particleForces.ShootStyleDamage = Mathf.FloorToInt((0.35f + OFAperCent) * (quirksSliders.legAttackSlider.value + 10));
    }
    private void OnCalculatingOFAFingersDamage(float OFAperCent)
    {
        particleForces.fingersDamage = Mathf.FloorToInt((0.30f + OFAperCent) * (quirksSliders.fingersAttackSlider.value + 10));
    }
}
