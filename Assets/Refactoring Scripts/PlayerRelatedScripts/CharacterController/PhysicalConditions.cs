using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalConditions : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField] private Transform dekusRealTransform;
    private float airLimitOffeset = 1f;
    private float airLimitWhileSwinging = 3f;
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
                beforeJumpLimit = dekusRealTransform.position.y + 
                (!playerAnimatingConditions.isUsingBlackWhipForSwing 
                ? airLimitOffeset : airLimitWhileSwinging);
            }
            airLimit = beforeJumpLimit;
            return airLimit;
        } 
        set
        {
            if(playerAnimatingConditions.canUseOneforAll)
            {
                airLimitOffeset *= 3f;
                airLimitWhileSwinging *= 4f;
            }
            else
            {
                airLimitOffeset = 1f;
                airLimitWhileSwinging = 3f;
            }
        }
    }
    public float gravity = 0f;
    PlayerAnimatingConditions playerAnimatingConditions;

    void Awake() 
    {
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();    
    }
    void Start() 
    {
        AirLimit = 1.5f;
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
