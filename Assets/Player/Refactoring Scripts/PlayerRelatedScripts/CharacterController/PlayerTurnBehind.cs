using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBehind : MonoBehaviour
{
    CharacterController cc;
    PlayerAnimatingConditions playerAnimatingConditions;

    void Awake() 
    {
        cc = GetComponent<CharacterController>();
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();    
    }
    public void PlayerTurns()
    {
        if(Input.GetKeyDown(KeyCode.T) && !Input.GetKey(KeyCode.W) && cc.isGrounded
        && !playerAnimatingConditions.isTurningBehind 
        && !playerAnimatingConditions.isPoweringUp 
        && !playerAnimatingConditions.isSmashing 
        && !playerAnimatingConditions.isFingering 
        && !playerAnimatingConditions.isKicking 
        && !playerAnimatingConditions.isAirFloating
        && !playerAnimatingConditions.isUsingBlackWhipForAttack
        )
        {
            playerAnimatingConditions.isTurningBehind = true;
            StartCoroutine(SmoothlyTurnsBehind());
        }
    }
    private IEnumerator SmoothlyTurnsBehind()
    {
        yield return new WaitForSeconds(1f);
        Quaternion playersQuaternion = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 180, Vector3.up);
        transform.rotation = playersQuaternion;
        playerAnimatingConditions.isTurningBehind = false;
        playerAnimatingConditions.isIdle = true;
    }
}
