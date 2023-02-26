using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftingGearQuirk : MonoBehaviour
{
    CharacterController cc;
    AnimatorMainFunctionality animatorMainFunctionality;
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    PlayerStats playerStats;
    OneForAllSoundEffects oneForAllSoundEffects;
    [SerializeField]DronesMainControllerCenter droneSpawner;
    [SerializeField] GameObject transmitionFog;
    [SerializeField] Transform dekus_RealPosition;
    void Awake() 
    {
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        cc = GetComponent<CharacterController>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        playerStats = GetComponent<PlayerStats>();
    }
    public void StartShiftingGear()
    {
        if(playerAnimatingConditions.canUseOneforAll && !playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isKicking && Input.GetKeyDown(KeyCode.Return) && playerAnimatingConditions.canGoShiftingSpeed)
        {
            if(playerStats.MpImgBar.rectTransform.transform.localScale.x >= 0.75f)
            {
                playerAnimatingConditions.canGoShiftingSpeed = false;
                StartCoroutine(ShiftingSpeed());
            }
            else
            {
                oneForAllSoundEffects.PlayAnimSound(14);
            }
        }
    }
    IEnumerator ShiftingSpeed()
    {
        int shiftingTimes = 1;
        float distanceFromDrone = 3f;
        while(shiftingTimes <= 4)
        {
            if (Input.GetKeyDown(KeyCode.Space)) break;
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.ShiftSpeed));
            ShiftingGearVoice(shiftingTimes);
            yield return new WaitForSeconds(1.5f);
            if (Input.GetKeyDown(KeyCode.Space)) break;
            transmitionFog.SetActive(true);
            cc.enabled = false;
            DroneController closestToHeroDrone = droneSpawner.activeDrones.OrderBy(d => (d.transform.position - dekus_RealPosition.position).magnitude).FirstOrDefault(d => (d.transform.position - dekus_RealPosition.position).magnitude <= droneSpawner.explosionRadius);
            if (closestToHeroDrone != null)
            {
                transform.position = new Vector3(closestToHeroDrone.transform.position.x, closestToHeroDrone.transform.position.y, closestToHeroDrone.transform.position.z - distanceFromDrone);
                distanceFromDrone = distanceFromDrone > 0 ? -2.5f : 2.5f;
                transform.LookAt(closestToHeroDrone.transform.position);
            }
            cc.enabled = true;
            if (closestToHeroDrone != null) OneForAllAttackChoice(shiftingTimes);
            else oneForAllSoundEffects.PlayAnimSound(13);
            yield return new WaitForSeconds(2.5f);
            if (closestToHeroDrone == null) break;
            if (Input.GetKeyDown(KeyCode.Space)) break;
            shiftingTimes += 1;
        }
        Debug.Log("breaking the loop");
        transmitionFog.SetActive(false);
        physicalConditions.RepairingGravity();
        cc.Move(-Vector3.up * Time.deltaTime * 1f);
        playerAnimatingConditions.canGoShiftingSpeed = true;
        playerStats.MpImgBar.rectTransform.transform.localScale = new Vector3(0,1,1);
        playerAnimatingConditions.isPoweringUp = false;
    }

    private void OneForAllAttackChoice(int shiftingGearIndex)
    {
        switch(shiftingGearIndex)
        {
            case 1: playerAnimatingConditions.isFingering = true; playerAnimatingConditions.isSmashing = playerAnimatingConditions.isKicking = false; break;
            case 2: playerAnimatingConditions.isKicking = true; playerAnimatingConditions.isFingering = playerAnimatingConditions.isSmashing = false; break;
            case 3: playerAnimatingConditions.isSmashing = true; playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; break;
            case 4: playerAnimatingConditions.isSmashing = true; playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; break;
            default: Debug.Log("Something went wrong"); break;
        }
    }

    private void ShiftingGearVoice(int shiftingTimes)
    {
        switch(shiftingTimes)
        {
            case 1: oneForAllSoundEffects.PlayAnimSound(9); break;
            case 2: oneForAllSoundEffects.PlayAnimSound(10); break;
            case 3: oneForAllSoundEffects.PlayAnimSound(11); break;
            case 4: oneForAllSoundEffects.PlayAnimSound(12); break;
            default: break;
        }
    }
}
