using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalConditions : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField] private Transform dekusRealTransform;
    private const float airLimitOffeset = 1f;
    private float beforeJumpLimit;
    private bool canResetTheAirLimit;
    public float jumpingAcceleration;
    public float fallingAcceleration;
    private float airLimit;
    public float AirLimit 
    {
        get
        {
            if(cc.isGrounded)
            {
                beforeJumpLimit = dekusRealTransform.position.y + airLimitOffeset;
                airLimit = beforeJumpLimit;
                canResetTheAirLimit = true;
            }
            else if(!playerAnimatingConditions.canUseOneforAll)
            {
                StartCoroutine(GetLimitAfterAwhile());
            }
            return !playerAnimatingConditions.canUseOneforAll ? airLimit : 15;
        } 
        set
        {
            airLimit = value;
        }
    }
    public float gravity = 0f;
    PlayerAnimatingConditions playerAnimatingConditions;

    void Awake() 
    {
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();    
    }
    
    IEnumerator GetLimitAfterAwhile()
    {
        if(canResetTheAirLimit)
        {
            yield return new WaitForSeconds(2f);
            canResetTheAirLimit = false;
            beforeJumpLimit = dekusRealTransform.position.y - airLimitOffeset;
            AirLimit = beforeJumpLimit;
        }
    }
    public void RepairingGravity()
    {
        this.fallingAcceleration=0.51f;
        this.jumpingAcceleration=1f;
        playerAnimatingConditions.IsIdleOrFloating();
    }
    public void ZeroGravity()
    {
        this.jumpingAcceleration = 0;
        this.fallingAcceleration = 0;
        this.gravity = 0;
    }
}