using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _balance;

    public event EventHandler<WalletChangedEventArgs> WalletChanged;
    public event EventHandler WalletIsEmpty;

    public int RequestMoney(int amount)
    {
        if (amount <= _balance)
        {
            WalletIsEmpty?.Invoke(this, EventArgs.Empty);
            _balance = 0;
            return _balance;
        }

        _balance -= amount;
        WalletChanged?.Invoke(this, new WalletChangedEventArgs(_balance));
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
        WalletChanged?.Invoke(this, new WalletChangedEventArgs(_balance));
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