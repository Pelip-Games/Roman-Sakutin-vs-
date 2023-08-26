using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private bool _canBeUsed;

    private void Awake()
    {
        _canBeUsed = true;
    }

    private void Update()
    {
        if (_canBeUsed == false
            || Input.GetMouseButtonDown(0) == false)
        {
            return;
        }

        _weapon.Shoot();
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