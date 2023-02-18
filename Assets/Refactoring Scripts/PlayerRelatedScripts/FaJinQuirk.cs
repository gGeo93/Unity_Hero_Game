using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaJinQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces fajinParticlesEffect;
    OneForAllConditions oneForAll;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    const float fajinLossRate = -0.25f;
    const float fajinRechargeRate = 0.05f;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        fajinParticlesEffect = GetComponent<ParticleForces>();
        oneForAll = GetComponent<OneForAllConditions>();
        quirksRateChange = GameManager.Instance.quirksRateChange;
        quirksSliders = GameManager.Instance.quirksSliders;
        quirksSlidersFunctionality = GameManager.Instance.quirksSlidersFunctionality;
    }
    public void FajinActivating()
    {
        if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.cc.isGrounded)
        {
            FajinQuirkState();
        }
    }
    private void FajinQuirkState()
    {
        if (Input.GetKey(KeyCode.G) && quirksRateChange.faJinRate < 0)
        {
            playerAnimatingConditions.isUsingFaJin = true;
            //Fajin Bar goes in a certain rate from 1 to 0
            quirksSlidersFunctionality.QuirkEndurance(quirksSliders.fajinSlider, ref quirksRateChange.faJinRate, fajinRechargeRate);
            //FaJin particles go to a loop
            StartCoroutine(FaJinParticlesApplied());
            //if Q,Z,X stores energy to a specific part of Deku's body
            //3 oneforall bars begin to fill from 0 to 100
            oneForAll.StoringEnergyToBodyParts();
            //You can swip from storing energy from one part of the body to the other
            //When FaJin bar reaches zero the storing energy process is over and the bar begins to fill at a specific rate
            //The Q,Z,X bars start to lose their energy with a rate
        }
        else if(!Input.GetKey(KeyCode.G) && quirksRateChange.faJinRate > 0)
        {
            playerAnimatingConditions.isUsingFaJin = false;
            FaJinParticlesStop();
            quirksSlidersFunctionality.QuirkRefill(quirksSliders.fajinSlider, ref quirksRateChange.faJinRate, fajinLossRate);
        }
        else if(Input.GetKeyUp(KeyCode.G))
        {
            playerAnimatingConditions.isUsingFaJin = false;
            quirksRateChange.faJinRate = fajinRechargeRate;
        }
    }
    private IEnumerator FaJinParticlesApplied()
    {
        yield return new WaitForSeconds(1.0f);
        if(playerAnimatingConditions.isUsingFaJin)
        {
            fajinParticlesEffect.FaJinParticles.Play();
            var particlesMain = fajinParticlesEffect.FaJinParticles.main;
            particlesMain.loop = true;
        }
    }

    private void FaJinParticlesStop()
    {
        fajinParticlesEffect.fingersParticles.Stop();
        var particlesMain = fajinParticlesEffect.FaJinParticles.main;
        particlesMain.loop = false;
    }
}
