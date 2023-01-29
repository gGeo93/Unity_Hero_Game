using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public RawImage HpImgBar;
    public RawImage MpImgBar;
    PlayerAnimatingConditions playerAnimatingConditions;
    ElectricityScript electricityScript;
    AnimatorMainFunctionality animatorMainFunctionality;
    DyingCase dyingCase;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        electricityScript = GetComponent<ElectricityScript>();
        dyingCase = GetComponent<DyingCase>();    
    }
     public void GettingDamage(float amountOfDamage)
    {
        if(animatorMainFunctionality.CurrentState != nameof(PlayerAnimationState.PowerUp))
        {
            HpImgBar.rectTransform.localScale -= Vector3.right*amountOfDamage/100.0f;
            FixHpOrMpBars();
            if(HpImgBar.rectTransform.localScale.x <= 0)
            {
                dyingCase.MainCaseOfDying();
            }
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
    private IEnumerator GettingTired()
    {
        if(playerAnimatingConditions.canUseOneforAll)
        {
            while(MpImgBar.rectTransform.transform.localScale.x > 0)
            {
                if(!PauseMenu.theGameIsPaused)
                {
                    MpImgBar.rectTransform.localScale -= Vector3.right * 0.005f/100.0f;
                }
                yield return new WaitForSeconds(3.0f);
            }
            FixHpOrMpBars();
            electricityScript.ElectricityOff();
        }
    }
    private IEnumerator RegainingStrength()
    {
        while(!playerAnimatingConditions.canUseOneforAll && MpImgBar.rectTransform.transform.localScale.x < 1)
        {
            MpImgBar.rectTransform.localScale += Vector3.right * 0.1f/100.0f;
            yield return new WaitForSeconds(1.0f);
        }
        electricityScript.isTired = false;
    }
    private void FixHpOrMpBars()
    {
        if(HpImgBar.rectTransform.transform.localScale.x < 0)
        {
            HpImgBar.rectTransform.transform.localScale= new Vector3(0f,1f,1f);
        }
        if(MpImgBar.rectTransform.transform.localScale.x < 0)
        {
            MpImgBar.rectTransform.transform.localScale = new Vector3(0f,1f,1f);
        }    
    }
}
