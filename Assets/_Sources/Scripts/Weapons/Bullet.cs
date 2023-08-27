using UnityEngine;

internal class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifetime = 5f;

    private float _deathTimer = 5f;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.right);

        _deathTimer += Time.deltaTime;

        if (_deathTimer >= _lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent(typeof(SakutinMovementController)) is not null)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Employee>(out Employee employee))
        {
            employee.TakeMoney();
        }

        Destroy(gameObject);
    }
}