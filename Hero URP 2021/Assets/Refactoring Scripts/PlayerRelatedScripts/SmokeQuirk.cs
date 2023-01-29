using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    OneForAllSoundEffects soundEffects;
    PlayerLocation playerLocation;
    [SerializeField] ParticleSystem smokeQuirk;
    private void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        playerLocation = GetComponent<PlayerLocation>();
        soundEffects = GetComponent<OneForAllSoundEffects>();    
    }
    public void SmokeQuirkActivative()
    {
        if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.cannotEmmitSmoke && Input.GetKeyDown(KeyCode.S))
        {
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
        yield return new WaitForSeconds(5f);
        playerAnimatingConditions.cannotEmmitSmoke = false;
    }
    IEnumerator SmokeQuirkDuration()
    {
        yield return new WaitForSeconds(7f);
        playerAnimatingConditions.isUsingSmokeQuirk = false;
        smokeQuirk.Stop();
    }
}
