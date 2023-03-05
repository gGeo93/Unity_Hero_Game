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
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    
    void Awake() 
    {
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        physicalConditions = GetComponent<PhysicalConditions>();    
    }
    public void UsingFloatQuirk()
    {
        if (!playerAnimatingConditions.isDead 
        && !playerAnimatingConditions.isSweepFalling
        &&  AirFloatQuirksState()
        && playerAnimatingConditions.canGoShiftingSpeed
        )        
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
                playerAnimatingConditions.isUsingBlackWhipForAttack = true;
            }
            if (!playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isKicking && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isUsingBlackWhipForAttack)
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
    private bool AirFloatQuirksState()
    {
        if(Input.GetKey(KeyCode.F) && quirksRateChange.airFloatingRate < 0)
        {
            quirksRateChange.airFloatingRate = quirksSlidersFunctionality.QuirkEndurance(quirksSliders.airFloatSlider, quirksRateChange.airFloatingRate, 0.05f);
            return true;
        }
        else if(!Input.GetKey(KeyCode.F) && quirksRateChange.airFloatingRate > 0)
        {
            quirksRateChange.airFloatingRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.airFloatSlider, quirksRateChange.airFloatingRate, -0.05f);
            return false;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            quirksRateChange.airFloatingRate = 0.05f;
        }
        return false;    
    }
}
