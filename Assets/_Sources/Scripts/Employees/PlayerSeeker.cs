using System;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _visionAngle = 30f;
    [SerializeField, Min(0f)] private float _visionDistance = 3f;
    [SerializeField] private LayerMask _wallMask;

    public event Action PlayerBecameVisible;
    public event Action PlayerBecameInvisible;

    private bool _isPlayerVisible;
    private Transform _player;

    private void OnDrawGizmos()
    {
        Vector3 leftDirection = Quaternion.Euler(new Vector3(0f, 0f, -_visionAngle / 2f)) * transform.up;
        Vector3 rightDirection = Quaternion.Euler(new Vector3(0f, 0f, _visionAngle / 2f)) * transform.up;
        
        Gizmos.DrawLine(transform.position, transform.position + (leftDirection * _visionDistance));
        Gizmos.DrawLine(transform.position, transform.position + (rightDirection * _visionDistance));
    }

    private void Update()
    {
        bool isPlayerVisible = IsPlayerVisible();

        if (isPlayerVisible == _isPlayerVisible)
            return;

        if (isPlayerVisible)
            PlayerBecameVisible?.Invoke();
        else
            PlayerBecameInvisible?.Invoke();

        _isPlayerVisible = isPlayerVisible;
    }

    public void Init(Transform player)
    {
        _isPlayerVisible = false;
        _player = player;
    }

    private bool IsPlayerVisible()
    {
        Vector2 way = _player.position - transform.position;
        return way.magnitude <= _visionDistance && InAngle(way.normalized) ;
    }

    private bool InAngle(Vector2 direction)
    {
        return _visionAngle * 0.5f <= Vector2.Angle(transform.up, direction);
    }
}