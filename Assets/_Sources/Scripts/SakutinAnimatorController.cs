using UnityEngine;

public class SakutinAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private bool _isRunning;

    public void SetWeapon()
    {
        _animator.SetBool("isArmed", true);
    }

    public void DropWeapon()
    {
        _animator.SetBool("isArmed", false);
    }

    public void SetIdle()
    {
        if (_isRunning == false)
        {
            return;
        }

        _isRunning = false;
        _animator.SetTrigger("isIdle");
    }

    public void SetRunning()
    {
        if (_isRunning)
        {
            return;
        }

        _isRunning = true;
        _animator.SetTrigger("isRunning");
    }

    public void SetHurt()
    {
        _animator.SetTrigger("isHurt");
    }

    public void SetStun()
    {
        _animator.SetTrigger("isStunned");
    }
}