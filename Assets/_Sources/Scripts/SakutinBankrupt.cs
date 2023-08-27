using System;
using UnityEngine;

public class SakutinBankrupt : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SakutinMovementController _movement;
    [SerializeField] private SakutinAnimatorController _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Start()
    {
        _wallet.WalletIsEmpty += OnWalletIsEmpty;
    }

    private void OnDestroy()
    {
        _wallet.WalletIsEmpty -= OnWalletIsEmpty;
    }

    private void OnWalletIsEmpty(object sender, EventArgs e)
    {
        _wallet.WalletIsEmpty -= OnWalletIsEmpty;
        
        _movement.enabled = false;
        _animator.SetLosing();
        _rigidbody.simulated = false;
    }
}