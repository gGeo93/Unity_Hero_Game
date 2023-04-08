using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    OneForAllSoundEffects soundEffects;
    PlayerLocation playerLocation;
    [SerializeField] ParticleSystem smokeQuirk;
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    float rateOfQuirkRegain;
    private void Awake() 
    {
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        soundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();    
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        playerLocation = GetComponent<PlayerLocation>();
    }
    public void SmokeQuirkActivative()
    {
        if (quirksSliders.smokeSlider.value == quirksSliders.smokeSlider.maxValue && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.SBtnPressed())
        {
            quirksSliders.smokeSlider.value = 0f;
            playerAnimatingConditions.canEmmitSmoke = true;
            
            playerAnimatingConditions.isUsingSmokeQuirk = true;
            soundEffects.PlayAnimSound(6);
            StartCoroutine(SmokeBeingReleased());
            StartCoroutine(CanReEmmitSmoke());
            StartCoroutine(SmokeQuirkDuration());
        }
        playerLocation.SightUnClear(!playerAnimatingConditions.isUsingSmokeQuirk);
    }
    IEnumerator SmokeBeingReleased()
    {
        yield return new WaitForSeconds(1.5f);
        smokeQuirk.Play();
        playerAnimatingConditions.canEmmitSmoke = false;
    }
    IEnumerator CanReEmmitSmoke()
    {
        while(true)
        {
            quirksSliders.smokeSlider.value += quirksRateChange.smokeScreenRate * Time.deltaTime;
            yield return null;
            if(quirksSliders.smokeSlider.value == quirksSliders.smokeSlider.maxValue)
            {
                playerAnimatingConditions.canEmmitSmoke = true;
                Debug.Log(playerAnimatingConditions.canEmmitSmoke);
                break;
            }
        }
    }
    IEnumerator SmokeQuirkDuration()
    {
        yield return new WaitForSeconds(7f);
        playerAnimatingConditions.isUsingSmokeQuirk = false;
        smokeQuirk.Stop();
    }
}
