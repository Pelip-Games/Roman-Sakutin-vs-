using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SakutinMovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _stunSeconds;
    [SerializeField] private SakutinAnimatorController _animator;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Phrases _phrases;

    private bool _isStunned;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    public void ApplyStun()
    {
        _isStunned = true;
        _animator.SetStun();
        _rb.velocity = new Vector2(0, 0);
        StartCoroutine(StunRoutine(_stunSeconds));
    }

    private IEnumerator StunRoutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isStunned = false;
        _animator.SetIdle();
    }

    #region Implement MonoBehaviour
    private void Awake()
    {
        _direction = transform.localPosition;
    }

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_speed == 0)
        {
            _speed = 1;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    private void Move()
    {
        if (_isStunned)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            _animator.SetIdle();
        }
        else
        {
            _animator.SetRunning();
        }

        _direction.x = horizontalInput * _speed;
        _direction.y = verticalInput * _speed;

        _rb.velocity = _direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out WeaponProp weapon))
        {
            _weapon.Activate(); // это бы вынести в WeaponInput
            _animator.SetWeapon();
            _phrases.SayGunPhrase();
            Destroy(weapon.gameObject);
        }
    }
}