using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlaying : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    AnimatorMainFunctionality animatorMainFunctionality;
    SmokeQuirk smokeQuirk;
    DangerSenseQuirk dangerSenseQuirk;
    
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        smokeQuirk = GetComponent<SmokeQuirk>();
        dangerSenseQuirk = GetComponent<DangerSenseQuirk>();
    }
    public void AnimationPlayingNow()
    {
        if (playerAnimatingConditions.isWalking)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Walking));
            playerAnimatingConditions.isWalking=false;
        }
        if (playerAnimatingConditions.isRunning)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Running));
            playerAnimatingConditions.isRunning=false;
        }
        if (playerAnimatingConditions.isIdle)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Idle));
            playerAnimatingConditions.isIdle=false;
        }
        if (playerAnimatingConditions.isFloating)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Floating));
            playerAnimatingConditions.isFloating=false;
        }
        if(playerAnimatingConditions.isAirFloating)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Flying));
            playerAnimatingConditions.isAirFloating = false;
        }
        if (playerAnimatingConditions.isUsingSmokeQuirk)
        {
            smokeQuirk.SmokeBeingReleased();
        }
        if(playerAnimatingConditions.isTurningBehind)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.TurnBack));
        }
        if (playerAnimatingConditions.isPoweringUp)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.PowerUp));
        }
        else if (playerAnimatingConditions.isSmashing)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Smash));
        }
        else if (playerAnimatingConditions.isKicking)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.ShootStyle));
        }
        else if (playerAnimatingConditions.isFingering)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.FingerAttack));
        }
        else if(playerAnimatingConditions.isUsingBlackWhipForAttack)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.BlackWhip));
        }
        if(playerAnimatingConditions.isUsingFaJin)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Fa_Jin));
        }
        if(playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isDead)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.SweepFall));
            
            playerAnimatingConditions.isPoweringUp = false;
            playerAnimatingConditions.isSmashing = false;
            playerAnimatingConditions.isKicking = false;
            playerAnimatingConditions.isFingering = false;
            playerAnimatingConditions.isUsingBlackWhipForAttack = false;
        }
        if (playerAnimatingConditions.isDead)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.Dying));
        }
    }
}    

