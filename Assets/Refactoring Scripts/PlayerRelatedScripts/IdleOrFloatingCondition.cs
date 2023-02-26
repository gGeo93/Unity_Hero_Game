using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOrFloatingCondition : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();    
    }
    public void StayingIdleOrFloating()
    {
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftShift))
            && !Input.GetKey(KeyCode.Space)
            && !Input.GetKey(KeyCode.F)
            && !playerAnimatingConditions.isLanding
            && !playerAnimatingConditions.isUsingFaJin
            && !playerAnimatingConditions.isSweepFalling
            && !playerAnimatingConditions.isDead
            && playerAnimatingConditions.canGoShiftingSpeed)
        {
            playerAnimatingConditions.IsIdleOrFloating();
        }
    }
}
