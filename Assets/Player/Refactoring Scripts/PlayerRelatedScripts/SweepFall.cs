using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepFall : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    public Vector3 sweepFallDirection;
    PhysicalConditions physicalConditions;
    void Awake() 
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
    }
    public void SweepFalling()
    {
        if (playerAnimatingConditions.isSweepFalling)
        {
            playerAnimatingConditions.isFloating = false;
            playerAnimatingConditions.isWalking = false;
            physicalConditions.jumpingAcceleration = 0;
            physicalConditions.fallingAcceleration = 0;
            physicalConditions.gravity = 0;

            playerAnimatingConditions.cc.Move(sweepFallDirection * 5f * Time.deltaTime);

            StartCoroutine(RegainConciousnessAfterExplosion());
        }
    }
    
    IEnumerator RegainConciousnessAfterExplosion()
    {
        yield return new WaitForSeconds(4f);
        physicalConditions.RepairingGravity();
    }  
}
