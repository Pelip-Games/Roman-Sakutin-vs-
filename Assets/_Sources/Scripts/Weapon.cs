using UnityEngine;

internal class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    public bool IsActive { get; private set; }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    internal void Shoot()
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
