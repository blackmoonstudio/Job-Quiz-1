using Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStick_Movement : MonoBehaviour
{
    private PlayerInputSystem controls;
    private Vector3 move;
    private Vector3 look;
    private CharacterController characterController;
    private Camera playerCamera;

    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    private void Awake()
    {
        controls = new PlayerInputSystem();
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector3>();
        controls.Player.Move.canceled += ctx => move = Vector3.zero;
        controls.Player.Look.performed += ctx => look = ctx.ReadValue<Vector3>();
        controls.Player.Look.canceled += ctx => look = Vector3.zero;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        MovePlayer();
        LookAround();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(move.x, 0, move.y);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void LookAround()
    {
        Vector3 lookDelta = look * lookSpeed * Time.deltaTime;
        transform.Rotate(0, lookDelta.x, 0);
        playerCamera.transform.Rotate(-lookDelta.y, 0, 0);
    }
}
