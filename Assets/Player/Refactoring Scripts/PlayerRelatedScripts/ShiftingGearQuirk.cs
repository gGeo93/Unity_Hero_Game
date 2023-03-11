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
        else if(Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();

            EndOrDisruptShiftingGeras();

            physicalConditions.RepairingGravity();
        }
        else if(playerAnimatingConditions.canGoShiftingSpeed)
        {
            quirksSlidersFunctionality.QuirkRefill(quirksSliders.shiftingGearsSlider, 0.025f, quirksRateChange.shiftingGearRate);
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
                OneForAllAttackChoice(shiftingTimes);
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
    private void OneForAllAttackChoice(int shiftingGearIndex)
    {
        switch(shiftingGearIndex)
        {
            case 1: playerAnimatingConditions.isFingering = true; 
                    playerAnimatingConditions.isSmashing = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.120f;
                    break;
            case 2: playerAnimatingConditions.isFingering = true; 
                    playerAnimatingConditions.isSmashing = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 3: playerAnimatingConditions.isKicking = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isSmashing = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 4: playerAnimatingConditions.isKicking = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isSmashing = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 5: playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 6: playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false;
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 7: playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            case 8: playerAnimatingConditions.isSmashing = true; 
                    playerAnimatingConditions.isFingering = playerAnimatingConditions.isKicking = false; 
                    playerStats.GettingDamage(10);
                    quirksSliders.shiftingGearsSlider.value -= 0.125f;
                    break;
            default: Debug.Log("Something went wrong"); break;
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
