using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public RawImage HpImgBar;
    public RawImage MpImgBar;
    public RawImage OFAImgBar;
    [SerializeField] TextMeshProUGUI powerUpPerCent;
    private float healingFactor = 0.025f;
    private float fatigueLossRate = 0.01f;
    private float OFAbarLosssRate = 0.01f;
    private float regainingStrengthRate = 0.1f;
    private int perCent = 0;
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    OneForAllSoundEffects oneForAllSoundEffects;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    PlayerStats playerStats;
    ElectricityScript electricityScript;
    DyingCase dyingCase;
    bool quirksRateChangeAreRising = true;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        electricityScript = GetComponent<ElectricityScript>();
        dyingCase = GetComponent<DyingCase>();
        particleForces = GetComponent<ParticleForces>();
        playerStats = GetComponent<PlayerStats>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
        oneForAllSoundEffects = GameManager.Instance.AudioManipulator.GetComponent<OneForAllSoundEffects>();
    }
    void Start() 
    {
        OFAImgBar.rectTransform.transform.localScale = new Vector3(0, 1, 1);  
    }
    public void PoweringUp()
    {
        if(playerAnimatingConditions.isPoweringUp)
        {
            powerUpPerCent.gameObject.SetActive(true);
            if(perCent < 100 && playerStats.MpImgBar.rectTransform.transform.localScale.x >= 0.70f)
            {
                playerAnimatingConditions.canUseOneforAll = true;
                OFAImgBar.rectTransform.transform.localScale += new Vector3(0.25f, 0, 0) * Time.deltaTime;
                perCent = Mathf.FloorToInt(OFAImgBar.rectTransform.transform.localScale.x * 100);
                powerUpPerCent.text = perCent + "%";
                PoweringUpConsequences();
            }
        }
        else
        {
            powerUpPerCent.text = Mathf.FloorToInt(OFAImgBar.rectTransform.transform.localScale.x * 100) + "%";
            perCent = Mathf.FloorToInt(OFAImgBar.rectTransform.transform.localScale.x * 100);
            OFABarLoss();
            FixAllBars();
        }
    }
    private void PoweringUpConsequences()
    {
        playerAnimatingConditions.canUseOneforAll = true;
        //All OFA slider bars go up to 100 (+)
        OFAMaxSliderValue();
        //All OFA attacks +100 (+)
        OFAPower();
        //All Quirks' SlideBars rates go higher (+)
        OFAQuirksRatesRise();
        //Mp bar(fatigue) loss rate go greatly higher (-)
        MpLossRateRise();
        //Maybe and a part of Hp bar(helth) lowers (-)
        HpFalling();
        //OFA 100%
        OneHundredPerCent();
    }
    
    public void GettingDamage(float amountOfDamage)
    {
        HpImgBar.rectTransform.localScale -= Vector3.right * amountOfDamage/100.0f;
        FixAllBars();
        if(HpImgBar.rectTransform.localScale.x <= 0)
        {
            dyingCase.MainCaseOfDying();
        }
    }
    private void HpFalling()
    {
        HpImgBar.rectTransform.transform.localScale -= Vector3.right * 0.1f * Time.deltaTime;
    }

    private void OFABarLoss()
    {
        OFAImgBar.rectTransform.localScale -= Vector3.right * OFAbarLosssRate * Time.deltaTime;
        perCent = Mathf.FloorToInt(OFAImgBar.rectTransform.localScale.x * 100);
    }
    public void HealingProcess()
    {
        if(HpImgBar.rectTransform.localScale.x >= 1f)
        {
            HpImgBar.rectTransform.localScale = Vector3.one;
        }
        else if(HpImgBar.rectTransform.localScale.x < 1f)
        {
            HpImgBar.rectTransform.localScale += Vector3.right * healingFactor/100f;
        }
    }
    public void FatigueLoss()
    {
        if (playerAnimatingConditions.canUseOneforAll && playerAnimatingConditions.canGoShiftingSpeed)
        {
            StartCoroutine(GettingTired());
        }
        else if (!playerAnimatingConditions.canUseOneforAll)
        {
            StartCoroutine(RegainingStrength());
        }
    }
    public void SetFatigueLossRate(float fatigueLossRate)
    {
        this.fatigueLossRate = fatigueLossRate;
    }
    public void SetRegainingStrengthRate(float regainingStrengthRate)
    {
        this.regainingStrengthRate = regainingStrengthRate;
    }
    private void MpLossRateRise()
    {
        SetFatigueLossRate(0.01f);
        SetRegainingStrengthRate(0.05f);
    }
    private void OneHundredPerCent()
    {
        if (perCent >= 100)
        {
            oneForAllSoundEffects.PlayAnimSound(15);
        }
    }
    private void OFAQuirksRatesRise()
    {
        if(quirksRateChangeAreRising)
        {
            quirksRateChangeAreRising = false;
            
            quirksRateChange.detroitSmashRate += 1;
            quirksRateChange.shootStyleRate += 1;
            quirksRateChange.fingerSmashRate += 1;
        }
    }

    private void OFAPower()
    {
        particleForces.punchDamage += 1;
        particleForces.ShootStyleDamage += 1;
        particleForces.fingersDamage += 1;
    }

    private void OFAMaxSliderValue()
    {
        quirksSliders.handsAttackSlider.value += perCent * Time.deltaTime;
        quirksSliders.legAttackSlider.value += perCent * Time.deltaTime;
        quirksSliders.fingersAttackSlider.value += perCent * Time.deltaTime;
    }
    private IEnumerator GettingTired()
    {
        if(playerAnimatingConditions.canUseOneforAll)
        {
            while(MpImgBar.rectTransform.transform.localScale.x > 0)
            {
                if(!PauseMenu.theGameIsPaused)
                {
                    MpImgBar.rectTransform.localScale -= Vector3.right * fatigueLossRate * Time.deltaTime;
                }
                yield return new WaitForSeconds(3.0f);
            }
            FixAllBars();
            electricityScript.ElectricityOff();
        }
    }
    private IEnumerator RegainingStrength()
    {
        while(!playerAnimatingConditions.canUseOneforAll && MpImgBar.rectTransform.transform.localScale.x < 1)
        {
            MpImgBar.rectTransform.localScale += Vector3.right * regainingStrengthRate/100.0f;
            yield return new WaitForSeconds(1.0f);
        }
        electricityScript.isTired = false;
    }
    public void FixAllBars()
    {
        if(HpImgBar.rectTransform.transform.localScale.x < 0)
        {
            HpImgBar.rectTransform.transform.localScale= new Vector3(0f,1f,1f);
        }
        else if(HpImgBar.rectTransform.transform.localScale.x >= 1)
        {
            HpImgBar.rectTransform.transform.localScale= new Vector3(1f,1f,1f);
        }        
        if(MpImgBar.rectTransform.transform.localScale.x < 0)
        {
            MpImgBar.rectTransform.transform.localScale = new Vector3(0f,1f,1f);
        }
        else if(MpImgBar.rectTransform.transform.localScale.x >= 1)
        {
            MpImgBar.rectTransform.transform.localScale = new Vector3(1f,1f,1f);
        }
        if(OFAImgBar.rectTransform.transform.localScale.x < 0)
        {
            OFAImgBar.rectTransform.transform.localScale = new Vector3(0f,1f,1f);
        }    
        else if(OFAImgBar.rectTransform.transform.localScale.x >= 1 )
        {
            OFAImgBar.rectTransform.transform.localScale = new Vector3(1f,1f,1f);
        }    
    }
}
