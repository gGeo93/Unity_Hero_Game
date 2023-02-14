using UnityEngine;

public class OneForAllConditions : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    PhysicalConditions physicalConditions;
    PlayerStats playerStats;
    
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
        physicalConditions = GetComponent<PhysicalConditions>();
        playerStats = GetComponent<PlayerStats>();
    }
    public void OneforallDifferentUses()
    {
        if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && playerAnimatingConditions.canUseOneforAll && !playerAnimatingConditions.isTurningBehind && Input.GetKeyDown(KeyCode.Q))
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isSmashing = true;
        }
        else if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && playerAnimatingConditions.canUseOneforAll && !playerAnimatingConditions.isTurningBehind && Input.GetKeyDown(KeyCode.Z))
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isKicking = true;
        }
        else if (!playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.canGoShiftingSpeed && playerAnimatingConditions.canUseOneforAll && !playerAnimatingConditions.isTurningBehind && Input.GetKeyDown(KeyCode.X))
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isFingering = true;
        }
        else if (playerStats.MpImgBar.rectTransform.transform.localScale.x >= (1.0f - 0.1f) && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && !playerAnimatingConditions.isTurningBehind && Input.GetKeyDown(KeyCode.R) && !Input.GetKey(KeyCode.W))
        {
            physicalConditions.ZeroGravity();
            playerAnimatingConditions.isPoweringUp = true;
        }
    }
    public void CannotUseOneForAll()
    {
        if (playerAnimatingConditions.isPoweringUp)
            playerAnimatingConditions.canUseOneforAll = false;
    }
}
