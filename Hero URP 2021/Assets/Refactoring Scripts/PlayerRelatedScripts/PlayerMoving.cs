using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float walkingSpeed;
    public float sprindSpeed;
    PlayerAnimatingConditions playerAnimatingConditions;
    PhysicalConditions physicalConditions;
    OneForAllConditions oneForAllConditions;
    [SerializeField]GameObject cam3;

    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        physicalConditions = GetComponent<PhysicalConditions>();
        oneForAllConditions = GetComponent<OneForAllConditions>();    
    }

    public void Move()
    {
        if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            RegainGravity();
            oneForAllConditions.CannotUseOneForAll();
            if (playerAnimatingConditions.cc.isGrounded  && !playerAnimatingConditions.isAirFloating)
            {
                playerAnimatingConditions.cc.Move(transform.forward * this.walkingSpeed * Time.deltaTime);
                playerAnimatingConditions.isWalking = true;
                playerAnimatingConditions.isFloating = false;
            }
            else if (!playerAnimatingConditions.cc.isGrounded)
            {
                playerAnimatingConditions.cc.Move(transform.forward * 2.5f * Time.deltaTime);
                playerAnimatingConditions.isWalking = false;
                playerAnimatingConditions.isFloating = !playerAnimatingConditions.isAirFloating;;
            }
        }
        else if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            RegainGravity();
            oneForAllConditions.CannotUseOneForAll();
            if (playerAnimatingConditions.cc.isGrounded && !playerAnimatingConditions.isAirFloating)
            {
                playerAnimatingConditions.cc.Move(transform.forward * this.sprindSpeed * Time.deltaTime);
                playerAnimatingConditions.isRunning = true;
                playerAnimatingConditions.isFloating = false;
            }
            else if (!playerAnimatingConditions.cc.isGrounded)
            {
                playerAnimatingConditions.cc.Move(transform.forward * this.sprindSpeed * Time.deltaTime);
                playerAnimatingConditions.isRunning = false;
                playerAnimatingConditions.isFloating = !playerAnimatingConditions.isAirFloating;
            }
        }
    }
    private void RegainGravity()
    {
        if (playerAnimatingConditions.isPoweringUp || playerAnimatingConditions.isFingering || playerAnimatingConditions.isSmashing || playerAnimatingConditions.isKicking || playerAnimatingConditions.isUsingFaJin)
        {
            physicalConditions.fallingAcceleration = 0.51f;
            physicalConditions.jumpingAcceleration = 1f;
            playerAnimatingConditions.isWalking = true;
            playerAnimatingConditions.isPoweringUp = false;
            playerAnimatingConditions.isFingering = false;
            playerAnimatingConditions.isSmashing = false;
            playerAnimatingConditions.isKicking = false;
            playerAnimatingConditions.isUsingFaJin = false;
            playerAnimatingConditions.isAirFloating = false;
        }
    }
}
