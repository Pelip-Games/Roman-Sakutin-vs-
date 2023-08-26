#nullable enable
using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI? _balance;
    [SerializeField] private TextMeshProUGUI? _employees;

    public void UpdateBalance(int newCount)
    {
        if (_balance is null)
        {
            return;
        }

        _balance.text = newCount.ToString();
    }

    public void UpdateEmployees(int newCount)
    {
        if (_employees is null)
        {
            return;
        }

        _employees.text = newCount.ToString();
    }
}
