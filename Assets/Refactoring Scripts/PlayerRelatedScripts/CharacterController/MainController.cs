using UnityEngine;

public class MainController : MonoBehaviour
{
    PhysicalConditions physicalConditions;
    PlayerAnimatingConditions playerAnimatingConditions;
    AnimatorMainFunctionality animatorMainFunctionality;
    AnimationPlaying animationPlaying;
    PlayerStats playerStats;
    PlayerMoving playerMoving;
    PlayerJump playerJump;
    IdleOrFloatingCondition idleOrFloating;
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
        playerJump = GetComponent<PlayerJump>();
        idleOrFloating = GetComponent<IdleOrFloatingCondition>();
        sweepFall = GetComponent<SweepFall>();
        dying = GetComponent<DyingCase>();
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
        playerStats.FatigueLoss();
        playerJump.Jump();
        playerJump.Fall();
        playerJump.Falling();
        airFloatQuirk.UsingFloatQuirk();
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