using UnityEngine;

internal class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private SpriteRenderer _weaponSprite;
    [SerializeField] private Transform _shootPoint;    

    public bool IsActive { get; private set; }

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(IsActive);
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(IsActive);
    }

    internal void Shoot()
    {
        Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);
    }
}
