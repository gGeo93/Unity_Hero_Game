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
    private float fatigueLossRate = 0.005f;
    private float regainingStrengthRate = 0.1f;
    QuirksSliders quirksSliders;
    QuirksRateChange quirksRateChange;
    PlayerAnimatingConditions playerAnimatingConditions;
    ParticleForces particleForces;
    ElectricityScript electricityScript;
    AnimatorMainFunctionality animatorMainFunctionality;
    DyingCase dyingCase;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        electricityScript = GetComponent<ElectricityScript>();
        dyingCase = GetComponent<DyingCase>();
        particleForces = GetComponent<ParticleForces>();
        quirksSliders = GameManager.Instance.UITransform.GetComponent<QuirksSliders>();
        quirksRateChange = GameManager.Instance.UITransform.GetComponent<QuirksRateChange>();
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
            OFAImgBar.rectTransform.transform.localScale += new Vector3(0.25f, 0, 0) * Time.deltaTime;
            int perCent = Mathf.FloorToInt(OFAImgBar.rectTransform.transform.localScale.x * 100);
            powerUpPerCent.text = perCent + "%";
        }
        else
        {
            powerUpPerCent.gameObject.SetActive(false);
            OFABarLoss();
            FixAllBars();
        }
    }
    public void PoweringUpConsequences()
    {
        int perCent = Mathf.RoundToInt(OFAImgBar.rectTransform.transform.localScale.x * 100);        
        playerAnimatingConditions.canUseOneforAll = true;
        //All OFA slider bars go up to 100 (+)
        OFAMaxSliderValue(perCent);
        //All OFA attacks +100 (+)
        OFAPower(perCent);
            //All Quirks' SlideBars rates go higher (+)
        OFAQuirksRatesRise();
            //Mp bar(fatigue) loss rate go greatly higher (-)
        MpLossRateRise();
            //Maybe and a part of Hp bar(helth) lowers (-)        
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
    private void OFABarLoss()
    {
        OFAImgBar.rectTransform.localScale -= Vector3.right * 0.05f * Time.deltaTime;
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

    private void OFAQuirksRatesRise()
    {
        quirksRateChange.detroitSmashRate += 1;
        quirksRateChange.shootStyleRate += 1;
        quirksRateChange.fingerSmashRate += 1;
    }

    private void OFAPower(int perCent)
    {
        particleForces.punchDamage += perCent;
        particleForces.ShootStyleDamage += perCent;
        particleForces.fingersDamage += perCent;
    }

    private void OFAMaxSliderValue(int perCent)
    {
        quirksSliders.handsAttackSlider.value = perCent;
        quirksSliders.legAttackSlider.value = perCent;
        quirksSliders.fingersAttackSlider.value = perCent;
    }
    private IEnumerator GettingTired()
    {
        if(playerAnimatingConditions.canUseOneforAll)
        {
            while(MpImgBar.rectTransform.transform.localScale.x > 0)
            {
                if(!PauseMenu.theGameIsPaused)
                {
                    MpImgBar.rectTransform.localScale -= Vector3.right * fatigueLossRate/100.0f;
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
