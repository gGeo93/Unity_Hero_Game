using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaJinQuirk : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
    }
    public void FajinActivating()
    {
        if (!playerAnimatingConditions.isUsingFaJin && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.cc.isGrounded && playerAnimatingConditions.canUseOneforAll && Input.GetKeyDown(KeyCode.G))
        {
            playerAnimatingConditions.isUsingFaJin = true;
            StartCoroutine(EndOfFaJinAnimation());
        }
    }
    IEnumerator EndOfFaJinAnimation()
    {
        yield return new WaitForSeconds(1.75f);
        playerAnimatingConditions.isUsingFaJin = false;
    }
}
