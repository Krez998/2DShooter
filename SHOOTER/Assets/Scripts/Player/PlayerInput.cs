using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Player _player;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }

    private void Update()
    {
        _moveDirection = _playerControls.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _player.Move(_moveDirection);
    }
}
