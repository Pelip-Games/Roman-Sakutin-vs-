using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SakutinMovementController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction;
    private Rigidbody2D _rb;

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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _direction.x = horizontalInput * _speed;
        _direction.y = verticalInput * _speed;

        _rb.velocity = _direction;
    }
}