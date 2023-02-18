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
    CharacterController cc;
    OneForAllSoundEffects oneForAllSoundEffects;
    PlayerAnimatingConditions playerAnimatingConditions;
    float elapsedTime;
    void Awake()
    {
        quirksSliders = GameManager.Instance.quirksSliders;
        quirksRateChange = GameManager.Instance.quirksRateChange;
        DekusRealPosition = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        oneForAllSoundEffects = GetComponent<OneForAllSoundEffects>();
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
            Dodging();
            GravityApply();
        }
    }
    private bool DangerSenseQuirksState()
    {
        if(Input.GetKey(KeyCode.D) && quirksRateChange.dangerSenseRate < 0)
        {
            GameManager.Instance.quirksSlidersFunctionality.QuirkEndurance(quirksSliders.dangerSenseSlider, ref quirksRateChange.dangerSenseRate, 0.175f);
            return true;
        }
        else if(!Input.GetKey(KeyCode.D) && quirksRateChange.dangerSenseRate > 0)
        {
            GameManager.Instance.quirksSlidersFunctionality.QuirkRefill(quirksSliders.dangerSenseSlider, ref quirksRateChange.dangerSenseRate, -0.25f);
            return false;
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            quirksRateChange.dangerSenseRate = 0.175f;
        }
        return false;    
    }
    private void GravityApply()
    {
        cc.Move(DekusRealPosition.forward * Time.deltaTime);
    }

    private void Dodging()
    {
        cc.enabled = false;
        DekusRealPosition.position = new Vector3(DekusRealPosition.localPosition.x, DekusRealPosition.localPosition.y + 2f, DekusRealPosition.localPosition.z + 1f);
        cc.enabled = true;
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
