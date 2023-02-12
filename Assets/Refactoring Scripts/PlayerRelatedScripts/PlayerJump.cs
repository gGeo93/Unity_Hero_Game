using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Transform dekusRealTransform;
    [SerializeField]QuirksSliders quirksSliders;
    CharacterController cc;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    PhysicalConditions physicalConditions;
    float rateOfChange = -0.1f;
    
    void Awake() 
    {
        cc = GetComponent<CharacterController>();
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
            if (Mathf.Abs(dekusRealTransform.position.y) <= physicalConditions.AirLimit)
            {
                playerAnimatingConditions.isWalking=false;
                playerAnimatingConditions.isRunning=false;
                physicalConditions.gravity += physicalConditions.jumpingAcceleration * Time.deltaTime;
                playerAnimatingConditions.cc.Move(Vector3.up * physicalConditions.gravity);                
                
                if(!playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isKicking && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isPoweringUp)
                    playerAnimatingConditions.IsIdleOrFloating();
            }
            if(rateOfChange < 0 && Input.GetKey(KeyCode.B))
            {
                physicalConditions.AirLimit = 10f;
                particleForces.BlackWhipApplied();
                rateOfChange = -0.1f;
                GameManager.Instance.quirksSlidersFunctionality.QuirkEndurance(quirksSliders.blackWhipSlider,ref rateOfChange, 0.2f);
            }
        }
        else if(!Input.GetKey(KeyCode.Space) && rateOfChange > 0)
        {
            particleForces.BlackWhipStopped();
            rateOfChange = 0.1f;
            GameManager.Instance.quirksSlidersFunctionality.QuirkRefill(quirksSliders.blackWhipSlider,ref rateOfChange, -0.1f);
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
