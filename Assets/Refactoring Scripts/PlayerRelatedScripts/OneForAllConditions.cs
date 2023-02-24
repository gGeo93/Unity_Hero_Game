using System.Collections;
using UnityEngine;

public class OneForAllConditions : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    PhysicalConditions physicalConditions;
    PlayerStats playerStats;
    ParticleForces particleForces;
    QuirksRateChange quirksRateChange;
    QuirksSliders quirksSliders;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    
    void Awake() 
    {
        quirksRateChange = GameManager.Instance.quirksRateChange;
        quirksSliders = GameManager.Instance.quirksSliders;
        quirksSlidersFunctionality = GameManager.Instance.quirksSlidersFunctionality;
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        particleForces = GetComponent<ParticleForces>();
        physicalConditions = GetComponent<PhysicalConditions>();
        playerStats = GetComponent<PlayerStats>();
    }
    public void OneforallDifferentUses()
    {
        if (Input.GetKeyDown(KeyCode.Q) && quirksSliders.handsAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isSmashing = true;
            quirksSlidersFunctionality.QuirkEndurance(quirksSliders.handsAttackSlider, 20);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && quirksSliders.legAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isKicking = true;
            quirksSlidersFunctionality.QuirkEndurance(quirksSliders.legAttackSlider, 15);
        }
        else if (Input.GetKeyDown(KeyCode.X) && quirksSliders.fingersAttackSlider.value > 0 && !playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && !playerAnimatingConditions.isTurningBehind)
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isFingering = true;
            quirksSlidersFunctionality.QuirkEndurance(quirksSliders.fingersAttackSlider, 10);
        }
        else if (Input.GetKeyDown(KeyCode.R) && !playerAnimatingConditions.isUsingFaJin && playerStats.MpImgBar.rectTransform.transform.localScale.x >= (1.0f - 0.1f) && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind && !Input.GetKey(KeyCode.W))
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isPoweringUp = true;
            StartCoroutine(PoweringUpConsequences(2f));
        }
    }
    private IEnumerator PoweringUpConsequences(float powerUpDurationInSeconds)
    {
        yield return new WaitForSeconds(powerUpDurationInSeconds);
        if(playerAnimatingConditions.isPoweringUp)
        {
            playerAnimatingConditions.isPoweringUp = false;
            Debug.Log("OFA 100%");
            //All OFA slider bars go up to 100 (+)
            OFAMaxSliderValue();
            //All OFA attacks +100 (+)
            OFAPower(50);
            //All Quirks' SlideBars rates go higher (+)
            OFAQuirksRatesRise();
            //Mp bar(fatigue) loss rate go greatly higher (-)
            MpLossRateRise();
            //Maybe and a part of Hp bar(helth) lowers (-)
            HpLoss();
        }
    }

    private void MpLossRateRise()
    {
        playerStats.SetFatigueLossRate(0.01f);
        playerStats.SetRegainingStrengthRate(0.05f);
    }

    private void HpLoss()
    {
        float healthRemaining = playerStats.HpImgBar.rectTransform.localScale.x;
        float amountOfDamage = healthRemaining > 0.55f ? 50f : 10f;
        playerStats.GettingDamage(amountOfDamage);
    }

    private void OFAQuirksRatesRise()
    {
        quirksRateChange.detroitSmashRate += 1;
        quirksRateChange.shootStyleRate += 1;
        quirksRateChange.fingerSmashRate += 1;
    }

    private void OFAPower(int powerParam)
    {
        particleForces.punchDamage += powerParam;
        particleForces.ShootStyleDamage += powerParam;
        particleForces.fingersDamage += powerParam;
    }

    private void OFAMaxSliderValue()
    {
        quirksSliders.handsAttackSlider.value = 100;
        quirksSliders.legAttackSlider.value = 100;
        quirksSliders.fingersAttackSlider.value = 100;
    }

    public void CannotUseOneForAll()
    {
        if (playerAnimatingConditions.isPoweringUp)
            playerAnimatingConditions.canUseOneforAll = false;
    }
    public void StoringEnergyToBodyParts()
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
}
