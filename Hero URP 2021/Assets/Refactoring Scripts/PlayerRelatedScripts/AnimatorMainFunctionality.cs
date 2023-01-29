using UnityEngine;

public class AnimatorMainFunctionality : MonoBehaviour, IAnimatableAbstraction
{
    Animator playerAnimator;
    private string currentState = "init state";
    public string CurrentState { get { return currentState; } private set { } }
    void Awake() 
    {
        playerAnimator = GetComponent<Animator>();
    }
    void Start() 
    {
        CurrentState = currentState;    
    }
    public void AnimationState(string newState)
    {
        ChangeAnimationState(newState);
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        playerAnimator.Play(newState);
        currentState = newState;
    }
}
