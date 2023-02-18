using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    void Awake()
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        particleForces = GetComponent<ParticleForces>();
    }
    void Start() 
    {
        quirksRateChange = GameManager.Instance.quirksRateChange;
        quirksSliders = GameManager.Instance.quirksSliders;
    }
    public void Swing()
    {
        if(Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.B) && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isDead)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            physicalConditions.AirLimit = playerAnimatingConditions.canUseOneforAll ? 5f : 1.5f;
        }
        else if(quirksRateChange.blackWhipRate < 0 && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.Space) && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isDead)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = true;
            physicalConditions.AirLimit = playerAnimatingConditions.canUseOneforAll ? 10f : 5f;
            particleForces.BlackWhipApplied();
            quirksRateChange.blackWhipRate = -0.1f;
            GameManager.Instance.quirksSlidersFunctionality.QuirkEndurance(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, 0.2f);
        }
        else if((!Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.B)) && quirksRateChange.blackWhipRate > 0)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            particleForces.BlackWhipStopped();
            quirksRateChange.blackWhipRate = 0.1f;
            GameManager.Instance.quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, -0.1f);
        }
        if(quirksRateChange.blackWhipRate > 0)
        {
            playerAnimatingConditions.isUsingBlackWhipForSwing = false;
            physicalConditions.AirLimit = playerAnimatingConditions.canUseOneforAll ? 5f : 1.5f;
            quirksRateChange.blackWhipRate = 0.1f;
            GameManager.Instance.quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider,ref quirksRateChange.blackWhipRate, -0.1f);
        }
    }
}
