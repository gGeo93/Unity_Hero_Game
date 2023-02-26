using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingAfterJump : MonoBehaviour
{
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    CharacterController cc;
    private float groundChecker;
    private const float bufferCheckDistance = 0.1f;

    void Awake() 
    {
        cc = GetComponent<CharacterController>();
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();    
    }

    public void Landing()
    {
        groundChecker = cc.height / 2f + bufferCheckDistance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, groundChecker) && !Input.GetKey(KeyCode.W) && physicalConditions.gravity < 0)
        {
            playerAnimatingConditions.isWalking = false;
            playerAnimatingConditions.isRunning = false;
            playerAnimatingConditions.isFloating = false;
            playerAnimatingConditions.isLanding = true;
        }
        else
        {
            playerAnimatingConditions.isLanding = false;
        }
    }
}
