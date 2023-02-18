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
        if (Input.GetKey(KeyCode.G) && !playerAnimatingConditions.isDead && !playerAnimatingConditions.isSweepFalling && playerAnimatingConditions.cc.isGrounded)
        {
            playerAnimatingConditions.isUsingFaJin = true;
            //Fajin Bar goes in a certain rate from 1 to 0
            //FaJin particles go to a loop
            //if Q,Z,X stores energy to a specific part of Deku's body
            //3 oneforall bars begin to fill from 0 to 100
            //You can swip from storing energy from one part of the body to the other
            //When FaJin bar reaches zero the storing energy process is over and the bar begins to fill at a specific rate
            //The Q,Z,X bars start to lose their energy with a rate
        }
        else if(Input.GetKeyUp(KeyCode.G))
        {
            playerAnimatingConditions.isUsingFaJin = false;
        }
    }
    IEnumerator EndOfFaJinAnimation()
    {
        yield return new WaitForSeconds(1.75f);
        playerAnimatingConditions.isUsingFaJin = false;
    }
}
