using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaJinQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces fajinParticlesEffect;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    const float fajinLossRate = -0.20f;
    const float fajinRechargeRate = 0.15f;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        fajinParticlesEffect = GetComponent<ParticleForces>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
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
            quirksRateChange.faJinRate = quirksSlidersFunctionality.QuirkEndurance(quirksSliders.fajinSlider, quirksRateChange.faJinRate, fajinRechargeRate);
            //FaJin particles go to a loop
            StartCoroutine(FaJinParticlesApplied());
            //if Q,Z,X stores energy to a specific part of Deku's body
            StoringEnergyToBodyParts();
            //You can swip from storing energy from one part of the body to the other
            //When FaJin bar reaches zero the storing energy process is over and the bar begins to fill at a specific rate
            //The Q,Z,X bars start to lose their energy with a rate
        }
        else if(!Input.GetKey(KeyCode.G) && quirksRateChange.faJinRate > 0)
        {
            playerAnimatingConditions.isUsingFaJin = false;
            FaJinParticlesStop(fajinParticlesEffect.fingersParticles);
            quirksRateChange.faJinRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.fajinSlider, quirksRateChange.faJinRate, fajinLossRate);
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            playerAnimatingConditions.isUsingFaJin = false;
            quirksRateChange.faJinRate = fajinRechargeRate;
            StartCoroutine(AllFajinEffectsStop());
        }
    }
    public bool FajinEnergyRelease(DroneHealthLoss droneHealthLoss, string partOfBody)
    {
        if(fajinParticlesEffect.detroitSmashConcentration[0].enabled && partOfBody == "Punch_PS")
        {
            int punchDamage = (int)quirksSliders.handsAttackSlider.value + 50;
            droneHealthLoss.droneImg.rectTransform.localScale -= Vector3.right * punchDamage / 150; 
            droneHealthLoss.droneImgText.text = (int.Parse(droneHealthLoss.droneImgText.text) - punchDamage).ToString();
            quirksSliders.handsAttackSlider.value = 0;
            fajinParticlesEffect.detroitSmashConcentration[0].enabled = false;
            fajinParticlesEffect.detroitSmashConcentration[1].enabled = false;
            return true;
        }
        else if(fajinParticlesEffect.shootStyleConcentration.enabled && partOfBody == "Kick_PS")
        {
            int legDamage = (int)quirksSliders.legAttackSlider.value + 50;
            droneHealthLoss.droneImg.rectTransform.localScale -= Vector3.right * legDamage; 
            droneHealthLoss.droneImgText.text = (int.Parse(droneHealthLoss.droneImgText.text) - legDamage).ToString();
            quirksSliders.fingersAttackSlider.value = 0;
            fajinParticlesEffect.shootStyleConcentration.enabled = false;
            return true;
        }
        else if(fajinParticlesEffect.fingerSmashConcentration.enabled && partOfBody == "Finger_PS")
        {
            int fingersDamage = (int)quirksSliders.fingersAttackSlider.value + 50;
            droneHealthLoss.droneImg.rectTransform.localScale -= Vector3.right * fingersDamage; 
            droneHealthLoss.droneImgText.text = (int.Parse(droneHealthLoss.droneImgText.text) - fingersDamage).ToString(); 
            quirksSliders.fingersAttackSlider.value = 0;
            fajinParticlesEffect.fingerSmashConcentration.enabled = false;
            return true;
        }
        return false;
    }
    private void StoringEnergyToBodyParts()//enable false
    {
        if(playerAnimatingConditions.isUsingFaJin)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                if(quirksSliders.handsAttackSlider.value == 100) return;
                quirksRateChange.detroitSmashRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.handsAttackSlider, quirksRateChange.detroitSmashRate);
                fajinParticlesEffect.detroitSmashConcentration[0].enabled = true;
                fajinParticlesEffect.detroitSmashConcentration[1].enabled = true;
            }
            else if(Input.GetKey(KeyCode.Z))
            {
                if(quirksSliders.legAttackSlider.value == 100) return;
                fajinParticlesEffect.shootStyleConcentration.enabled = true;
                quirksRateChange.shootStyleRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.legAttackSlider, quirksRateChange.shootStyleRate);
            }
            else if(Input.GetKey(KeyCode.X))
            {
                if(quirksSliders.fajinSlider.value == 100) return;
                fajinParticlesEffect.fingerSmashConcentration.enabled = true;
                quirksRateChange.fingerSmashRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.fingersAttackSlider, quirksRateChange.fingerSmashRate);
            }
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
    private IEnumerator AllFajinEffectsStop()
    {
        yield return new WaitForSeconds(20);
        fajinParticlesEffect.detroitSmashConcentration[0].enabled = false;
        fajinParticlesEffect.detroitSmashConcentration[1].enabled = false;
        
        fajinParticlesEffect.shootStyleConcentration.enabled = false;

        fajinParticlesEffect.fingerSmashConcentration.enabled = false;
    }
    private void FaJinParticlesStop(ParticleSystem particleSystem)
    {
        fajinParticlesEffect.fingersParticles.Stop();
        var particlesMain = fajinParticlesEffect.FaJinParticles.main;
        particlesMain.loop = false;
    }
}
