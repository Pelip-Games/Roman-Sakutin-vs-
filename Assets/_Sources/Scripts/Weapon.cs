using UnityEngine;

internal class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    internal void Shoot()
    {
        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
        var bullet = Instantiate(
            _bulletPrefab,
            transform.position,
            transform.rotation
        );

        // bullet.transform.LookAt(_shootPoint);
    }
}
