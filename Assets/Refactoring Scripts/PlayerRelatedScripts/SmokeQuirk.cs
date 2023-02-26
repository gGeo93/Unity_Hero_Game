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
        if (quirksSliders.smokeSlider.value == quirksSliders.smokeSlider.maxValue && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.cannotEmmitSmoke && Input.GetKeyDown(KeyCode.S))
        {
            quirksSliders.smokeSlider.value = 0f;
            playerAnimatingConditions.isUsingSmokeQuirk = true;
            playerAnimatingConditions.cannotEmmitSmoke = true;
            soundEffects.PlayAnimSound(6);
            StartCoroutine(CanReEmmitSmoke());
            StartCoroutine(SmokeQuirkDuration());
        }
        playerLocation.SightUnClear(!playerAnimatingConditions.isUsingSmokeQuirk);
    }
    public void SmokeBeingReleased()
    {
        smokeQuirk.Play();
    }
    IEnumerator CanReEmmitSmoke()
    {
        while(true)
        {
            quirksSliders.smokeSlider.value += quirksRateChange.smokeScreenRate * Time.deltaTime;
            yield return null;
            if(quirksSliders.smokeSlider.value == quirksSliders.smokeSlider.maxValue)
            {
                playerAnimatingConditions.cannotEmmitSmoke = false;
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
