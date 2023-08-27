using System;
using System.Collections;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private WeaponInput _weaponInput;
    [SerializeField] private Phrases _phrases;

    private void OnEnable()
    {
        if (_wallet is null)
        {
            Debug.LogError($"В контексте сцены нет {nameof(Wallet)}");
        }

        if (_gameMenu is null)
        {
            Debug.LogError($"В контексте сцены нет {nameof(GameMenu)}");
        }

        if (_weaponInput is null)
        {
            Debug.LogError($"В контексте сцены нет {nameof(WeaponInput)}");
        }

        StartCoroutine(SayStartPhrasesRoutine(1));

        _gameMenu.UpdateBalance(_wallet.Balance);

        _wallet.BalanceChanged += UpdateUIBalance;
        _wallet.WalletIsEmpty += DisableWeapon;

        _weaponInput.Fired += RequestMoney;
    }

    private IEnumerator SayStartPhrasesRoutine(int delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        _phrases.SayStartPhrases();
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= UpdateUIBalance;
        _wallet.WalletIsEmpty -= DisableWeapon;

        _weaponInput.Fired -= RequestMoney;
    }

    private void RequestMoney(object sender, EventArgs e)
    {
        _wallet?.RequestMoney(100);
    }

    private void DisableWeapon(object sender, EventArgs e)
    {
        _weaponInput?.Disable();
    }

    private void UpdateUIBalance(object sender, BalanceChangedEventArgs e)
    {
        _gameMenu?.UpdateBalance(e.Amount);
    }
}