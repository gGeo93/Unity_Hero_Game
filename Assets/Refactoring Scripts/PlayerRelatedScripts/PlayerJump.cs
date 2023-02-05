using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Transform dekusRealTransform;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    PhysicalConditions physicalConditions; 
    
    void Awake() 
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        particleForces = GetComponent<ParticleForces>();    
    }
    
    public void Jump()
    {
        if(!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && Input.GetKey(KeyCode.Space))
        {         
            if(playerAnimatingConditions.isPoweringUp || playerAnimatingConditions.isFingering || playerAnimatingConditions.isSmashing || playerAnimatingConditions.isKicking)
                physicalConditions.RepairingGravity();
            if (Mathf.Abs(dekusRealTransform.position.y) <= (int)physicalConditions.AirLimit)
            {
                playerAnimatingConditions.isWalking=false;
                playerAnimatingConditions.isRunning=false;
                physicalConditions.gravity += physicalConditions.jumpingAcceleration * Time.deltaTime;
                playerAnimatingConditions.cc.Move(Vector3.up * physicalConditions.gravity);                
                if(playerAnimatingConditions.canUseOneforAll)
                {
                    particleForces.BlackWhipApplied();
                }
                if(!playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isKicking && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isPoweringUp)
                    playerAnimatingConditions.IsIdleOrFloating();
            }    
        }
    }
    public void Fall()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            particleForces.BlackWhipStopped();
        }
    }
    public void Falling()
    {
        if(!playerAnimatingConditions.isAirFloating && !playerAnimatingConditions.cc.isGrounded && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isKicking && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isPoweringUp && !playerAnimatingConditions.isUsingBlackWhip)
        {
            PlayerFalling();
        }
    }
    private void PlayerFalling()
    {
        playerAnimatingConditions.isWalking=false;
        playerAnimatingConditions.isRunning=false;
        physicalConditions.gravity -= physicalConditions.fallingAcceleration * Time.deltaTime;
        playerAnimatingConditions.cc.Move(Vector3.up * physicalConditions.gravity );
        playerAnimatingConditions.IsIdleOrFloating();
        if ( playerAnimatingConditions.cc.isGrounded ) {physicalConditions.gravity = 0;}
    }
}
