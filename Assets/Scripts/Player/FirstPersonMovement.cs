using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    [SerializeField] private float runSpeed = 9;

    [SerializeField] private Transform cam;

    [Inject] private PlayerInput _input;

    private float _currentMovingSpeed;

    public bool IsRunning {get; private set;}

    private Rigidbody _rigidbody;

    private bool _isRunning;

    private Vector2 _targetVelocity;

    private Joystick _joystick;
    public Joystick Joystick { get { return _joystick; } set { } }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joystick = FindObjectOfType<Joystick>();
        _currentMovingSpeed = speed;

        _input.Player.Run.started += context => StartRun();
        _input.Player.Run.canceled += context => EndRun();
    }

    void FixedUpdate()
    {
        _targetVelocity = _input.Player.Move.ReadValue<Vector2>() * _currentMovingSpeed;

        _rigidbody.velocity = transform.rotation * new Vector3(_targetVelocity.x, _rigidbody.velocity.y, _targetVelocity.y);
    }

    private void StartRun()
    {
        _currentMovingSpeed = runSpeed;
    }

    private void EndRun()
    {
        _currentMovingSpeed = speed;
    }
}