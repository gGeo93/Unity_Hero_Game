using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFloatQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    PhysicalConditions physicalConditions;
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] GameObject cam3;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        physicalConditions = GetComponent<PhysicalConditions>();    
    }
    public void UsingFloatQuirk()
    {
        if (!playerAnimatingConditions.isDead 
        && !playerAnimatingConditions.isSweepFalling 
        && playerAnimatingConditions.canUseOneforAll 
        && playerAnimatingConditions.canGoShiftingSpeed
        && Input.GetKey(KeyCode.F))
        {
            cam3.SetActive(true);
            cam1.SetActive(false);
            cam2.SetActive(false);
            playerAnimatingConditions.isFloating = false;
            playerAnimatingConditions.isWalking = false;
            playerAnimatingConditions.isRunning = false;
            physicalConditions.jumpingAcceleration = 0;
            physicalConditions.fallingAcceleration = 0;
            physicalConditions.gravity = 0;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerAnimatingConditions.isAirFloating = false;
                playerAnimatingConditions.isSmashing = true;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                playerAnimatingConditions.isAirFloating = false;
                playerAnimatingConditions.isKicking = true;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                playerAnimatingConditions.isAirFloating = false;
                playerAnimatingConditions.isFingering = true;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                playerAnimatingConditions.isAirFloating = false;
                playerAnimatingConditions.isUsingBlackWhip = true;
            }
            if (!playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isKicking && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isUsingBlackWhip)
                playerAnimatingConditions.isAirFloating = true;
        }
        else if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && Input.GetKeyUp(KeyCode.F))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            cam3.SetActive(false);
            playerAnimatingConditions.isAirFloating = false;
            physicalConditions.fallingAcceleration = 0.51f;
            physicalConditions.jumpingAcceleration = 1f;
            playerAnimatingConditions.IsIdleOrFloating();
        }
    }
}
