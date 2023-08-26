using System;

public class WalletChangedEventArgs : EventArgs
{
    public int Balance { get; }

    public WalletChangedEventArgs(int balance)
    {
        Balance = balance;
    }
}