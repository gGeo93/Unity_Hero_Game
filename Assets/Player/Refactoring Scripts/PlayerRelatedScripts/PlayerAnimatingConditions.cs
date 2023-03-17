using UnityEngine;
public class PlayerAnimatingConditions : MonoBehaviour
{
    public CharacterController cc;

    public bool canUseOneforAll;
    public bool isPoweringUp;
    public bool isWalking;
    public bool isRunning;
    public bool isIdle;
    public bool isFloating;
    public bool isSmashing;
    public bool isKicking;
    public bool isFingering;
    public bool isAirFloating;
    public bool isLanding;
    public bool isUsingBlackWhipForAttack;
    public bool isUsingBlackWhipForSwing;
    public bool isUsingFaJin;
    public bool canEmmitSmoke = true;
    public bool isUsingSmokeQuirk;
    public bool isDead;
    public bool isSweepFalling;
    public bool isTurningBehind;
    public bool canDodgeWithDangerSense;
    public bool canGoShiftingSpeed = true;
    public bool SBtnPressed() => Input.GetKeyDown(KeyCode.S);

    void Awake() 
    {
        cc = GetComponent<CharacterController>();
    }
    
    public void IsIdleOrFloating()
    {
        if((!Input.GetKey(KeyCode.W) || isLanding) && !isDead)
        {
            if(cc.isGrounded)
            {
                isPoweringUp = false;
                isKicking = false;
                
                isRunning = false;
                isWalking = false;
                
                isSmashing = false;
                isFingering = false;

                isUsingBlackWhipForAttack = false;
                
                isSweepFalling = false;
                isUsingFaJin = false;
                
                isAirFloating = false;
                isLanding = false;
                
                isFloating = false;
                isIdle=true;
            }
            else if (!cc.isGrounded)
            {
                isPoweringUp = false;
                isKicking = false;
                
                isRunning = false;
                isWalking = false;
                
                isSmashing = false;
                isFingering = false;

                isUsingBlackWhipForAttack = false;
                isUsingFaJin = false;

                isSweepFalling = false;
                isAirFloating = false;
                isLanding = false;
                
                isIdle = false;
                isFloating=true;
            }
        }
    }
}
