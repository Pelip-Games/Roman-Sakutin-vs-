using System;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private bool _canBeUsed;

    public event EventHandler Fired;

    private void Awake()
    {
        _canBeUsed = true;
    }

    private void Update()
    {
        if (_canBeUsed == false
            || Input.GetMouseButtonDown(0) == false
            || _weapon.IsActive == false)
        {
            return;
        }

        _weapon.Shoot();
        Fired?.Invoke(this, EventArgs.Empty);
    }

    public void Enable()
    {
        _canBeUsed = true;
    }

    public void Disable()
    {
        _canBeUsed = false;
    }
}