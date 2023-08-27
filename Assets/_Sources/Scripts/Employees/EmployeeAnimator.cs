using UnityEngine;

public class EmployeeAnimator : MonoBehaviour
{
    private readonly int _idle = Animator.StringToHash("isIdle");
    private readonly int _walk = Animator.StringToHash("isWalking");
    private readonly int _run = Animator.StringToHash("isRunning");
    private readonly int _cash = Animator.StringToHash("isGettingCash");
    private readonly int _stun = Animator.StringToHash("isStunned");
    
    [SerializeField] private Animator _animator;
    
    public void Idle()
    {
        _animator.SetTrigger(_idle);
    }
    
    public void Walk()
    {
        _animator.SetTrigger(_walk);
    }
    
    public void Run()
    {
        _animator.SetTrigger(_run);
    }

    public void Stun()
    {
        _animator.SetTrigger(_stun);
    }

    public void GetCash()
    {
        _animator.SetTrigger(_cash);
    }
}