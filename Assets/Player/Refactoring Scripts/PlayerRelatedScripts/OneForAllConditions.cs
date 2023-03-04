using System.Collections;
using UnityEngine;

public class OneForAllConditions : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    PhysicalConditions physicalConditions;
    IdleOrFloatingCondition idleOrFloatingCondition;
    PlayerStats playerStats;
    ParticleForces particleForces;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    OneForAllSoundEffects oneForAllSoundEffects;
    
    void Awake() 
    {
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksSlidersFunctionality = GameManager.Instance.UITransform.GetComponent<QuirksSlidersFunctionality>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        particleForces = GetComponent<ParticleForces>();
        physicalConditions = GetComponent<PhysicalConditions>();
        playerStats = GetComponent<PlayerStats>();
        idleOrFloatingCondition = GetComponent<IdleOrFloatingCondition>();
    }
    public void OneforallDifferentUses()
    {
        if(!playerAnimatingConditions.canUseOneforAll)
        {
            OFAPowerReset();
        }
        if (Input.GetKeyDown(KeyCode.Q) && quirksSliders.handsAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isSmashing = true;
            if(playerAnimatingConditions.canUseOneforAll)
                quirksSliders.handsAttackSlider.value = playerStats.OFAImgBar.rectTransform.transform.localScale.x * 0.01f;
            else
                quirksSlidersFunctionality.QuirkEndurance(quirksSliders.handsAttackSlider, 20);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && quirksSliders.legAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isKicking = true;
            if(playerAnimatingConditions.canUseOneforAll)
                quirksSliders.legAttackSlider.value = playerStats.OFAImgBar.rectTransform.transform.localScale.x * 0.01f;
            else
               quirksSlidersFunctionality.QuirkEndurance(quirksSliders.legAttackSlider, 15);
        }
        else if (Input.GetKeyDown(KeyCode.X) && quirksSliders.fingersAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isFingering = true;
            if(playerAnimatingConditions.canUseOneforAll)
                quirksSliders.fingersAttackSlider.value = playerStats.OFAImgBar.rectTransform.transform.localScale.x * 0.01f;
            else
                quirksSlidersFunctionality.QuirkEndurance(quirksSliders.fingersAttackSlider, 10);
        }
        else if (Input.GetKey(KeyCode.R) && !playerAnimatingConditions.canUseOneforAll && !playerAnimatingConditions.isUsingFaJin && playerStats.MpImgBar.rectTransform.transform.localScale.x >= (1.0f - 0.1f) && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind && !Input.GetKey(KeyCode.W))
        {
            if(playerStats.OFAImgBar.rectTransform.transform.localScale.x >= 100)
            {
                playerStats.OFAImgBar.rectTransform.transform.localScale = Vector3.one;
                return;
            }   
            playerAnimatingConditions.isPoweringUp = true;
            physicalConditions.ZeroGravity();
            playerStats.FixAllBars();
            HpLoss();
        }
        else if(Input.GetKeyUp(KeyCode.R) && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind)
        {
            playerStats.PoweringUpConsequences();
        }
    }

    private void OFAPowerReset()
    {
        particleForces.punchDamage = 20;
        particleForces.ShootStyleDamage = 30;
        particleForces.fingersDamage = 20;
    }
    private void HpLoss()
    {
        float healthRemaining = playerStats.HpImgBar.rectTransform.localScale.x;
        playerStats.HpImgBar.rectTransform.transform.localScale -= Vector3.right * 0.1f * Time.deltaTime;
    }

    public void CannotUseOneForAll()
    {
        if (playerAnimatingConditions.isPoweringUp)
            playerAnimatingConditions.canUseOneforAll = false;
    }
    public void StoringEnergyToBodyParts()//enable false
    {
        if(playerAnimatingConditions.isUsingFaJin)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                quirksSlidersFunctionality.QuirkRefill(quirksSliders.handsAttackSlider, ref quirksRateChange.detroitSmashRate, -1);
                particleForces.detroitSmashConcentration[0].enabled = true;
                particleForces.detroitSmashConcentration[1].enabled = true;
            }
            else if(Input.GetKey(KeyCode.Z))
            {
                particleForces.shootStyleConcentration.enabled = true;
                quirksSlidersFunctionality.QuirkRefill(quirksSliders.legAttackSlider, ref quirksRateChange.shootStyleRate, -1);
            }
            else if(Input.GetKey(KeyCode.X))
            {
                particleForces.fingerSmashConcentration.enabled = true;
                quirksSlidersFunctionality.QuirkRefill(quirksSliders.fingersAttackSlider, ref quirksRateChange.fingerSmashRate, -1);
            }
        }
    }
    public void OFAVoiceSoundEffect(int index)//Triggered By Animation Event
    {
        oneForAllSoundEffects.PlayAnimSound(index);
    }
}
