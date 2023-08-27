using System;
using UnityEngine;

public class MoneyHunter : MonoBehaviour
{
    [SerializeField, Min(1)] private int _neededMoney = 500;

    public bool _isHunted = false;
    public event Action Hunted;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHunted)
            return;
        
        if (collision.transform.TryGetComponent(out Wallet wallet) == false)
            return;

        wallet.RequestMoney(_neededMoney);
        _isHunted = true;
        Hunted?.Invoke();

        if (collision.transform.TryGetComponent(out SakutinMovementController sakutin))
        {
            sakutin.ApplyStun();
        }
    }
}