using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerSenseQuirk : MonoBehaviour
{
    [SerializeField]GameObject dangerSense;
    [SerializeField]DronesMainControllerCenter droneSpawner;
    [SerializeField]Transform DekusRealPosition;
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    CharacterController cc;
    OneForAllSoundEffects oneForAllSoundEffects;
    PlayerAnimatingConditions playerAnimatingConditions;
    float elapsedTime;
    void Awake()
    {
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        DekusRealPosition = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
    }
    void Start() 
    {
        StartCoroutine(CheckForDronesRange());    
    }
    public void DodgingLaserBeamCondition()
    {
        playerAnimatingConditions.canDodgeWithDangerSense = DangerSenseQuirksState();
    }
    public void DodgingLaserBeamMoving()
    {
        if(playerAnimatingConditions.canDodgeWithDangerSense)
        {
            dangerSense.SetActive(true);
            DangerSenseInnerVoice();
        }
    }
    private bool DangerSenseQuirksState()
    {
        if(Input.GetKey(KeyCode.D) && quirksRateChange.dangerSenseRate < 0)
        {
            quirksRateChange.dangerSenseRate = quirksSlidersFunctionality.QuirkEndurance(quirksSliders.dangerSenseSlider, quirksRateChange.dangerSenseRate, 0.175f);
            return true;
        }
        else if(!Input.GetKey(KeyCode.D) && quirksRateChange.dangerSenseRate > 0)
        {
            quirksRateChange.dangerSenseRate = quirksSlidersFunctionality.QuirkRefill(quirksSliders.dangerSenseSlider, quirksRateChange.dangerSenseRate, -0.25f);
            return false;
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            quirksRateChange.dangerSenseRate = 0.175f;
        }
        return false;    
    }

    private IEnumerator CheckForDronesRange()
    {
        while(true)
        {
            yield return refreshRate;
            if(droneSpawner.activeDrones.Count > 0 && IsNearDanger())
            {
                dangerSense.gameObject.SetActive(true);
                DangerSenseVoiceFrequency();
            }
            else
            {
                if(!playerAnimatingConditions || !IsNearDanger())
                {
                    yield return new WaitForSeconds(1f);
                    dangerSense.gameObject.SetActive(false);
                }
                elapsedTime = 0f;
            }
        }
    }
    WaitForSeconds refreshRate = null;
    private bool IsNearDanger()
    {
        for (int i = 0; i < droneSpawner.activeDrones.Count; i++)
        {
            if((droneSpawner.activeDrones[i].GetComponent<DroneAttacks>().IsAboutToExplode && (transform.position - droneSpawner.activeDrones[i].transform.position).magnitude <= droneSpawner.explosionRadius))            
                return true;   
        }
        return false;
    }
    private void DangerSenseVoiceFrequency()
    {
        if (elapsedTime == 0f)
            DangerSenseInnerVoice();
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 2.5f)
            elapsedTime = 0f;
    }

    private void DangerSenseInnerVoice()
    {
        oneForAllSoundEffects.PlayAnimSound(5);
    }
}
