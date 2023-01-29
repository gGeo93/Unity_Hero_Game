using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSenseQuirk : MonoBehaviour
{
    [SerializeField]GameObject dangerSense;
    [SerializeField]DronesMainControllerCenter droneSpawner;
    [SerializeField]Transform DekusRealPosition;
    CharacterController cc;
    OneForAllSoundEffects oneForAllSoundEffects;
    PlayerAnimatingConditions playerAnimatingConditions;
    float elapsedTime;
    void Start() 
    {
        DekusRealPosition = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        oneForAllSoundEffects = GetComponent<OneForAllSoundEffects>();
        StartCoroutine(CheckForDronesRange());    
    }
    public void DodgingLaserBeamCondition()
    {
        playerAnimatingConditions.canDodgeWithDangerSense = Input.GetKeyDown(KeyCode.D) ? true : false;
    }
    public void DodgingLaserBeamMoving()
    {
        DangerSenseInnerVoice();
        Dodging();
        GravityApply();
        StartCoroutine(DangerSenseVisualEffectDuration());
        playerAnimatingConditions.canDodgeWithDangerSense = false;
    }
    private IEnumerator DangerSenseVisualEffectDuration()
    {
        dangerSense.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dangerSense.gameObject.SetActive(false);
    }
    private void GravityApply()
    {
        cc.Move(DekusRealPosition.forward * 0.1f);
    }

    private void Dodging()
    {
        cc.enabled = false;
        DekusRealPosition.position = new Vector3(DekusRealPosition.localPosition.x, DekusRealPosition.localPosition.y + 3f, DekusRealPosition.localPosition.z);
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
