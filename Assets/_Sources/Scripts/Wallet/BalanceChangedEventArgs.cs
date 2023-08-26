using System;

public class BalanceChangedEventArgs : EventArgs
{
    public int Amount { get; }

    public BalanceChangedEventArgs(int amount)
    {
        Amount = amount;
    }
}