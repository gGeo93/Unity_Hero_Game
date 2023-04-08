using System.Linq;
using System.Collections;
using UnityEngine;

public class ShiftingGearQuirk : MonoBehaviour
{
    CharacterController cc;
    AnimatorMainFunctionality animatorMainFunctionality;
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    PlayerStats playerStats;
    ParticleForces particleForces;
    OneForAllSoundEffects oneForAllSoundEffects;
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    [SerializeField]DronesMainControllerCenter droneSpawner;
    [SerializeField] GameObject transmitionFog;
    [SerializeField] Transform dekus_RealPosition;
    void Awake() 
    {
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        cc = GetComponent<CharacterController>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        physicalConditions = GetComponent<PhysicalConditions>();
        particleForces = GetComponent<ParticleForces>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        playerStats = GetComponent<PlayerStats>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
    }
    public void StartShiftingGear()
    {
        if(Input.GetKeyDown(KeyCode.Return) && quirksSliders.shiftingGearsSlider.value == 1 && !playerAnimatingConditions.isDead &&!playerAnimatingConditions.isSmashing && !playerAnimatingConditions.isFingering && !playerAnimatingConditions.isKicking && playerAnimatingConditions.canGoShiftingSpeed)
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
        else if(Input.GetKeyDown(KeyCode.E) && !playerAnimatingConditions.canGoShiftingSpeed)
        {
            StopAllCoroutines();

            EndOrDisruptShiftingGeras();

            physicalConditions.RepairingGravity();
        }
        else if(playerAnimatingConditions.canGoShiftingSpeed)
        {
            quirksSlidersFunctionality.QuirkRefill(quirksSliders.shiftingGearsSlider, 0.0175f, quirksRateChange.shiftingGearRate);
        }
    }

    IEnumerator ShiftingSpeed()
    {
        int shiftingTimes = 1;
        float distanceFromDrone = 2.5f;
        DroneController closestToHeroDrone;
        while(shiftingTimes <= 8)
        {
            animatorMainFunctionality.AnimationState(nameof(PlayerAnimationState.ShiftSpeed));

            ShiftingGearVoice(shiftingTimes);

            yield return new WaitForSeconds(1.5f);

            transmitionFog.SetActive(true);
            
            cc.enabled = false;

            closestToHeroDrone = ClosestEnemy();
            
            distanceFromDrone = InstantTransmition(distanceFromDrone, closestToHeroDrone);

            cc.enabled = true;

            if (closestToHeroDrone != null)
                OneForAllAttackChoice(ref shiftingTimes);
            else
                oneForAllSoundEffects.PlayAnimSound(13);

            yield return new WaitForSeconds(2.5f);

            closestToHeroDrone = ClosestEnemy();

            if (closestToHeroDrone == null) break;

            shiftingTimes += 1;
        }

        EndOrDisruptShiftingGeras();
    }

    private float InstantTransmition(float distanceFromDrone, DroneController closestToHeroDrone)
    {
        if (closestToHeroDrone != null)
        {
            transform.position = new Vector3(closestToHeroDrone.transform.position.x, closestToHeroDrone.transform.position.y, closestToHeroDrone.transform.position.z - distanceFromDrone);
            distanceFromDrone = distanceFromDrone > 0 ? -2.5f : 2.5f;
            transform.LookAt(closestToHeroDrone.transform.position);
        }

        return distanceFromDrone;
    }

    private DroneController ClosestEnemy()
    {
        DroneController closestToHeroDrone;
        closestToHeroDrone = droneSpawner.activeDrones
                    .OrderBy(d => (d.transform.position - dekus_RealPosition.position).magnitude)
                    .FirstOrDefault(/*d => 
            (d.transform.position - dekus_RealPosition.position).magnitude <= 
            droneSpawner.explosionRadius + extraDistance*/);
        return closestToHeroDrone;
    }
    private void EndOrDisruptShiftingGeras()
    {
        Debug.Log("End Or Disruption Shifting Gears");
        transmitionFog.SetActive(false);
        physicalConditions.RepairingGravity();
        cc.Move(-Vector3.up * Time.deltaTime * 1f);
        playerAnimatingConditions.canGoShiftingSpeed = true;
        playerStats.MpImgBar.rectTransform.transform.localScale = new Vector3(0, 1, 1);
        playerAnimatingConditions.isPoweringUp = false;
    }
    private void OneForAllAttackChoice(ref int shiftingGearIndex)
    {
        switch(shiftingGearIndex)
        {
            case 1:
                    if(quirksSliders.fingersAttackSlider.value - particleForces.fingersDamage <= 0)
                    {
                        shiftingGearIndex = 3;
                        break;
                    }    
                    playerAnimatingConditions.isFingering = true; 
                    playerAnimatingConditions.isSmashing = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.120f;
                    break;
            case 2: if(quirksSliders.fingersAttackSlider.value - particleForces.fingersDamage <= 0)
                        break;
                    playerAnimatingConditions.isFingering = true; 
                    playerAnimatingConditions.isSmashing = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 3: if(quirksSliders.legAttackSlider.value - particleForces.ShootStyleDamage <= 0)
                    {
                        shiftingGearIndex = 5;
                        break;
                    }    
                    playerAnimatingConditions.isKicking = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isSmashing = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 4: if(quirksSliders.legAttackSlider.value - particleForces.ShootStyleDamage <= 0)
                        break;    
                    playerAnimatingConditions.isKicking = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isSmashing = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 5: if(quirksSliders.handsAttackSlider.value - particleForces.punchDamage <= 0)
                    {
                        shiftingGearIndex = 9;   
                        break;
                    }
                    playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 6: if(quirksSliders.handsAttackSlider.value - particleForces.punchDamage <= 0)
                        break;
                    playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false;
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 7: if(quirksSliders.handsAttackSlider.value - particleForces.punchDamage <= 0)
                        break;
                    playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 8: if(quirksSliders.handsAttackSlider.value - particleForces.punchDamage <= 0)
                        break; 
                    playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            default: shiftingGearIndex = 9; Debug.Log("Not enough resources"); break;
        }
    }

    private void ShiftingGearVoice(int shiftingTimes)
    {
        switch(shiftingTimes)
        {
            case 1: oneForAllSoundEffects.PlayAnimSound(9); break;
            case 3: oneForAllSoundEffects.PlayAnimSound(10); break;
            case 5: oneForAllSoundEffects.PlayAnimSound(11); break;
            case 8: oneForAllSoundEffects.PlayAnimSound(12); break;
            default: break;
        }
    }
}
