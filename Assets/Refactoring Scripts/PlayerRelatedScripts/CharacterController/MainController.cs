using UnityEngine;

public class MainController : MonoBehaviour
{
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    AnimatorMainFunctionality animatorMainFunctionality;
    AnimationPlaying animationPlaying;
    PlayerStats playerStats;
    PlayerMoving playerMoving;
    BlackWhipSwinging swinging;
    PlayerJump playerJump;
    IdleOrFloatingCondition idleOrFloating;
    QuirksSlidersFunctionality quirksSlidersFunctionality;
    ElectricityScript electricityScript;
    OneForAllConditions oneForAllConditions;
    DangerSenseQuirk dangerSenseQuirk;
    ShiftingGearQuirk shiftingGearQuirk;
    AirFloatQuirk airFloatQuirk;
    FaJinQuirk faJinQuirk;
    SmokeQuirk smokeQuirk;
    PlayerTurnBehind playerTurnBehind;
    SweepFall sweepFall;
    DyingCase dying;
    FixingRotation fixingRotation;    

    void Awake() 
    {
        physicalConditions = GetComponent<PhysicalConditions>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        animationPlaying = GetComponent<AnimationPlaying>();
        animatorMainFunctionality = GetComponent<AnimatorMainFunctionality>();
        playerStats = GetComponent<PlayerStats>();
        playerMoving = GetComponent<PlayerMoving>();
        swinging = GetComponent<BlackWhipSwinging>();
        playerJump = GetComponent<PlayerJump>();
        idleOrFloating = GetComponent<IdleOrFloatingCondition>();
        sweepFall = GetComponent<SweepFall>();
        dying = GetComponent<DyingCase>();
        quirksSlidersFunctionality = GameManager.Instance.quirksSlidersFunctionality;
        electricityScript = GetComponent<ElectricityScript>();
        oneForAllConditions = GetComponent<OneForAllConditions>();
        shiftingGearQuirk = GetComponent<ShiftingGearQuirk>();
        dangerSenseQuirk = GetComponent<DangerSenseQuirk>();
        airFloatQuirk = GetComponent<AirFloatQuirk>();
        faJinQuirk = GetComponent<FaJinQuirk>();
        smokeQuirk = GetComponent<SmokeQuirk>();
        playerTurnBehind = GetComponent<PlayerTurnBehind>();
        fixingRotation = GetComponent<FixingRotation>();
    }
    
    void Update()
    {
        PlayerBehaviour();
    }
    void FixedUpdate() 
    {
        PlayerAnimatedNow();
    }

    private void PlayerBehaviour()
    {
        dangerSenseQuirk.DodgingLaserBeamCondition();
        playerTurnBehind.PlayerTurns();
        shiftingGearQuirk.StartShiftingGear();
        dying.CaseOfDying();
        playerStats.HealingProcess();
        playerStats.FatigueLoss();
        swinging.Swing();
        playerJump.Jump();
        playerJump.Fall();
        playerJump.Falling();
        airFloatQuirk.UsingFloatQuirk();
        quirksSlidersFunctionality.GeneralOFAAttacksPower(playerStats.MpImgBar.rectTransform.transform.localScale.x);
        oneForAllConditions.OneforallDifferentUses();
        playerMoving.Move();
        faJinQuirk.FajinActivating();
        smokeQuirk.SmokeQuirkActivative();
        sweepFall.SweepFalling();
        idleOrFloating.StayingIdleOrFloating();
        fixingRotation.RotationFix();
    }
    private void PlayerAnimatedNow()
    {
        animationPlaying.AnimationPlayingNow();            
    }
}
