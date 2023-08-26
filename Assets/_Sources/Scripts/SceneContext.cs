using System;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private WeaponInput _weaponInput;

    private void OnEnable()
    {
        _wallet.WalletChanged += UpdateUIBalance;

        _wallet.WalletIsEmpty += DisableWeapon;
    }

    private void OnDisable()
    {
        _wallet.WalletChanged -= UpdateUIBalance;

        _wallet.WalletIsEmpty -= DisableWeapon;
    }

    private void DisableWeapon(object sender, EventArgs e)
    {
        _weaponInput.Disable();
    }

    private void UpdateUIBalance(object sender, WalletChangedEventArgs e)
    {
        _gameMenu.UpdateBalance(e.Balance);
    }
}