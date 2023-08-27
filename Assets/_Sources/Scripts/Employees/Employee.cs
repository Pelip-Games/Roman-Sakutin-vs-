using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    [SerializeField] private float _stalkeringStopDelay = 1f;
    [SerializeField] private float _destroyDelay = 3f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _player;
    [SerializeField] private Patrolling _patrolling;
    [SerializeField] private PlayerSeeker _playerSeeker;
    [SerializeField] private Stalker _stalker;
    //[SerializeField] private GoAway _goAway;
    [SerializeField] private Phrases _phrases;
    [SerializeField] private MoneyHunter _moneyHunter;
    [SerializeField] private EmployeeSpriteSwap _spriteSwap;
    [SerializeField] private EmployeeAnimator _animator;

    private bool _delayActive;
    private Coroutine _delay;

    public event Action<Employee> MoneyTaken;
    
    private void Awake()
    {
        _patrolling.Init(_agent);
        _playerSeeker.Init(_player, _agent);
        _stalker.Init(_player, _agent);
        //_goAway.Init(_agent);
        _spriteSwap.Init(_agent);
        
        _patrolling.Enable();
        _animator.Walk();
    }

    private void OnEnable()
    {
        _playerSeeker.PlayerBecameVisible += OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible += OnPlayerBecameInvisible;
        _moneyHunter.Hunted += OnHunted;
    }

    private void OnDisable()
    {
        _playerSeeker.PlayerBecameVisible -= OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible -= OnPlayerBecameInvisible;
        _moneyHunter.Hunted -= OnHunted;
    }

    [ContextMenu(nameof(TakeMoney))]
    public void TakeMoney()
    {
        if (_delay != null)
            StopCoroutine(_delay);
        
        _phrases.SayMoneyPhrase();
        _animator.GetCash();
        _playerSeeker.Disable();
        _moneyHunter.Disable();
        _patrolling.Disable();
        _stalker.Disable();

        _rigidbody.simulated = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.inertia = 0f;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        _agent.enabled = false;
        
        Vector3 direction = transform.position - _player.position;
        direction.z = 0;
        direction.Normalize();
        // _goAway.Go(direction);
        //
        // _goAway.Gone += OnGone;
        
        OnDisable();
        
        MoneyTaken?.Invoke(this);
        
        Destroy(gameObject, _destroyDelay);
    }

    private void OnPlayerBecameVisible()
    {
        if (_delayActive)
        { 
            StopCoroutine(_delay);
        }
        else
        {
            _patrolling.Disable();
            _stalker.Enable();
            _animator.Run();
            _phrases.SayStalkerPhrase();
        }
    }

    private void OnPlayerBecameInvisible()
    {
        _delayActive = true;
        _delay = StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_stalkeringStopDelay);

        _stalker.Disable();
        _patrolling.Enable();
        _phrases.SayMissPhrase();
        _animator.Walk();
        _delayActive = false;
    }

    private void OnHunted()
    {
        TakeMoney();
    }

    // private void OnGone()
    // {
    //     _goAway.Gone -= OnGone;
    //     Destroy(gameObject);
    // }
}