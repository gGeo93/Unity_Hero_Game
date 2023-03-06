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
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
    }

    public void Swing()
    {
        if(Input.GetKey(KeyCode.B) && quirksRateChange.blackWhipRate <= 0f && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isDead)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = true;
            quirksRateChange.blackWhipRate = -0.1f;
            quirksRateChange.blackWhipRate = quirksSlidersFunctionality.QuirkEndurance(quirksSliders.blackWhipSlider, quirksRateChange.blackWhipRate, 0.2f);
            particleForces.BlackWhipApplied();
        }
        else
        {
            particleForces.BlackWhipStopped();
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            quirksRateChange.blackWhipRate = 0.1f;
            quirksRateChange.blackWhipRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider, quirksRateChange.blackWhipRate, -0.1f);
        }
    }
}
