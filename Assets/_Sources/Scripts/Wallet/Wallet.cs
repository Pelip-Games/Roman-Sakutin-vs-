using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _balance;

    public int Balance => _balance;

    public event EventHandler<BalanceChangedEventArgs> BalanceChanged;
    public event EventHandler WalletIsEmpty;

    public int RequestMoney(int amount)
    {
        if (_balance <= amount)
        {
            _balance = 0;
            WalletIsEmpty?.Invoke(this, EventArgs.Empty);
            BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(_balance));
            return _balance;
        }

        _balance -= amount;
        BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(_balance));
        return amount;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("You cannot add a negative amount of money", nameof(amount));
        }

        if (amount == 0)
        {
            return;
        }

        _balance += amount;
        BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(_balance));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Money money))
        {
            AddMoney(money.Amount);
            Destroy(money.gameObject);
        }
    }
}