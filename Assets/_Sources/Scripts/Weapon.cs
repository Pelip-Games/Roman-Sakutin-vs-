using UnityEngine;

internal class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private SpriteRenderer _weaponSprite;

    public bool IsActive { get; private set; }

    public void Activate()
    {
        IsActive = true;
        _weaponSprite.gameObject.SetActive(IsActive);
    }

    public void Deactivate()
    {
        IsActive = false;
        _weaponSprite.gameObject.SetActive(IsActive);
    }

    internal void Shoot()
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
