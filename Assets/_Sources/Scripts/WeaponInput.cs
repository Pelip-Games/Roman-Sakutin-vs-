using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weapon.Shoot();
        }
    }
}