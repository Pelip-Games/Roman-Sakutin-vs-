using System;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private WeaponInput _weaponInput;

    private void OnEnable()
    {
        _gameMenu.UpdateBalance(_wallet.Balance);

        _wallet.BalanceChanged += UpdateUIBalance;
        _wallet.WalletIsEmpty += DisableWeapon;

        _weaponInput.Fired += RequestMoney;
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= UpdateUIBalance;
        _wallet.WalletIsEmpty -= DisableWeapon;

        _weaponInput.Fired -= RequestMoney;
    }

    private void RequestMoney(object sender, EventArgs e)
    {
        _wallet.RequestMoney(100);
    }

    private void DisableWeapon(object sender, EventArgs e)
    {
        _weaponInput.Disable();
    }

    private void UpdateUIBalance(object sender, BalanceChangedEventArgs e)
    {
        _gameMenu.UpdateBalance(e.Amount);
    }
}