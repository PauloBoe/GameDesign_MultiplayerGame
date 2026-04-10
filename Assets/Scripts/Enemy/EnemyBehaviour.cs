using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;


/*
    AUTEUR: Stijn Grievink
    USER STORY: [EX-6] 
*/
public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private States _currentState;
    private Transform _currentTarget = null;
    // Variables for patrolling
    [Header("Patrolling")]
    [SerializeField] public List<Transform> _waypoints = new();
    private int _waypointIndex = 0;
    // Variables for checking for player
    private float _playerCheckTime = 0f;
    private float _playerCheckDelay = 1f;
    [Header("Player checks")]
    [SerializeField] private float _visionRange = 100f;
    [SerializeField] private float _visionCone = 45f;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _playerGroundMask;
    private GameObject _player;
    // Variables for enemy movement
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 4f;
    // Variables for shooting
    [SerializeField] private BasicPistol _basicPistol;
    [SerializeField] private float _shootingRange = 100f;

    private enum States
    {
        Patrolling,
        Hiding,
        Rushing,
        Shooting,
    }

    private void Awake()
    {
        // Get references to necessary components
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        // Set enemies movement state at the start of the game
        _currentState = States.Patrolling;
        if (_waypoints.Count > 0)
        {
            _currentTarget = _waypoints[_waypointIndex];
        }
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            // Logic for the patrolling state when the enemy is idle
            case States.Patrolling:
                // Set speed to walking
                _agent.speed = _walkSpeed;
                // Patrolling logic
                if (_playerCheckTime > _playerCheckDelay)
                {
                    // Check for player
                    if (CheckForPlayer())
                    {
                        SetToHunt();
                        break;
                    }

                    // Set waypoint again
                    if (_waypoints.Count > 0)
                    {
                        _currentTarget = _waypoints[_waypointIndex];
                    }
                    // Reset check time
                    _playerCheckTime = 0f;
                }
                // Check if new waypoint has been reached
                if (_currentTarget != null && (_currentTarget.position - transform.position).magnitude < 2f)
                {
                    _waypointIndex = (_waypointIndex + 1) % _waypoints.Count;
                    _currentTarget = _waypoints[_waypointIndex];
                }
                _playerCheckTime += Time.fixedDeltaTime;
                break;
            // Logic for the rushing state when the enemy sees the player
            case States.Rushing:
                _agent.speed = _runSpeed;
                if ((_player.transform.position - transform.position).magnitude < _shootingRange)
                {
                    _currentState = States.Shooting;
                }
                break;
            case States.Shooting:
                _agent.speed = _walkSpeed;
                // Shoot, reload if mag empty
                // Vision Cone check
                float angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
                if (angleToPlayer < _visionCone)
                {
                    _basicPistol.Fire(_player.transform.position);
                    if (_basicPistol.CurrentBullets() <= 0)
                    {
                        _basicPistol.Reload();
                    }
                }
                break;
        }
        // Set the destination again to update pathfinding
        if (_currentTarget != null)
        {
            _agent.SetDestination(_currentTarget.position);
        }
    }

    private bool CheckForPlayer()
    {
        // Check for player
        if ((_player.transform.position - transform.position).magnitude < _visionRange)
        {
            // Vision Cone check
            float angleToPlayer = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
            if (angleToPlayer < _visionCone)
            {
                // Line of sight check
                RaycastHit hit;
                if (Physics.Linecast(transform.position, _player.transform.position, out hit, _playerGroundMask))
                {
                    if (hit.collider.gameObject.layer == 3)
                    {
                        // Return true if all these conditions are met
                        return true;
                    }
                };
                // Draw a ray for debugging in scene view
                Debug.DrawRay(transform.position, _player.transform.position - transform.position);
            }
        }
        // Return false if any of the conditions are not met, which means the player is not seen
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a debug sphere to see the vision range of the enemies in the scene view
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }

    public void SetToHunt()
    {
        // Public function to set the enemies state to rushing which can be called externally, for example if the enemy is shot and needs to react
        _currentState = States.Rushing;
        _currentTarget = _player.transform;
    }
}
