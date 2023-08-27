using System;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private WinPanel _gameOverPanel;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private EmployeesCounter _employeesCounter;

    private void Start()
    {
        _wallet.WalletIsEmpty += WalletOnWalletIsEmpty;
        _employeesCounter.EmployeesEnd += EmployeesCounterOnEmployeesEnd;
    }

    private void EmployeesCounterOnEmployeesEnd()
    {
        FinishGame(_winPanel.gameObject);
    }

    private void WalletOnWalletIsEmpty(object sender, EventArgs e)
    {
        FinishGame(_gameOverPanel.gameObject);
    }

    private void FinishGame(GameObject panel)
    {
        Instantiate(panel);
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _wallet.WalletIsEmpty -= WalletOnWalletIsEmpty;
        _employeesCounter.EmployeesEnd -= EmployeesCounterOnEmployeesEnd;
    }
}