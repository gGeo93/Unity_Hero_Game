using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWhipSwinging : MonoBehaviour
{
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    void Awake()
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        particleForces = GetComponent<ParticleForces>();
    }
    void Start() 
    {
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
    }
    public void Swing()
    {
        if(quirksRateChange.blackWhipRate < 0 && Input.GetKey(KeyCode.B) && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isDead)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = true;
            particleForces.BlackWhipApplied();
            quirksRateChange.blackWhipRate = -0.1f;
            quirksSlidersFunctionality.QuirkEndurance(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, 0.2f);
        }
        else if(!Input.GetKey(KeyCode.B))
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            particleForces.BlackWhipStopped();
            quirksRateChange.blackWhipRate = 0.1f;
            quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, -0.1f);
        }
        if(quirksRateChange.blackWhipRate > 0)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            quirksRateChange.blackWhipRate = 0.1f;
            quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, -0.1f);
        }
    }
}