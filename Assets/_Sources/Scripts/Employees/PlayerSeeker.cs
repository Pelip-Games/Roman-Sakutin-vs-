using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSeeker : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _visionAngle = 30f;
    [SerializeField, Min(0f)] private float _visionDistance = 3f;
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private VisibilityRange _visibilityRange;

    private NavMeshAgent _agent;
    
    public event Action PlayerBecameVisible;
    public event Action PlayerBecameInvisible;

    private bool _isPlayerVisible;
    private Transform _player;
    private Vector3 Forward => _agent.velocity.normalized;

    private void OnValidate()
    {
        _visibilityRange.Initialize(_visionDistance, _visionAngle, Vector2.up);
        _visibilityRange.SetDirection(Vector2.up);
    }

    private void OnDrawGizmos()
    {
        if (_agent == null)
            return;
        
        Vector3 leftDirection = Quaternion.Euler(new Vector3(0f, 0f, -_visionAngle / 2f)) * Forward;
        Vector3 rightDirection = Quaternion.Euler(new Vector3(0f, 0f, _visionAngle / 2f)) * Forward;
        
        Gizmos.DrawLine(transform.position, transform.position + (leftDirection * _visionDistance));
        Gizmos.DrawLine(transform.position, transform.position + (rightDirection * _visionDistance));
    }

    private void Update()
    {
        bool isPlayerVisible = IsPlayerVisible();
        
        _visibilityRange.SetDirection(Forward);

        if (isPlayerVisible == _isPlayerVisible)
            return;

        if (isPlayerVisible)
            PlayerBecameVisible?.Invoke();
        else
            PlayerBecameInvisible?.Invoke();

        _isPlayerVisible = isPlayerVisible;
    }

    public void Init(Transform player, NavMeshAgent agent)
    {
        _isPlayerVisible = false;
        _player = player;
        _agent = agent;
        
        _visibilityRange.Initialize(_visionDistance, _visionAngle, Forward);
    }

    public void Disable()
    {
        enabled = false;
        _visibilityRange.Disable();
    }

    private bool IsPlayerVisible()
    {
        Vector3 way = _player.position - transform.position;
        bool hasWall = Physics2D.Raycast(transform.position, way.normalized, _visionDistance, _wallMask);
        return way.magnitude <= _visionDistance && InAngle(way.normalized) && hasWall == false;
    }

    private bool InAngle(Vector3 direction)
    {
        return Vector3.Angle(Forward, direction) <= _visionAngle * 0.5f;
    }
}