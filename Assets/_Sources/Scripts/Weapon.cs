using UnityEngine;

internal class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    internal void Shoot()
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
