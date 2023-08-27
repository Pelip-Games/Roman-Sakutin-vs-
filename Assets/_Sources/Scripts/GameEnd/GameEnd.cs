using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _delay = 5f;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private EmployeesCounter _employeesCounter;

    private void Start()
    {
        _canvas.enabled = false;
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

    private async void FinishGame(GameObject panel)
    {
        await Task.Delay(TimeSpan.FromSeconds(_delay));
        
        _canvas.enabled = true;
        Instantiate(panel, _canvas.transform);
        Unsubscribe();

        Cursor.visible = true;
    }

    private void Unsubscribe()
    {
        _wallet.WalletIsEmpty -= WalletOnWalletIsEmpty;
        _employeesCounter.EmployeesEnd -= EmployeesCounterOnEmployeesEnd;
    }
}