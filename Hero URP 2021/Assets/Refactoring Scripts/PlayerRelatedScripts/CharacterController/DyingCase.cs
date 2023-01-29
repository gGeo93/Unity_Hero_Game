using UnityEngine;
using UnityEngine.UI;

public class DyingCase : MonoBehaviour
{
    PlayerAnimatingConditions playerAnimatingConditions;
    [SerializeField] Text DyingMessage;
    void Awake() 
    {
        playerAnimatingConditions = GetComponent<PlayerAnimatingConditions>();
    }
    private void QuitGame() => Application.Quit();
    private void DyingMessageAndExit()
    {
        DyingMessage.color = Color.red;
        DyingMessage.fontSize = 20;
        DyingMessage.text = "Game Over";
        Invoke("QuitGame",4f);
    }
    private void DyingState()
    {
        playerAnimatingConditions.canUseOneforAll = false;
        playerAnimatingConditions.isAirFloating = false;
        playerAnimatingConditions.isFloating = false;
        playerAnimatingConditions.isKicking = false;
        playerAnimatingConditions.isRunning = false;
        playerAnimatingConditions.isSmashing = false;
        playerAnimatingConditions.isWalking = false;
        playerAnimatingConditions.isFingering = false;
        playerAnimatingConditions.isIdle = false;
        playerAnimatingConditions.isSweepFalling = false;
        playerAnimatingConditions.isDead = true;

        DyingMessageAndExit();
    }
    public void MainCaseOfDying()
    {
        DyingState();
    }
    public void CaseOfDying()
    {
        if (transform.position.y < -5f)
        {
            DyingState();
        }
    }
}
